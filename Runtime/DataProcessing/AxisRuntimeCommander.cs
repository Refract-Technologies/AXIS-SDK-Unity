using AOT;
using Axis.Events;
using Refract.AXIS;
using System;
using UnityEngine;

namespace Axis.Communication.Commander
{    

    [ExecuteAlways]
    public class AxisRuntimeCommander : MonoBehaviour
    {
        private void OnEnable()
        {
            
            AxisEvents.OnSetNodeLedColor += HandleOnSetNodeLedColor;
            AxisEvents.OnSetNodeVibration += HandleOnSetNodeVibration;      
            AxisEvents.OnCalibration += HandleOnReboot;
            AxisEvents.OnZeroAll += HandleOnZeroAll;
            AxisEvents.OnSinglePoseCalibration += HandleOnSingleCalibration;
        }

        private void OnDisable()
        {
            AxisEvents.OnSetNodeLedColor -= HandleOnSetNodeLedColor;
            AxisEvents.OnSetNodeVibration -= HandleOnSetNodeVibration;
            AxisEvents.OnCalibration -= HandleOnReboot;
            AxisEvents.OnZeroAll -= HandleOnZeroAll;
            AxisEvents.OnSinglePoseCalibration -= HandleOnSingleCalibration;
        }

        private void HandleOnReboot()
        {           
            AxisRuntimeErrors result = AxisAPI.SendCalibrationCommand(NodeCalibrationCommandPayload_t.CalibrationCommandTypes.CalibrateFrame,NodeCalibrationCommandPayload_t.CalibrationCommandPoses.TableFloor);
            if (result != AxisRuntimeErrors.OK)
            {
                Debug.LogError("Table Calibration Error Encountered: " + result);
            }
        }


        private void HandleOnZeroAll()
        {           
            AxisRuntimeErrors result = AxisAPI.SendCalibrationCommand(NodeCalibrationCommandPayload_t.CalibrationCommandTypes.Zero, NodeCalibrationCommandPayload_t.CalibrationCommandPoses.Body);
            if(result != AxisRuntimeErrors.OK) 
            {
                Debug.LogError("Zero All command Error Encountered: " + result);
            }
        }
        private void HandleOnSetNodeVibration(int nodeIndex, float intensity, float durationSeconds)
        {
            BuzzCommandPayload_t cmd = new BuzzCommandPayload_t();          
            cmd.nodeIndex = (AxisNodePositions)nodeIndex;
            cmd.intensity = GetByteFromNormalizedFloat(intensity);
            cmd.duration = GetByteFromNormalizedFloat(durationSeconds / 25.5f);

            AxisRuntimeErrors result = AxisAPI.SendBuzzCommand(cmd);
            if(result != AxisRuntimeErrors.OK) 
            {
                Debug.LogError("Node Vibration command Error Encountered: " + result);
            }
        }

        private static byte GetByteFromNormalizedFloat(float normalizedFloat)
        {
            return BitConverter.GetBytes(Mathf.RoundToInt(normalizedFloat * 255))[0];
        }

        private void HandleOnSetNodeLedColor(int nodeIndex, Color32 color, float brightness)
        {
            brightness = brightness > 1f ? 1f/3f : brightness/3f;

            LEDCommandPayload_t cmd = new LEDCommandPayload_t();
            cmd.brightness = GetByteFromNormalizedFloat(brightness);

            cmd.red = color.r;
            cmd.green = color.g;
            cmd.blue = color.b;
            cmd.nodeIndex = (AxisNodePositions)nodeIndex;
            AxisRuntimeErrors result = AxisAPI.SendLEDCommand(cmd);
            if (result != AxisRuntimeErrors.OK)
            {
                Debug.LogError("Set Node command Error Encountered: " + result);
            }
        }
        private void HandleOnSingleCalibration()
        {
            AxisRuntimeErrors result = AxisAPI.SendCalibrationCommand(NodeCalibrationCommandPayload_t.CalibrationCommandTypes.All, NodeCalibrationCommandPayload_t.CalibrationCommandPoses.Body);
            if (result != AxisRuntimeErrors.OK)
            {
                Debug.LogError("Single Pose command Error Encountered: " + result);
            }
        }
    }
}

