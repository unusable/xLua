using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(BindAnchor), true)]
public class BindAnchorInspector : Editor
{

    public override void OnInspectorGUI()
    {
        BindAnchor targetObject = this.serializedObject.targetObject as BindAnchor;

        int selected = -1;
        List<string> names = new List<string>();
        var comps = targetObject.GetComponents(typeof(Component));
        for (int i = 0; i < comps.Length; i++)
        {
            var c = comps[i];
            names.Add(c.GetType().Name);
            if (c == targetObject.component)
            {
                selected = i;
            }
        }

        string newField = EditorGUILayout.TextField(targetObject.bindKey);
        if (newField != targetObject.bindKey)
        {
            targetObject.bindKey = newField;
            EditorUtility.SetDirty(targetObject);
        }

        int newSelected = EditorGUILayout.Popup(selected, names.ToArray());
        if (newSelected != selected)
        {
            selected = newSelected;
            targetObject.component = comps[selected];
            EditorUtility.SetDirty(targetObject);
        }

       this.serializedObject.UpdateIfRequiredOrScript();
    }

}