/*
 * Tencent is pleased to support the open source community by making xLua available.
 * Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
 * Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
 * http://opensource.org/licenses/MIT
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
*/

using System.Collections.Generic;
using System;
using XLua;
using System.Reflection;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEditor;

//配置的详细介绍请看Doc下《XLua的配置.doc》
public static class GenConfig
{

    [MenuItem("XLua/Generate Unity Type", false, 3)]
    public static void GenUnityTypes()
    {
        List<string> namespaces = new List<string>() // 在这里添加名字空间
           {
"UnityEngine",
// "UnityEngine.Accessibility",
// "UnityEngine.AI",
"UnityEngine.Analytics",
// "UnityEngine.Android",
// "UnityEngine.Animations",
// "UnityEngine.Apple.ReplayKit",
// "UnityEngine.Apple.TV",
"UnityEngine.Assertions",
"UnityEngine.Assertions.Comparers",
"UnityEngine.Assertions.Must",
"UnityEngine.Audio",
"UnityEngine.CrashReportHandler",
"UnityEngine.Diagnostics",
"UnityEngine.Events",
"UnityEngine.EventSystems",
// "UnityEngine.iOS",
"UnityEngine.Jobs",
// "UnityEngine.Lumin",
// "UnityEngine.Networking",
// "UnityEngine.Networking.Match",
// "UnityEngine.Networking.PlayerConnection",
// "UnityEngine.Networking.Types",
// "UnityEngine.Playables",
// "UnityEngine.Profiling",
// "UnityEngine.Profiling.Memory.Experimental",
// "UnityEngine.Rendering",
"UnityEngine.SceneManagement",
"UnityEngine.Scripting",
"UnityEngine.Scripting.APIUpdating",
// "UnityEngine.Serialization",
// "UnityEngine.SocialPlatforms",
// "UnityEngine.SocialPlatforms.GameCenter",
// "UnityEngine.SocialPlatforms.Impl",
"UnityEngine.Sprites",
// "UnityEngine.TestTools",
// "UnityEngine.TestTools.Constraints",
// "UnityEngine.TestTools.Utils",
"UnityEngine.TextCore",
// "UnityEngine.TextCore.LowLevel",
// "UnityEngine.Tilemaps",
// "UnityEngine.Timeline",
// "UnityEngine.tvOS",
// "UnityEngine.U2D",
"UnityEngine.UI",
// "UnityEngine.UIElements",
// "UnityEngine.Video",
// "UnityEngine.VR",
// "UnityEngine.Windows",
// "UnityEngine.Windows.Speech",
// "UnityEngine.Windows.WebCam",
// "UnityEngine.WSA",
// "UnityEngine.XR",
// "UnityEngine.XR.Provider",
// "UnityEngine.XR.WSA",
// "UnityEngine.XR.WSA.Input",
// "UnityEngine.XR.WSA.Persistence",
// "UnityEngine.XR.WSA.Sharing",
           };
        // var l = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
        //          where !(assembly.ManifestModule is System.Reflection.Emit.ModuleBuilder)
        //          from type in assembly.GetExportedTypes()
        //          where type.Namespace != null && type.Namespace.StartsWith("UnityEngine.")
        //           && !type.Namespace.StartsWith("UnityEngine.Experimental") && !type.Namespace.StartsWith("UnityEngine.Internal")
        //          select type.Namespace).Distinct().OrderBy(_ => _);
        // var content = string.Join("\",\n\"", l);
        // Debug.Log("\"UnityEngin\",\n\"" + content + "\"\n");

        var types = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                     where !(assembly.ManifestModule is System.Reflection.Emit.ModuleBuilder)
                     from type in assembly.GetExportedTypes()
                     where type.Namespace != null && namespaces.Contains(type.Namespace) && type.BaseType != typeof(MulticastDelegate) && !type.IsInterface && !type.IsEnum && !isObsolete(type)
                     orderby type.FullName
                     select type);

