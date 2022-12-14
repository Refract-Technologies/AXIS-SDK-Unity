using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class WindowLayoutLoader : MonoBehaviour
{
    public string windowLayoutPath;
    private void Awake()
    {
        LoadWindowLayout();
    }

    public void LoadWindowLayout()
    {

        // Loading layout from an asset
        //        LayoutUtility.LoadLayoutFromAsset("Assets/Editor/Layouts/Your Layout.wlt");

        //if (string.IsNullOrEmpty(windowLayoutPath))
        //{
        //    bool success = EditorUtility.LoadWindowLayout(windowLayoutPath);
        //
        //}
        //Debug.Log($"Loaded Layout: {success}");

    }
}
