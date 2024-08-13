using System;

namespace Axis.Events
{
    using System.Collections.Generic;
    using Axis.DataTypes;
    using Axis.Elements;
    using Axis.Utils;
    using Enumerations;
    using Refract.AXIS;
    using UnityEngine;



    public static class AxisEvents
    {

        //This Action is raised whenever there is new data from the Axis System
        //Check AxisOutputData to understand how the data is organized.
        public static Action<AxisOutputData> OnAxisOutputDataUpdated;
        public static Action<string, Dictionary<NodeBinding, AxisNode>> OnNodeByLimbsUpdated;

        #region Commands To Nodes


        //Example OnSetNodeVibration.Invoke(nodeIndex, intensity, duration) where:
        //nodeIndex: Check Enumerations.cs for relation of nodeIndex and default positions
        //intensity -> from 0 to 1
        //duration -> in seconds
        public static Action<int, float, float> OnSetNodeVibration;

        //Example OnSetNodeLedColor.Invoke(nodeIndex, Color.red, brightness) where:
        //nodeIndex: Check Enumerations.cs for relation of nodeIndex and default positions
        //Color32 -> auto conversion from Color
        //brightness -> from 0 to 1
        public static Action<int, Color32, float> OnSetNodeLedColor;

        //call this event to do T-Pose Calibration
        public static Action OnZeroAll;
        //call this event to do Table Calibration
        public static Action OnCalibration;
        //call this event to do Single Pose Calibration
        public static Action OnSinglePoseCalibration;

        //this event will be fired whenever there is a state change of dongle
        //such as dongle connected/disconnected
        public static Action<bool> OnDongleConnected;

        //this event will be fired whenever there is a Axis Error Occurred
        //AxisRuntimeErrors - Error type that is received
        //int - Error Code that is receive
        //string - description of the error received
        public static Action<AxisRuntimeErrors, int, string> OnAxisRuntimeErrorReceived;
        //this event will be fired whenever there is a Axis Hub is Paired
        //AxisRuntimeErrors - Error type that is received
        //int - Error Code that is receive
        //string - description of the error received
        public static Action<AxisRuntimeErrors, int, string> OnAxisHubPairingReceived;
        //this event will be fired whenever there is a Axis Hub is Start Streaming
        //AxisRuntimeErrors - Error type that is received
        //int - Error Code that is receive
        //string - description of the error received
        public static Action<AxisRuntimeErrors, int, string> OnAxisHubStartStreamReceived;
        //this event will be fired whenever there is a Axis Hub is Stop Streaming
        //AxisRuntimeErrors - Error type that is received
        //int - Error Code that is receive
        //string - description of the error received
        public static Action<AxisRuntimeErrors, int, string> OnAxisHubStopStreamReceived;
        //this event will be fired whenever there is a Axis Hub is ResetStream
        //AxisRuntimeErrors - Error type that is received
        //int - Error Code that is receive
        //string - description of the error received
        public static Action<AxisRuntimeErrors, int, string> OnAxisHubResetStreamReceived;
        //this event will be fired after calling AxisHub Search through AxisSDK class
        //AxisHubsInfo - information that of Axis hub that have been found 
        public static Action<AxisHubsInfo_t> OnAxisHubSearchingCallback;
        //this event will be fired after calling GetHubInfoPageNumber through AxisSDK class
        //AxisHubsInfo - information that of Axis hub that have been found 
        public static Action<AxisHubsInfo_t> OnGetHubInfoPageNumberCallback;
        //this event will be fired after calling AxisHubGetStatus through AxisSDK class
        //AxisHubStatus_t - information that of Axis hub that have been found 
        public static Action<AxisHubStatus_t> OnAxisHubGetStatusCallback;
        #endregion

    }
}
