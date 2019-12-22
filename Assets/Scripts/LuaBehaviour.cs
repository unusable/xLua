using UnityEngine;
using XLua;


public class LuaBehaviour : MonoBehaviour
{
    public string bootstrap;
    private LuaEnv luaEnv;
    private float lastGCTime;
    private const float GCInterval = 1;

    void Awake()
    {
        this.luaEnv = new LuaEnv();
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
}
