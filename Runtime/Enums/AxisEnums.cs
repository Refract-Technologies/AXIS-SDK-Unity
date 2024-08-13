using System;

namespace Refract 
{
    namespace AXIS
    {
        public enum AxisRuntimeErrors : UInt16 
        {
            OK,
            PacketCallbackNotSet,
            InvalidDongleCommand,
            HubTimeout,
            InvalidIPAddress,
            InvalidParameter,
            NoLANAdapter,
            HTTPError,
            HTTPException,
            SocketError,
            SocketException,
            PreviousOpInProgress,
            DongleNotConnected,
            SerialPortError,
            InternalError,
            SDKNotStarted,
            NoSubnet,
            RUNTIME_ERROR_COUNT,
        }

        public enum AxisPacketTypes : UInt16 
        {
            IMUData,
            VisibleNodes,
            PairedNodes,
            SetWifiResponse,
            ReportWifi,
            ReportBatt,
            DoubleClick,
            Versions,
            SuitInfo,
            NodeAssignAck,
            DevPacket,
            NodeConfigResponse,
            SkeletonData,
            PACKET_TYPES_COUNT,
        }

        public enum AxisDongleCommandTypes : UInt16 
        {
            Shutdown,
            GetWifi,
            GetBatt,
            GetVersions,
            Suitinfo,
            Reboot,
            SetWifi,
            NodeAssign,
            NodeInfo,
            NodeAssignSingle,
            NodeConfigSet,
            Calibration,
            NodeOperationMode,
            Buzz,
            LED,
            BuzzMAC,
            LEDMAC,
            NodeConfigGet,
            DONGLE_COMMAND_TYPES_COUNT,
        }

        public enum AxisNodePositions : UInt16 
        {
            RightThigh,
            RightCalf,
            LeftThigh,
            LeftCalf,
            RightUpperArm,
            RightForearm,
            LeftUpperArm,
            LeftForearm,
            Spine,
            RightFoot,
            LeftFoot,
            RightHand,
            LeftHand,
            RightShoulder,
            LeftShoulder,
            Head,
            Hip,
            Unassigned,
            NODE_INDEX_COUNT,
        }

        public enum AxisNodeOperationModes : UInt16 
        {
            Unsupported,
            Fusion,
            FusionNoMag,
            OPERATION_MODE_COUNT,
        }

        public enum AxisHubLocations : UInt16 
        {
            HubLocationBack,
            HubLocationFront,
            AXIS_HUB_LOCATION_COUNT,
        }

        public enum AxisHipProviderModes : UInt16 
        {
            HipProviderNone,
            HipProviderNode,
            HipProviderHub,
            AXIS_HIP_PROVIDER_MODES_COUNT,
        }

        public enum AxisNodeConfigCategories : UInt16 
        {
            DEV,
            Power,
            AXIS_NODE_CONFIG_CATEGORIES_COUNT,
        }

        public enum AxisNodeConfigDevParams : UInt16 
        {
            DevPacketBitflags,
            AXIS_NODE_CONFIG_DEV_PARAMETERS_COUNT,
        }

        public enum AxisNodeConfigPowerParams : UInt16 
        {
            TransmissionPower,
            TransmissionFrequency,
            AXIS_NODE_CONFIG_POWER_PARAMETERS_COUNT,
        }

        public enum AxisVmcBoneConfigBones : UInt16 
        {
            VMCHips,
            VMCLeftUpperLeg,
            VMCRightUpperLeg,
            VMCLeftLowerLeg,
            VMCRightLowerLeg,
            VMCLeftFoot,
            VMCRightFoot,
            VMCSpine,
            VMCChest,
            VMCUpperChest,
            VMCNeck,
            VMCHead,
            VMCLeftShoulder,
            VMCRightShoulder,
            VMCLeftUpperArm,
            VMCRightUpperArm,
            VMCLeftLowerArm,
            VMCRightLowerArm,
            VMCLeftHand,
            VMCRightHand,
            VMC_CONFIG_COUNT,
        }

        public enum AxisSkeletonJoints : UInt16 
        {
            SKHMD,
            SKHead,
            SKNeck,
            SKUpperSpine,
            SKMiddleSpine,
            SKLowerSpine,
            SKHip,
            SKRightHip,
            SKRightKnee,
            SKRightAnkle,
            SKRightFoot,
            SKLeftHip,
            SKLeftKnee,
            SKLeftAnkle,
            SKLeftFoot,
            SKRightScapula,
            SKRightShoulder,
            SKRightElbow,
            SKRightWrist,
            SKRightHand,
            SKLeftScapula,
            SKLeftShoulder,
            SKLeftElbow,
            SKLeftWrist,
            SKLeftHand,
            SK_JOINT_COUNT,
        }

    } 
} 

