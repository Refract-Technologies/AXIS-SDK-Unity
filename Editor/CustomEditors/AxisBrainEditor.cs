using AOT;
using Axis._Editor.Styling;
using Axis.Elements;
using Axis.Events;
using Refract.AXIS;
using UnityEditor;
using UnityEngine;

namespace Axis._Editor
{
    [CustomEditor(typeof(AxisBrain))]
    public class AxisBrainEditor: Editor
    {
        private bool isPrefabAsset = false;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorUiElementsStyling.InitStyles();

            if((PrefabUtility.GetPrefabAssetType(target) == PrefabAssetType.Regular || PrefabUtility.GetPrefabAssetType(target) == PrefabAssetType.Variant) && PrefabUtility.GetPrefabInstanceStatus(target) == PrefabInstanceStatus.NotAPrefab)
            {
                isPrefabAsset = true;
            } 

            DrawConnectToAxisButton(isPrefabAsset);

            HandleToggleMannequinVisibility();
            HandleToggleNodesRepresentationVisibility();

            SinglePoseCalibration();
            Recalibration();
            ResetStreamingHub();

            PairHub();
            StartStreamingHub();
            StopStreamingHub();
            //TableCalibration();
           // GetHubBattery();

            //Repaint();
            //FindObjectOfType<AxisRuntimeUdpSocketEditor>().DrawConnectToAxisButton();
            //AxisRuntimeUdpSocketEditor.DrawSomething();
        }

