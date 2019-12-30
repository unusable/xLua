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

    private void Bind(XLua.LuaTable table)
    {
        foreach(var b in this.anchors)
        {
            table.Set(b.key, b.value);
        }
    }

    public static void Bind(GameObject targetObject, XLua.LuaTable table)
    {
        LuaBehaviour behaviour = targetObject.GetComponent<LuaBehaviour>();
        behaviour.Bind(table);
    }

}
