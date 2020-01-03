// version: 1
using System.Collections.Generic;
using System;
public static class GenCustomTypes
{
    [XLua.LuaCallCSharp] public static IEnumerable<Type> LuaCallCSharp { get { return List; } }
    [XLua.CSharpCallLua] public static List<Type> CSharpCallLua { get { return GenConfig.CSharpCallLua(List); } }
    private static System.Type[] List = {

// typeof(BindAnchor),
// typeof(ExampleGenConfig),
// typeof(LuaBehaviour),
// typeof(LuaBehaviour.BindData),
// typeof(LuaCallCs),
// typeof(LuaCore),
// typeof(Tutorial.BaseClass),
// typeof(Tutorial.ByFile),
// typeof(Tutorial.ByString),
// typeof(Tutorial.CSCallLua),
// typeof(Tutorial.CSCallLua.DClass),
// typeof(Tutorial.CustomLoader),
// typeof(Tutorial.DerivedClass),
// typeof(Tutorial.DerivedClassExtensions),
// typeof(Tutorial.Param1),

    };
}
