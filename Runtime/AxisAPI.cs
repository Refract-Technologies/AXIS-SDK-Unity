using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


namespace Refract
{
    namespace AXIS
    {
        public static class AxisAPI
        {

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SetConfig(in AxisSDKConfig_t config);
            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors Start();


            [DllImport("axis_sdk")]
            public static extern bool IsDongleConnected();

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SearchForAxisHub(AxisHubInfoCallback callback);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors GetHubInfoPageNumber(byte pageNumber, AxisHubInfoCallback callback);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors IsAxisHubConnected(AxisRuntimeErrorCallback callback);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors PairWithHub(string ipAddress, int port, AxisRuntimeErrorCallback callback);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors GetHubStatus(AxisHubStatusCallback callback);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors StartHubStream(AxisRuntimeErrorCallback callback);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors StopHubStream(AxisRuntimeErrorCallback callback);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors ResetHubStream(AxisRuntimeErrorCallback callback);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SetHubLocation(AxisHubLocations location);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SetHipProviderMode(AxisHipProviderModes mode);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SetSteamVRTrackerProfile(in SteamVRTrackerProfile_t trackers);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SetSteamVRUserHeight(float height);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors EnableSteamVR(in AxisSteamVRConfig_t config);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors DisableSteamVR();

            [DllImport("axis_sdk")]
            public static extern void Stop();


            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SendNoPayloadCommand(AxisDongleCommandTypes type);
           
            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SendCalibrationCommand(NodeCalibrationCommandPayload_t.CalibrationCommandTypes type, NodeCalibrationCommandPayload_t.CalibrationCommandPoses pose);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SendRebootCommand(RebootCommandPayload_t.RebootCommandModes mode);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SendSetWifiCommand(byte channel);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SendNodeAssignCommand(in NodesInfo_t nodeinfo);
            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SendNodeInfoCommand(NodeInfoCommandPayload_t.NodeInfoCommandModes mode);
            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SendNodeAssignSingleCommand(in NodeInfo_t nodeinfo);
            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SendSetDevpacketBitflags([MarshalAs(UnmanagedType.I1)] bool accel, [MarshalAs(UnmanagedType.I1)] bool gyro, [MarshalAs(UnmanagedType.I1)] bool mag, [MarshalAs(UnmanagedType.I1)] bool fusion, [MarshalAs(UnmanagedType.I1)] bool linAccel = false);
            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SendSetPowerSettings(AxisNodeConfigPowerParams param, uint value);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SendGetPowerSettings();
            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SendSetOperationMode(AxisNodeOperationModes mode);
            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SendLEDCommand(in LEDCommandPayload_t command);
            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SendBuzzCommand(in BuzzCommandPayload_t command);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SendLEDMACCommand(in LEDMACCommandPayload_t command);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SendBuzzMACCommand(in BuzzMACCommandPayload_t command);

            [DllImport("axis_sdk")]
            public static extern AxisRuntimeErrors SetHubIP(string hubIP,int port);

            [DllImport("axis_sdk")]
            public static extern void TriggerTestAxisPacket(AxisPacketTypes packetType);
            [DllImport("axis_sdk")]
            public static extern void TriggerTestAxisHubInfo(AxisHubInfoCallback callback);
            [DllImport("axis_sdk")]
            public static extern void TriggerTestAxisHubStatus(AxisHubStatusCallback callback);
            [DllImport("axis_sdk")]
            public static extern void TriggerTestDongleConnection(AxisDongleConnectionCallback callback);
        }
    }    
}