        GenTypesFile("GenUnityTypes", types);
    }

    [MenuItem("XLua/Generate UniRx Types", false, 4)]
    public static void LuaCallCSharpUniRx()
    {
        string[] customAssemblys = new string[] {
               "UniRx",
           };
        var types = (from assembly in customAssemblys.Select(s => Assembly.Load(s))
                     from type in assembly.GetExportedTypes()
                     where type.BaseType != typeof(MulticastDelegate) && !type.IsInterface && !type.IsEnum
                     orderby type.FullName
                     select type);

        GenTypesFile("GenUniRxTypes", types);
    }

    [MenuItem("XLua/Generate Custom Types", false, 5)]
    public static void LuaCallCSharpCustom()
    {
        string[] customAssemblys = new string[] {
               "Assembly-CSharp",
           };
        var types = (from assembly in customAssemblys.Select(s => Assembly.Load(s))
                     from type in assembly.GetExportedTypes()
                     where (type.Namespace == null || !type.Namespace.StartsWith("XLua"))
                             && type.BaseType != typeof(MulticastDelegate) && !type.IsInterface && !type.IsEnum
                     orderby type.FullName
                     select type);

        GenTypesFile("GenCustomTypes", types);
    }

    //自动把LuaCallCSharp涉及到的delegate加到CSharpCallLua列表，后续可以直接用lua函数做callback
    public static List<Type> CSharpCallLua(IEnumerable<Type> LuaCallCSharp)
    {
        var lua_call_csharp = LuaCallCSharp;
        var delegate_types = new List<Type>();
        var flag = BindingFlags.Public | BindingFlags.Instance
            | BindingFlags.Static | BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly;
        foreach (var field in (from type in lua_call_csharp select type).SelectMany(type => type.GetFields(flag)))
        {
            if (typeof(Delegate).IsAssignableFrom(field.FieldType))
            {
                delegate_types.Add(field.FieldType);
            }
        }

        foreach (var method in (from type in lua_call_csharp select type).SelectMany(type => type.GetMethods(flag)))
        {
            if (typeof(Delegate).IsAssignableFrom(method.ReturnType))
            {
                delegate_types.Add(method.ReturnType);
            }
            foreach (var param in method.GetParameters())
            {
                var paramType = param.ParameterType.IsByRef ? param.ParameterType.GetElementType() : param.ParameterType;
                if (typeof(Delegate).IsAssignableFrom(paramType))
                {
                    delegate_types.Add(paramType);
                }
            }
        }
        return delegate_types.Where(t => t.BaseType == typeof(MulticastDelegate) && !hasGenericParameter(t) && !delegateHasEditorRef(t)).Distinct().ToList();
    }

    private static void GenTypesFile(string fileName, IEnumerable<Type> typeList)
    {
        Type[] validTypes = new Type[0];
        var types = Type.GetType(fileName);
        if (types != null)
        {
            var f = types.GetField("List", BindingFlags.NonPublic | BindingFlags.Static);
            validTypes = (Type[])f.GetValue(null);
        }

        List<string> lines = new List<string>();
        foreach (var type in typeList)
        {
            var line = "typeof(" + GetTypeLiteral(type) + "),";
            if (!validTypes.Contains(type))
            {
                line = "// " + line;
            }
            lines.Add(line);
        }

        string header = @"// version: 1
using System.Collections.Generic;
using System;
public static class " + fileName + @"
{
    [XLua.LuaCallCSharp] public static IEnumerable<Type> LuaCallCSharp { get { return List; } }
    [XLua.CSharpCallLua] public static List<Type> CSharpCallLua { get { return GenConfig.CSharpCallLua(List); } }
    private static System.Type[] List = {
";
        string end = @"
    };
}";
        lines.Insert(0, header);
        lines.Add(end);

        File.WriteAllLines(Application.dataPath + "/Scripts/Lua/Editor/" + fileName + ".cs", lines);
        Debug.Log("GenTypesFile: " + fileName + " completed.");
    }

    private static string GetTypeName(string typeName)
    {
        string[] parts = typeName.Split('`');
        var literal = parts[0];//.Replace("&", "");
        if (parts.Length > 1)
        {
            int count = 0;
            if(!int.TryParse(parts[1], out count))
            {
                throw new System.Exception("GetTypeName error!");
            }
            literal += "<" + new String(',', count - 1) + ">";
        }
        return literal;
    }

    private static string GetTypeLiteral(Type type)
    {
        string[] typeNames = type.FullName.Split('+');
        List<string> literal = new List<string>();
        foreach(var t in typeNames)
        {
            literal.Add(GetTypeName(t));
        }
        return string.Join(".", literal);
    }

    private static bool isObsolete(Type type)
    {
        return type.GetCustomAttribute(typeof(ObsoleteAttribute)) != null;
    }


    ///***************热补丁可以参考这份自动化配置***************/
    //[Hotfix]
    //static IEnumerable<Type> HotfixInject
    //{
    //   get
    //   {
    //       return (from type in Assembly.Load("Assembly-CSharp").GetTypes()
    //               where type.Namespace == null || !type.Namespace.StartsWith("XLua")
    //               select type);
    //   }
    //}
    //--------------begin 热补丁自动化配置-------------------------
    private static bool hasGenericParameter(Type type)
    {
        if (type.IsGenericTypeDefinition) return true;
        if (type.IsGenericParameter) return true;
        if (type.IsByRef || type.IsArray)
        {
            return hasGenericParameter(type.GetElementType());
        }
        if (type.IsGenericType)
        {
            foreach (var typeArg in type.GetGenericArguments())
            {
                if (hasGenericParameter(typeArg))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private static bool typeHasEditorRef(Type type)
    {
        if (type.Namespace != null && (type.Namespace == "UnityEditor" || type.Namespace.StartsWith("UnityEditor.")))
        {
            return true;
        }
        if (type.IsNested)
        {
            return typeHasEditorRef(type.DeclaringType);
        }
        if (type.IsByRef || type.IsArray)
        {
            return typeHasEditorRef(type.GetElementType());
        }
        if (type.IsGenericType)
        {
            foreach (var typeArg in type.GetGenericArguments())
            {
                if (typeHasEditorRef(typeArg))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private static bool delegateHasEditorRef(Type delegateType)
    {
        if (typeHasEditorRef(delegateType)) return true;
        var method = delegateType.GetMethod("Invoke");
        if (method == null)
        {
            return false;
        }
        if (typeHasEditorRef(method.ReturnType)) return true;
        return method.GetParameters().Any(pinfo => typeHasEditorRef(pinfo.ParameterType));
    }

    // // 配置某Assembly下所有涉及到的delegate到CSharpCallLua下，Hotfix下拿不准那些delegate需要适配到lua function可以这么配置
    // [CSharpCallLua]
    // static IEnumerable<Type> AllDelegate
    // {
    //     get
    //     {
    //         BindingFlags flag = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
    //         List<Type> allTypes = new List<Type>();
    //         var allAssemblys = new Assembly[]
    //         {
    //            Assembly.Load("Assembly-CSharp")
    //         };
    //         foreach (var t in (from assembly in allAssemblys from type in assembly.GetTypes() select type))
    //         {
    //             var p = t;
    //             while (p != null)
    //             {
    //                 allTypes.Add(p);
    //                 p = p.BaseType;
    //             }
    //         }
    //         allTypes = allTypes.Distinct().ToList();
    //         var allMethods = from type in allTypes
    //                          from method in type.GetMethods(flag)
    //                          select method;
    //         var returnTypes = from method in allMethods
    //                           select method.ReturnType;
    //         var paramTypes = allMethods.SelectMany(m => m.GetParameters()).Select(pinfo => pinfo.ParameterType.IsByRef ? pinfo.ParameterType.GetElementType() : pinfo.ParameterType);
    //         var fieldTypes = from type in allTypes
    //                          from field in type.GetFields(flag)
    //                          select field.FieldType;
    //         return (returnTypes.Concat(paramTypes).Concat(fieldTypes)).Where(t => t.BaseType == typeof(MulticastDelegate) && !hasGenericParameter(t) && !delegateHasEditorRef(t)).Distinct();
    //     }
    // }
    //--------------end 热补丁自动化配置-------------------------

    //黑名单
    [BlackList]
    public static List<List<string>> BlackList = new List<List<string>>()  {
                new List<string>(){"System.Xml.XmlNodeList", "ItemOf"},
                new List<string>(){"UnityEngine.WWW", "movie"},
    #if UNITY_WEBGL
                new List<string>(){"UnityEngine.WWW", "threadPriority"},
    #endif
                new List<string>(){"UnityEngine.Texture2D", "alphaIsTransparency"},
                new List<string>(){"UnityEngine.Security", "GetChainOfTrustValue"},
                new List<string>(){"UnityEngine.CanvasRenderer", "onRequestRebuild"},
                new List<string>(){"UnityEngine.Light", "areaSize"},
                new List<string>(){"UnityEngine.Light", "lightmapBakeType"},
                new List<string>(){"UnityEngine.WWW", "MovieTexture"},
                new List<string>(){"UnityEngine.WWW", "GetMovieTexture"},
                new List<string>(){"UnityEngine.AnimatorOverrideController", "PerformOverrideClipListCleanup"},
    #if !UNITY_WEBPLAYER
                new List<string>(){"UnityEngine.Application", "ExternalEval"},
    #endif
                new List<string>(){"UnityEngine.GameObject", "networkView"}, //4.6.2 not support
                new List<string>(){"UnityEngine.Component", "networkView"},  //4.6.2 not support
                new List<string>(){"System.IO.FileInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
                new List<string>(){"System.IO.FileInfo", "SetAccessControl", "System.Security.AccessControl.FileSecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
                new List<string>(){"System.IO.DirectoryInfo", "SetAccessControl", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "CreateSubdirectory", "System.String", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "Create", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"UnityEngine.MonoBehaviour", "runInEditMode"},
            };

#if UNITY_2018_1_OR_NEWER
    [BlackList]
    public static Func<MemberInfo, bool> MethodFilter = (memberInfo) =>
    {
        if (memberInfo.DeclaringType.IsGenericType && memberInfo.DeclaringType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
        {
            if (memberInfo.MemberType == MemberTypes.Constructor)
            {
                ConstructorInfo constructorInfo = memberInfo as ConstructorInfo;
                var parameterInfos = constructorInfo.GetParameters();
                if (parameterInfos.Length > 0)
                {
                    if (typeof(System.Collections.IEnumerable).IsAssignableFrom(parameterInfos[0].ParameterType))
                    {
                        return true;
                    }
                }
            }
            else if (memberInfo.MemberType == MemberTypes.Method)
            {
                var methodInfo = memberInfo as MethodInfo;
                if (methodInfo.Name == "TryAdd" || methodInfo.Name == "Remove" && methodInfo.GetParameters().Length == 2)
                {
                    return true;
                }
            }
        }
        return false;
    };
#endif
}
