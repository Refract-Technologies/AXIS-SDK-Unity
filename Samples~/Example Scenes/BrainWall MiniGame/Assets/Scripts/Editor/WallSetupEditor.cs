using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AxisExampleScenes.Minigame.BrainWall
{
    [CustomEditor(typeof(WallSetup))]
    public class WallSetupEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            WallSetup wallSetup = (WallSetup)target;

            for (int column = 0; column < wallSetup.disabledWallParts.Count; column++)
            {
                GUILayout.BeginHorizontal();
                for (int row = 0; row < wallSetup.disabledWallParts[column].boolList.Count; row++)
                {
                    bool isWallPartDisabled = wallSetup.disabledWallParts[column].boolList[row];
                    string value = isWallPartDisabled == true ? " " : "X";
                    GUI.backgroundColor = isWallPartDisabled == true ? Color.black : Color.cyan;
                    if (GUILayout.Button(
                        value,
                        GUILayout.Width(40),
                        GUILayout.Height(40)))
                    {
                        wallSetup.disabledWallParts[column].boolList[row] = !wallSetup.disabledWallParts[column].boolList[row];
                    }
                }

                EditorUtility.SetDirty(target);
                serializedObject.ApplyModifiedProperties();
                //
                GUILayout.EndHorizontal();
            }

        }
    }

}