        private void Recalibration()
        {
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUIStyle buttonStyle = EditorUiElementsStyling.notActiveButtonStyle;
            string buttonText = "Recalibration";
            string tooltipText = "For your first time calibration, use initial calibrate";
            if (GUILayout.Button(new GUIContent(buttonText, tooltipText), buttonStyle, GUILayout.Width(200f)))
            {
                AxisEvents.OnZeroAll();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
        }

        private void TableCalibration()
        {
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUIStyle buttonStyle = EditorUiElementsStyling.notActiveButtonStyle;
            string buttonText = "Calibrate On Table";
            string tooltipText = "Put on Nodes with LED facing the screen and calibrate";
            if (GUILayout.Button(new GUIContent(buttonText, tooltipText), buttonStyle, GUILayout.Width(200f)))
            {
                AxisEvents.OnCalibration();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
        }

        private void SinglePoseCalibration()
        {
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUIStyle buttonStyle = EditorUiElementsStyling.notActiveButtonStyle;
            string buttonText = "Initial Calibration";
            string tooltipText = "Put on Nodes on body and calibrate";
            if (GUILayout.Button(new GUIContent(buttonText, tooltipText), buttonStyle, GUILayout.Width(200f)))
            {
                AxisEvents.OnSinglePoseCalibration();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
        }
       
       
        private void GetHubBattery()
        {
            AxisBrain _target = (AxisBrain)target;
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUIStyle buttonStyle = EditorUiElementsStyling.notActiveButtonStyle;
            string buttonText = "Get Hub Battery";
            string tooltipText = "Make sure hub is paired";
            if (GUILayout.Button(new GUIContent(buttonText, tooltipText), buttonStyle, GUILayout.Width(200f)))
            {
               
                var result = _target.axisSDK.GetHubStatus();
                if(result == AxisRuntimeErrors.OK)
                {
                    Debug.Log("result of GetHubStatus: " + result);
                }
                else
                {
                    Debug.LogError("Error when calling GetHubStatus: " + result);
                }
                //AxisEvents.OnSinglePoseCalibration();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
        }
        private void PairHub()
        {
            AxisBrain _target = (AxisBrain)target;
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUIStyle buttonStyle = EditorUiElementsStyling.notActiveButtonStyle;
            string buttonText = "Pair Hub";
            string tooltipText = "Put on Nodes on body and calibrate";
            if (GUILayout.Button(new GUIContent(buttonText, tooltipText), buttonStyle, GUILayout.Width(200f)))
            {

                var result = _target.axisSDK.PairWithHub(_target.axisHubIP,_target.axisHubPort);
                if (result == AxisRuntimeErrors.OK)
                { 
                    Debug.Log("Result of Pairing Hub: " + result);
                }
                else
                {
                    Debug.LogError("Error encounter when Pairing Hub: " + result);
                }
                //AxisEvents.OnSinglePoseCalibration();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
        }
        private void StartStreamingHub()
        {
            AxisBrain _target = (AxisBrain)target;
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUIStyle buttonStyle = EditorUiElementsStyling.notActiveButtonStyle;
            string buttonText = "Start Stream";
            string tooltipText = "Start Data stream of connected Axis Hub";
            if (GUILayout.Button(new GUIContent(buttonText, tooltipText), buttonStyle, GUILayout.Width(200f)))
            {
                
                var result = _target.axisSDK.StartHubStream();
                if (result == AxisRuntimeErrors.OK)
                {
                    Debug.Log("result of StartStream: " + result);
                }
                else
                {
                    Debug.LogError("Error encounter when Start Stream: " + result);
                }
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
        }
        private void ResetStreamingHub()
        {
            AxisBrain _target = (AxisBrain)target;
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUIStyle buttonStyle = EditorUiElementsStyling.notActiveButtonStyle;
            string buttonText = "Reset Hub Stream";
            string tooltipText = "Reset Hub Position And Rotation";
            if (GUILayout.Button(new GUIContent(buttonText, tooltipText), buttonStyle, GUILayout.Width(200f)))
            {

                var result = _target.axisSDK.ResetHubStream();
                if (result != AxisRuntimeErrors.OK)
                {
                    Debug.LogError("Result of ResetStream: " + result);
                }
                if (result != AxisRuntimeErrors.OK)
                {
                    Debug.LogError("Error Encounter when ResetStream: " + result);
                }
                //AxisEvents.OnSinglePoseCalibration();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
        }
        private void StopStreamingHub()
        {
            AxisBrain _target = (AxisBrain)target;
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUIStyle buttonStyle = EditorUiElementsStyling.notActiveButtonStyle;
            string buttonText = "Stop Stream";
            string tooltipText = "Stop data Stream of Axis Hub";
            if (GUILayout.Button(new GUIContent(buttonText, tooltipText), buttonStyle, GUILayout.Width(200f)))
            {               
                var result = _target.axisSDK.StopHubStream();
                if(result == AxisRuntimeErrors.OK)
                {
                    Debug.Log("result of StopStream: " + result);
                }
                else
                {
                    Debug.LogError("Error when calling Stop Hub Stream: " + result);
                }
                //AxisEvents.OnSinglePoseCalibration();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
        }
        private void HandleToggleNodesRepresentationVisibility()
        {
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            AxisBrain _target = (AxisBrain)target;
            //bool isNodesRepresentationVisible = _target.isNodesRepresentationVisible;
            SerializedProperty isNodesRepresentationVisibileProperty = serializedObject.FindProperty("isNodesRepresentationVisible");

            GUIStyle buttonStyle = isNodesRepresentationVisibileProperty.boolValue == true ? EditorUiElementsStyling.activeButtonStyle : EditorUiElementsStyling.notActiveButtonStyle;
            string buttonText = isNodesRepresentationVisibileProperty.boolValue == true ? "Hide Nodes" : "Show Nodes";
            string tooltipText = isPrefabAsset == false ? "Show/Hide the mannequin representation" : "Only prefab instance can Show/Hide";
            GUI.enabled = !isPrefabAsset;
            if (GUILayout.Button(new GUIContent(buttonText, tooltipText), buttonStyle, GUILayout.Width(200f)))
            {
                isNodesRepresentationVisibileProperty.boolValue = !isNodesRepresentationVisibileProperty.boolValue;
                _target.axisNodesRepresentation.SetVisibility(isNodesRepresentationVisibileProperty.boolValue);
            }
            GUI.enabled = true;
            serializedObject.ApplyModifiedProperties();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);

        }

        private void HandleToggleMannequinVisibility()
        {
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            AxisBrain _target = (AxisBrain)target;

            SerializedProperty isMannequinVisibibleProperty = serializedObject.FindProperty("isMannequinVisible");

            bool isMannequinVisible = isMannequinVisibibleProperty.boolValue;
            GUIStyle buttonStyle = isMannequinVisible == true ? EditorUiElementsStyling.activeButtonStyle : EditorUiElementsStyling.notActiveButtonStyle;

            string buttonText = isMannequinVisible == true ? "Hide Mannequin" : "Show Mannequin";

            GUI.enabled = !isPrefabAsset;
            string tooltipText = isPrefabAsset == false ? "Show/Hide the mannequin representation" : "Only prefab instance can Show/Hide";
            if (GUILayout.Button(new GUIContent(buttonText, tooltipText), buttonStyle, GUILayout.Width(200f)))
            {
                isMannequinVisibibleProperty.boolValue = !isMannequinVisibibleProperty.boolValue;
                _target.axisMannequin.SetVisibility(isMannequinVisibibleProperty.boolValue);
                
                serializedObject.ApplyModifiedProperties();
                
            }

            GUI.enabled = true;
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
        }

        public void DrawConnectToAxisButton(bool isPrefabAsset)
        {

            AxisBrain _target = (AxisBrain)target;
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            string buttonText = _target.axisSDK.isRunning == false ? "Connect" : "Disconnect";
            string tooltipText = isPrefabAsset == false ? "Connect/Disconnect to Axis on Edit Mode, it will auto connect when on play mode" : "Only prefab instance can connect to Axis";
            GUILayout.FlexibleSpace();
            GUI.enabled = Application.isPlaying == false && isPrefabAsset == false;

            

            if (GUILayout.Button(new GUIContent(buttonText, tooltipText),
                EditorUiElementsStyling.GetButtonStyleFromConnectionStatus(_target.axisSDK.isRunning),
                GUILayout.MaxWidth(EditorUiElementsStyling.connectButtonWidth)))
                {
                    _target.axisSDK.isRunning = !_target.axisSDK.isRunning;
                    if(_target.axisSDK.isRunning) 
                    {
                        _target.axisSDK.StartSDK();
                    }
                    else
                    {
                        _target.axisSDK.StopSDK();
                    }    
                }
            GUI.enabled = true;

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(20);

        }
    }
}

