using UnityEngine;
using XLua;
#if UNITY_EDITOR
using System.Text;
using System.IO;
#endif


public class LuaCore : MonoBehaviour
{
    public string bootstrap;
    private LuaEnv luaEnv;
    private float lastGCTime;
    private const float GCInterval = 1;

    void Awake()
    {
        this.luaEnv = new LuaEnv();
#if UNITY_EDITOR
        this.luaEnv.AddLoader(Loader);
#endif
    }

    void Start()
    {
        this.lastGCTime = Time.time;
        luaEnv.DoString(string.Format("require '{0}'", this.bootstrap));
    }

    void Update()
    {
        if (Time.time - this.lastGCTime > GCInterval)
        {
            this.luaEnv.Tick();
            this.lastGCTime = Time.time;
        }
    }

    void OnDestroy()
    {
        this.luaEnv.Dispose();
        this.luaEnv = null;
    }

#if UNITY_EDITOR
    private static byte[] Loader(ref string filepath)
    {
        StringBuilder sb = new StringBuilder(Application.dataPath);
        sb.Append(Path.DirectorySeparatorChar);
        sb.Append("..");
        sb.Append(Path.DirectorySeparatorChar);
        sb.Append("Lua");
        sb.Append(Path.DirectorySeparatorChar);
        sb.Append(filepath.Replace('.', Path.DirectorySeparatorChar));
        sb.Append(".lua");
        string path = Path.GetFullPath(sb.ToString());
        if (!File.Exists(path))
        {
            return null;
        }

        try
        {
            return File.ReadAllBytes(path);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
            return null;
        }
    }
#endif
}
