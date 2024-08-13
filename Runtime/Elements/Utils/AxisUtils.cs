using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Refract
{
    namespace AXIS
    {
        public static class AxisUtils
        {
            public static Quaternion ConvertAxisQuatToUnityQuat(Quat_t axisQuat)
            {
                return new Quaternion(axisQuat.x, axisQuat.z, axisQuat.y, -axisQuat.w);
            }
            public static NodeIMUData_t GetAxisPositionNodeIMUData(NodeIMUData17_t axisImuData, AxisNodePositions position)
            {
                NodeIMUData_t nodeIMUData = new NodeIMUData_t();
                switch (position)
                {
                    case AxisNodePositions.RightThigh:
                        nodeIMUData = axisImuData.rightThigh;
                        break;
                    case AxisNodePositions.RightCalf:
                        nodeIMUData = axisImuData.rightCalf;
                        break;
                    case AxisNodePositions.LeftThigh:
                        nodeIMUData = axisImuData.leftThigh;
                        break;
                    case AxisNodePositions.LeftCalf:
                        nodeIMUData = axisImuData.leftCalf;
                        break;
                    case AxisNodePositions.RightUpperArm:
                        nodeIMUData = axisImuData.rightUpperArm;
                        break;
                    case AxisNodePositions.RightForearm:
                        nodeIMUData = axisImuData.rightForearm;
                        break;
                    case AxisNodePositions.LeftUpperArm:
                        nodeIMUData = axisImuData.leftUpperArm;
                        break;
                    case AxisNodePositions.LeftForearm:
                        nodeIMUData = axisImuData.leftForearm;
                        break;
                    case AxisNodePositions.Spine:
                        nodeIMUData = axisImuData.spine;
                        break;
                    case AxisNodePositions.RightFoot:
                        nodeIMUData = axisImuData.rightFoot;
                        break;
                    case AxisNodePositions.LeftFoot:
                        nodeIMUData = axisImuData.leftFoot;
                        break;
                    case AxisNodePositions.RightHand:
                        nodeIMUData = axisImuData.rightHand;
                        break;
                    case AxisNodePositions.LeftHand:
                        nodeIMUData = axisImuData.leftHand;
                        break;
                    case AxisNodePositions.RightShoulder:
                        nodeIMUData = axisImuData.rightShoulder;
                        break;
                    case AxisNodePositions.LeftShoulder:
                        nodeIMUData = axisImuData.leftShoulder;
                        break;
                    case AxisNodePositions.Head:
                        nodeIMUData = axisImuData.head;
                        break;
                    case AxisNodePositions.Hip:
                        nodeIMUData = axisImuData.hip;
                        break;
                  
                    default:
                        throw new System.ArgumentException("AxisPosition has to be valid");
                }
                return nodeIMUData;
            }
            public static Vector3 ConvertAxisVector3toUnityVector3(Vector3_t v)
            {
                return new Vector3(-v.x, v.y, -v.z);
            }
           
        }
    }
}

