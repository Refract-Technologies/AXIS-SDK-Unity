using Axis.DataTypes;
using System;
using System.Runtime.InteropServices;


namespace Refract
{
    namespace AXIS
    {
        [StructLayout(LayoutKind.Sequential,Pack = 8)]
        public struct Quat_t 
        {
            public float x;
            public float y;
            public float z;
            public float w;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct Vector3_t
        {
            public float x;
            public float y;
            public float z;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct PosQuat_t
        {
            public Quat_t quat;
            public Vector3_t pos;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct NodeIMUData17_t
        {
            public NodeIMUData_t rightThigh;
            public NodeIMUData_t rightCalf;
            public NodeIMUData_t leftThigh;
            public NodeIMUData_t leftCalf;
            public NodeIMUData_t rightUpperArm;
            public NodeIMUData_t rightForearm;
            public NodeIMUData_t leftUpperArm;
            public NodeIMUData_t leftForearm;
            public NodeIMUData_t spine;
            public NodeIMUData_t rightFoot;
            public NodeIMUData_t leftFoot;
            public NodeIMUData_t rightHand;
            public NodeIMUData_t leftHand;
            public NodeIMUData_t rightShoulder;
            public NodeIMUData_t leftShoulder;
            public NodeIMUData_t head;
            public NodeIMUData_t hip;

            public NodeIMUData_t this[AxisNodePositions i]
            {
                get
                {
                    switch (i)
                    {
                        case AxisNodePositions.RightThigh:
                            return rightThigh;
                        case AxisNodePositions.RightCalf:
                            return rightCalf;
                        case AxisNodePositions.LeftThigh:
                            return leftThigh;
                        case AxisNodePositions.LeftCalf:
                            return leftCalf;
                        case AxisNodePositions.RightUpperArm:
                            return rightUpperArm;
                        case AxisNodePositions.RightForearm:
                            return rightForearm;
                        case AxisNodePositions.LeftUpperArm:
                            return leftUpperArm;
                        case AxisNodePositions.LeftForearm:
                            return leftForearm;
                        case AxisNodePositions.Spine:
                            return spine;
                        case AxisNodePositions.RightFoot:
                            return rightFoot;
                        case AxisNodePositions.LeftFoot:
                            return leftFoot;
                        case AxisNodePositions.RightHand:
                            return rightHand;
                        case AxisNodePositions.LeftHand:
                            return leftHand;
                        case AxisNodePositions.RightShoulder:
                            return rightShoulder;
                        case AxisNodePositions.LeftShoulder:
                            return leftShoulder;
                        case AxisNodePositions.Head:
                            return head;
                        case AxisNodePositions.Hip:
                            return hip;
                        case AxisNodePositions.Unassigned:
                        case AxisNodePositions.NODE_INDEX_COUNT:
                        default:
                            throw new Exception("Invalid parameter");
                    }
                }
            }

        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct NodeIMUData_t
        {
            [MarshalAs(UnmanagedType.I1)]
            public bool on;
            public Quat_t quat;
            public Vector3_t linAccel;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct NodeInfo17_t
        {
            public NodeInfo_t node0;
            public NodeInfo_t node1;
            public NodeInfo_t node2;
            public NodeInfo_t node3;
            public NodeInfo_t node4;
            public NodeInfo_t node5;
            public NodeInfo_t node6;
            public NodeInfo_t node7;
            public NodeInfo_t node8;
            public NodeInfo_t node9;
            public NodeInfo_t node10;
            public NodeInfo_t node11;
            public NodeInfo_t node12;
            public NodeInfo_t node13;
            public NodeInfo_t node14;
            public NodeInfo_t node15;
            public NodeInfo_t node16;
            public NodeInfo_t this[ushort i]
            {
                get
                {
                    switch (i)
                    {
                        case 0:
                            return node0;
                        case 1:
                            return node1;
                        case 2:
                            return node2;
                        case 3:
                            return node3;
                        case 4:
                            return node4;
                        case 5:
                            return node5;
                        case 6:
                            return node6;
                        case 7:
                            return node7;
                        case 8:
                            return node8;
                        case 9:
                            return node9;
                        case 10:
                            return node10;
                        case 11:
                            return node11;
                        case 12:
                            return node12;
                        case 13:
                            return node13;
                        case 14:
                            return node14;
                        case 15:
                            return node15;
                        case 16:
                            return node16;
                        default:
                            throw new Exception("Invalid parameter");
                    }
                }
                set
                {
                    switch (i)
                    {
                        case 0:
                            node0 = value;
                            break;
                        case 1:
                            node1 = value;
                            break;
                        case 2:
                            node2 = value;
                            break;
                        case 3:
                            node3 = value;
                            break;
                        case 4:
                            node4 = value;
                            break;
                        case 5:
                            node5 = value;
                            break;
                        case 6:
                            node6 = value;
                            break;
                        case 7:
                            node7 = value;
                            break;
                        case 8:
                            node8 = value;
                            break;
                        case 9:
                            node9 = value;
                            break;
                        case 10:
                            node10 = value;
                            break;
                        case 11:
                            node11 = value;
                            break;
                        case 12:
                            node12 = value;
                            break;
                        case 13:
                            node13 = value;
                            break;
                        case 14:
                            node14 = value;
                            break;
                        case 15:
                            node15 = value;
                            break;
                        case 16:
                            node16 = value;
                            break;

                        default:
                            throw new Exception("Invalid parameter");
                    }
                }
            }
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct NodeInfo_t
        {
            public AxisNodePositions nodeIndex;
            [MarshalAs(UnmanagedType.I1)]
            public bool on;
            public MacAddresses mac;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct WifiRes_t : IAxisData
        {
            public byte wifiChannel;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct SetWifiRes_t : IAxisData
        {
            public byte nodeCount;
            [MarshalAs(UnmanagedType.I1)]
            public bool success;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct IMUData_t : IAxisData
        {
            public NodeIMUData17_t nodeImuData;
            public PosQuat_t hubData;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct DoubleClickNodesInfo_t : IAxisData
        {
            public byte nodeCount;

            public NodeInfo17_t nodeInfo;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct VisibleNodesInfo_t : IAxisData
        {
            public byte nodeCount;

            public NodeInfo17_t nodeInfo;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct PairedNodesInfo_t : IAxisData
        {
            public byte nodeCount;

            public NodeInfo17_t nodeInfo;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct NodesInfo_t : IAxisData
        {
            public byte nodeCount;           
            
            public NodeInfo17_t nodeInfo;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct NodeBattInfo17_t
        {
            public NodeBattInfo_t node0;
            public NodeBattInfo_t node1;
            public NodeBattInfo_t node2;
            public NodeBattInfo_t node3;
            public NodeBattInfo_t node4;
            public NodeBattInfo_t node5;
            public NodeBattInfo_t node6;
            public NodeBattInfo_t node7;
            public NodeBattInfo_t node8;
            public NodeBattInfo_t node9;
            public NodeBattInfo_t node10;
            public NodeBattInfo_t node11;
            public NodeBattInfo_t node12;
            public NodeBattInfo_t node13;
            public NodeBattInfo_t node14;
            public NodeBattInfo_t node15;
            public NodeBattInfo_t node16;

            public NodeBattInfo_t this[ushort i]
            {
                get
                {
                    switch (i)
                    {
                        case 0:
                            return node0;
                        case 1:
                            return node1;
                        case 2:
                            return node2;
                        case 3:
                            return node3;
                        case 4:
                            return node4;
                        case 5:
                            return node5;
                        case 6:
                            return node6;
                        case 7:
                            return node7;
                        case 8:
                            return node8;
                        case 9:
                            return node9;
                        case 10:
                            return node10;
                        case 11:
                            return node11;
                        case 12:
                            return node12;
                        case 13:
                            return node13;
                        case 14:
                            return node14;
                        case 15:
                            return node15;
                        case 16:
                            return node16;
                        default:
                            throw new Exception("Invalid parameter");
                    }
                }
                set
                {
                    switch (i)
                    {
                        case 0:
                            node0 = value;
                            break;
                        case 1:
                            node1 = value;
                            break;
                        case 2:
                            node2 = value;
                            break;
                        case 3:
                            node3 = value;
                            break;
                        case 4:
                            node4 = value;
                            break;
                        case 5:
                            node5 = value;
                            break;
                        case 6:
                            node6 = value;
                            break;
                        case 7:
                            node7 = value;
                            break;
                        case 8:
                            node8 = value;
                            break;
                        case 9:
                            node9 = value;
                            break;
                        case 10:
                            node10 = value;
                            break;
                        case 11:
                            node11 = value;
                            break;
                        case 12:
                            node12 = value;
                            break;
                        case 13:
                            node13 = value;
                            break;
                        case 14:
                            node14 = value;
                            break;
                        case 15:
                            node15 = value;
                            break;
                        case 16:
                            node16 = value;
                            break;

                        default:
                            throw new Exception("Invalid parameter");
                    }
                }
            }
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct NodeBattInfo_t
        {
            public AxisNodePositions nodeIndex;
            public byte batteryLevel;
            public float voltageLevel;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct BattInfo_t : IAxisData
        {
            public byte nodeCount;
            public NodeBattInfo17_t nodeBattInfo;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct Version_t
        {
            public byte major;
            public byte minor;
            public byte patch;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct DongleVersion_t
        {
            public Version_t hardware;
            public Version_t firmware;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct NodeVersion17_t
        {
            public NodeVersion_t node0;
            public NodeVersion_t node1;
            public NodeVersion_t node2;
            public NodeVersion_t node3;
            public NodeVersion_t node4;
            public NodeVersion_t node5;
            public NodeVersion_t node6;
            public NodeVersion_t node7;
            public NodeVersion_t node8;
            public NodeVersion_t node9;
            public NodeVersion_t node10;
            public NodeVersion_t node11;
            public NodeVersion_t node12;
            public NodeVersion_t node13;
            public NodeVersion_t node14;
            public NodeVersion_t node15;
            public NodeVersion_t node16;

            public NodeVersion_t this[ushort i] 
            {
                get
                {
                    switch (i)
                    {
                        case 0:
                            return node0;
                        case 1:
                            return node1;
                        case 2:
                            return node2;
                        case 3:
                            return node3;
                        case 4:
                            return node4;
                        case 5:
                            return node5;
                        case 6:
                            return node6;
                        case 7:
                            return node7;
                        case 8:
                            return node8;
                        case 9:
                            return node9;
                        case 10:
                            return node10;
                        case 11:
                            return node11;
                        case 12:
                            return node12;
                        case 13:
                            return node13;
                        case 14:
                            return node14;
                        case 15:
                            return node15;
                        case 16:
                            return node16;
                        default:
                            throw new Exception("Invalid parameter");
                    }
                }
            }

        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct NodeVersion_t
        {
            public AxisNodePositions nodeIndex;
            public Version_t hardware;
            public Version_t firmware;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct VersionsInfo_t : IAxisData
        {
            public DongleVersion_t dongleVersion;
            public byte nodeCount;
            public NodeVersion17_t nodeVersions;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct NodeSuitInfo17_t
        {
            public NodeSuitInfo_t node0;
            public NodeSuitInfo_t node1;
            public NodeSuitInfo_t node2;
            public NodeSuitInfo_t node3;
            public NodeSuitInfo_t node4;
            public NodeSuitInfo_t node5;
            public NodeSuitInfo_t node6;
            public NodeSuitInfo_t node7;
            public NodeSuitInfo_t node8;
            public NodeSuitInfo_t node9;
            public NodeSuitInfo_t node10;
            public NodeSuitInfo_t node11;
            public NodeSuitInfo_t node12;
            public NodeSuitInfo_t node13;
            public NodeSuitInfo_t node14;
            public NodeSuitInfo_t node15;
            public NodeSuitInfo_t node16;

            public NodeSuitInfo_t this[ushort i] 
            {
                get
                {
                    switch (i)
                    {
                        case 0:
                            return node0;
                        case 1:
                            return node1;
                        case 2:
                            return node2;
                        case 3:
                            return node3;
                        case 4:
                            return node4;
                        case 5:
                            return node5;
                        case 6:
                            return node6;
                        case 7:
                            return node7;
                        case 8:
                            return node8;
                        case 9:
                            return node9;
                        case 10:
                            return node10;
                        case 11:
                            return node11;
                        case 12:
                            return node12;
                        case 13:
                            return node13;
                        case 14:
                            return node14;
                        case 15:
                            return node15;
                        case 16:
                            return node16;
                        default:
                            throw new Exception("Invalid parameter");
                    }
                }
            }
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct NodeSuitInfo_t
        {
            public AxisNodePositions nodeIndex;
            public MacAddresses mac;
            public Version_t firmware;
            public AxisNodeOperationModes operationMode;
            public byte batteryPercent;
            [MarshalAs(UnmanagedType.I1)]
            public bool on;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct SuitInfo_t : IAxisData
        {
            public byte nodeCount;

            public NodeSuitInfo17_t nodeSuitInfo;
            public MacAddresses dongleMAC;
            public DongleVersion_t dongleVersion;
            public byte homeWifiChannel;
            public byte currentWifiChannel;
        }
        [StructLayout(LayoutKind.Sequential,Pack = 8)]
        public struct MacAddresses
        {
            public byte mac0, mac1, mac2,mac3,mac4,mac5;
            public byte this[int i]
            {
                get 
                {
                    switch (i)
                    {
                        case 0:
                            return mac0;
                        case 1:
                            return mac1;
                        case 2:
                            return mac2;
                        case 3:
                            return mac3;
                        case 4:
                            return mac4;
                        case 5:
                            return mac5;
                        default:
                            throw new Exception("Invalid parameter");
                    }
                }
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct NodeConfigPayloadFloat_t
        {
            public float value;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct NodeConfigPayloadUInt32_t
        {
            public uint value;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct DevPacketSensorData_t
        {
            public Vector3_t data;
            public byte acc;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct DevPacketFusionState_t
        {
            public float accelError;
            public float accelRecovTrigger;
            public float magError;
            public float magRecovTrigger;
            [MarshalAs(UnmanagedType.I1)]
            public bool accelIgnored;
            [MarshalAs(UnmanagedType.I1)]
            public bool magIgnored;
            [MarshalAs(UnmanagedType.I1)]
            public bool initializing;
            [MarshalAs(UnmanagedType.I1)]
            public bool angularRateRecov;
            [MarshalAs(UnmanagedType.I1)]
            public bool accelRecov;
            [MarshalAs(UnmanagedType.I1)]
            public bool magRecov;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct SteamVRTrackerProfile_t
        {
            [MarshalAs(UnmanagedType.I1)]
            public bool rightCalf;
            [MarshalAs(UnmanagedType.I1)]
            public bool leftCalf;
            [MarshalAs(UnmanagedType.I1)]
            public bool rightUpperArm;
            [MarshalAs(UnmanagedType.I1)]
            public bool rightForearm;
            [MarshalAs(UnmanagedType.I1)]
            public bool leftUpperArm;
            [MarshalAs(UnmanagedType.I1)]
            public bool leftForearm;
            [MarshalAs(UnmanagedType.I1)]
            public bool chest;
            [MarshalAs(UnmanagedType.I1)]
            public bool rightFoot;
            [MarshalAs(UnmanagedType.I1)]
            public bool leftFoot;
            [MarshalAs(UnmanagedType.I1)]
            public bool rightWrist;
            [MarshalAs(UnmanagedType.I1)]
            public bool leftWrist;
            [MarshalAs(UnmanagedType.I1)]
            public bool rightShoulder;
            [MarshalAs(UnmanagedType.I1)]
            public bool leftShoulder;
            [MarshalAs(UnmanagedType.I1)]
            public bool hip;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct SteamVRLimbLengths_t
        {
            public float height;
            public float unit;
            public float hmdOffsetZ;
            public float hmdOffsetY;
            public float neck;
            public float fakeNeck;
            public float shoulder;
            public float torso;
            public float spine3;
            public float spine2;
            public float spine1;
            public float hipUp;
            public float hipDown;
            public float hipWidth;
            public float hipWidthHalf;
            public float thigh;
            public float shin;
            public float foot;
            public float shoulderRootX;
            public float shoulderRootY;
            public float shoulderX;
            public float shoulderY;
            public float upperArm;
            public float forearm;
            public float hand;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct DevPacket_t : IAxisData
        {
           
            public enum DevPacketComponents : UInt16
            {
                Accelerometer, 
                Gyroscope, 
                Magnetometer,
                FusionStates,
                LinearAccelerometer, ///< Accelerometer with gravity removed
                DEV_PACKET_COMPONENT_COUNT,
            };

        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (ushort)DevPacketComponents.DEV_PACKET_COMPONENT_COUNT)]
            //public bool[] flags;
            public DevPacketBitFlags flags;

            public DevPacketSensorData_t accelerometer;

            public DevPacketSensorData_t gyroscope;

            public DevPacketSensorData_t magnetometer;

            public DevPacketFusionState_t fusionState;

            public AxisNodePositions nodeIndex;

            public uint timeStamp;

            public DevPacketSensorData_t linearAccelerometer;
        }

        [StructLayout(LayoutKind.Explicit, Pack = 8)]
        public struct NodeConfigPayload_t
        {
            [FieldOffset(0)]
            DevPacketBitFlags devPacketBitFlag;
            [FieldOffset(0)]
            public uint value;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct DevPacketBitFlags
        {
            [MarshalAs(UnmanagedType.I1)]
            bool Accelerometer;
            [MarshalAs(UnmanagedType.I1)]
            bool Gyroscope;
            [MarshalAs(UnmanagedType.I1)]
            bool Magnetometer;
            [MarshalAs(UnmanagedType.I1)]
            bool FusionStates;
            [MarshalAs(UnmanagedType.I1)]
            bool LinearAccelerometer;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct NodeConfig_t
        {
            public AxisNodeConfigCategories categories;
            public byte parameter;
            public NodeConfigPayload_t payload;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct NodeConfigResponsePowerPayload_t
        {           
            public byte power;

            public byte frequency;
        }

        [StructLayout(LayoutKind.Explicit, Pack = 8)]
        public struct NodeConfigResponsePayload_t
        {
            [FieldOffset(0)]
            public NodeConfigResponsePowerPayload_t power;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct NodeConfigResponse_t : IAxisData
        {
            public AxisNodeConfigCategories category;
            public NodeConfigResponsePayload_t payload;
        }

        [StructLayout(LayoutKind.Explicit, Pack = 8)]
        public struct AxisPacket_t : IAxisData
        {
            [FieldOffset(0)]
            public IMUData_t imuData;

            [FieldOffset(0)]
            public NodesInfo_t nodesInfo;

            [FieldOffset(0)]
            public DoubleClickNodesInfo_t doubleClickNodesInfo;

            [FieldOffset(0)]
            public PairedNodesInfo_t pairedNodesInfo;

            [FieldOffset(0)]
            public VisibleNodesInfo_t visibleNodesInfo;

            [FieldOffset(0)]
            public SetWifiRes_t setWifiRes;

            [FieldOffset(0)]
            public WifiRes_t wifiRes;

            [FieldOffset(0)]
            public BattInfo_t battInfo;

            [FieldOffset(0)]
            public VersionsInfo_t versions;

            [FieldOffset(0)]
            public SuitInfo_t suitInfo;

            [FieldOffset(0)]
            public DevPacket_t devPacket;

            [FieldOffset(0)]
            public NodeConfigResponse_t nodeConfigRes;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct RebootCommandPayload_t
        {
            public enum RebootCommandModes : ushort
            {
                AllPaired,
                Legs,
                UpperBody
            }

            public RebootCommandModes mode;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct WifiCommandPayload_t
        {
            public byte channel;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct NodeInfoCommandPayload_t
        {
            public enum NodeInfoCommandModes : ushort
            {
                Visible,
                Paired
            }
            NodeInfoCommandModes mode;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct NodeCalibrationCommandPayload_t
        {
            public enum CalibrationCommandTypes : ushort
            {
                All,
                CalibrateFrame,
                Zero
            }
            public enum CalibrationCommandPoses : ushort
            {
                TableFloor,
                Body
            }
            public CalibrationCommandTypes type;
            public CalibrationCommandPoses pose;
        }

       
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct NodeOperationModeCommandPayload_t
        {
            public AxisNodeOperationModes mode;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct BuzzCommandPayload_t
        {
            public AxisNodePositions nodeIndex;
            public byte intensity;
            public byte duration;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct LEDCommandPayload_t
        {
            public AxisNodePositions nodeIndex;
            public byte red;
            public byte green;
            public byte blue;
            public byte brightness;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct BuzzMACCommandPayload_t
        {
          
            public MacAddresses mac;
            public byte intensity;
            public byte duration;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct LEDMACCommandPayload_t
        {
            public MacAddresses mac;
            public byte red;
            public byte green;
            public byte blue;
            public byte brightness;
            
        }

     
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 8)]
        public struct AxisHubInfo_t : IAxisData
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string deviceName;

            [MarshalAs(UnmanagedType.LPStr)]
            public string deviceIp;

            public int devicePort;

            [MarshalAs(UnmanagedType.LPStr)]
            public string deviceMac;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 8)]
        public struct AxisHubsInfo_t :IAxisData
        {
            public AxisRuntimeErrors error;
            [MarshalAs(UnmanagedType.LPStr)]
            public string errorString;
            public byte hubsDetected;
            public byte pageNumber;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public AxisHubInfo_t[] hubsInfo;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 8)]
        public struct AxisHubStatus_t : IAxisData
        {
            public AxisRuntimeErrors error;
            public int errorCode;
            [MarshalAs(UnmanagedType.LPStr)]
            public string errorString;
            public float battery;
            [MarshalAs(UnmanagedType.LPStr)]
            public string deviceName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string deviceMac;
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void AxisRuntimeErrorCallback(AxisRuntimeErrors runtimeErrors, int errorCode, string errorString);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void AxisPacketCallback(AxisPacketTypes packetType, in AxisPacket_t incomingPacket);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void AxisHubInfoCallback(in AxisHubsInfo_t hubInfo);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void AxisHubStatusCallback(in AxisHubStatus_t hubStatus);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void AxisDongleConnectionCallback([MarshalAs(UnmanagedType.I1)] bool connected);
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct AxisSteamVRConfig_t
        {
            public float userHeight;
            public SteamVRTrackerProfile_t steamVRTrackers;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct AxisSDKConfig_t
        {
            public AxisRuntimeErrorCallback runtimeErrorCallback;
            public AxisPacketCallback packetCallback;
            public AxisDongleConnectionCallback dongleConnectionCallback;
            public bool steamVR;
            public AxisSteamVRConfig_t steamVrConfig;
            public AxisHipProviderModes hipProvider;
            public AxisHubLocations hubLocations;
        }
    }
}