using UnityEngine;

[DisallowMultipleComponent]
public class LuaBehaviour : MonoBehaviour
{
    [System.Serializable]
    public class BindData
    {
        public string key;
        public Component value;
    }

    [SerializeField]
    private BindData[] anchors;

    [XLua.BlackList]
    public BindData[] Anchors { get => this.anchors; }

    public XLua.LuaTable Bind(XLua.LuaEnv env)
    {
        var table = env.NewTable();
        table.Set("gameObject", this.gameObject);
        table.Set("transform", this.transform);

        var binder = env.NewTable();
        foreach(var b in this.anchors)
        {
            binder.Set(b.key, b.value);
        }
        table.Set("binder", binder);
        return table;
    }


}
