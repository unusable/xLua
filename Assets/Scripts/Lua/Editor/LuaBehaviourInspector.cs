using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LuaBehaviour), true)]
public class LuaBehaviourInspector : Editor
{
    public override void OnInspectorGUI()
    {
        LuaBehaviour targetObject = this.serializedObject.targetObject as LuaBehaviour;
        LuaBehaviour.BindData[] anchors = targetObject.Anchors;
        if (anchors == null)
        {
            return;
        }

        for (int i = 0; i < anchors.Length; i++)
        {
            LuaBehaviour.BindData anchor = anchors[i];
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(anchor.key, GUILayout.Width(100));
            EditorGUILayout.ObjectField(anchor.value, typeof(Component), false);
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Bind"))
        {
            List<BindAnchor> newAnchors = new List<BindAnchor>();
            this.EnumAnchor(targetObject.transform, newAnchors);
            List<LuaBehaviour.BindData> data = new List<LuaBehaviour.BindData>();
            newAnchors.ForEach(b =>
            {
                data.Add(new LuaBehaviour.BindData()
                {
                    key = b.bindKey,
                    value = b.component
                });
            });

            System.Type type = targetObject.GetType();
            FieldInfo fieldInfo = type.GetField("anchors", (BindingFlags.Instance | BindingFlags.NonPublic));
            fieldInfo.SetValue(targetObject, data.ToArray());
            EditorUtility.SetDirty(targetObject);
        }
        this.serializedObject.UpdateIfRequiredOrScript();
    }

    private void EnumAnchor(Transform node, List<BindAnchor> anchorList)
    {
        for (int i = 0; i < node.childCount; i++)
        {
            Transform child = node.GetChild(i);
            this.EnumAnchor(child, anchorList);
            BindAnchor anchor = child.GetComponent<BindAnchor>();
            if (anchor)
            {
                anchorList.Add(anchor);
            }
        }
    }

}