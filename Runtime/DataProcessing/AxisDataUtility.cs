using System.Collections.Generic;
using UnityEngine;

namespace Axis.DataProcessing
{
    using Enumerations;
    using Axis.DataTypes;
    using System;



    // Class that converts the raw dictionaries to a list of AxisNodesData, which contains rotation and acceleration data.
    public static class AxisDataUtility
    {
        #region Data Conversion

        // Depending on the nodes arrangement in the body, different nodes can be assigned to different bones in the model
        public static HumanBodyBones ConvertAxisLimbToHumanBone(NodeBinding nodeLimb)
        {
            switch (nodeLimb)
            {
                case NodeBinding.RightThigh:
                    return HumanBodyBones.RightUpperLeg;

                case NodeBinding.RightCalf:
                    return HumanBodyBones.RightLowerLeg;

                case NodeBinding.LeftThigh:
                    return HumanBodyBones.LeftUpperLeg;

                case NodeBinding.LeftCalf:
                    return HumanBodyBones.LeftLowerLeg;

                case NodeBinding.RightUpperArm:
                    return HumanBodyBones.RightUpperArm;

                case NodeBinding.RightForeArm:
                    return HumanBodyBones.RightLowerArm;

                case NodeBinding.LeftUpperArm:
                    return HumanBodyBones.LeftUpperArm;

                case NodeBinding.LeftForeArm:
                    return HumanBodyBones.LeftLowerArm;
                case NodeBinding.Chest:
                    return HumanBodyBones.Chest;

                case NodeBinding.RightFoot:
                    return HumanBodyBones.RightFoot;

                case NodeBinding.LeftFoot:
                    return HumanBodyBones.LeftFoot;

                case NodeBinding.Head:
                    return HumanBodyBones.Head;
                case NodeBinding.RightShoulder:
                    return HumanBodyBones.RightShoulder;
                case NodeBinding.LeftShoulder:
                    return HumanBodyBones.LeftShoulder;
                case NodeBinding.RightHand:
                    return HumanBodyBones.RightHand;

                case NodeBinding.LeftHand:
                    return HumanBodyBones.LeftHand;
                case NodeBinding.Hips:
                    return HumanBodyBones.Hips;


                default:
                    //Debug.LogWarning($"Bone not found {nodeLimb}");
                    return HumanBodyBones.LastBone;
            }
        }

        internal static Quaternion ConvertRotationBasedOnKey(NodeBinding key, Quaternion rotation)
        {
            
            return new Quaternion(
                           rotation.x, //x
                           rotation.y, //z
                           rotation.z, //y                          
                           rotation.w  //w
                           );
        }
        internal static Quaternion ConvertHubRotationToUnitySpace(AxisHubData axisHubData)
        {
            return new Quaternion(
                           -axisHubData.rotation.x, //x
                           axisHubData.rotation.z, //z
                           axisHubData.rotation.y, //y                          
                           axisHubData.rotation.w  //w
                           );
        }
        #endregion


        public static List<NodeBinding> GetDefaultNodeArrangement()
        {
            return new List<NodeBinding>
            {
                NodeBinding.RightThigh,
                NodeBinding.RightCalf,
                NodeBinding.LeftThigh,
                NodeBinding.LeftCalf,
                NodeBinding.RightUpperArm,
                NodeBinding.RightForeArm,
                NodeBinding.LeftUpperArm,
                NodeBinding.LeftForeArm,
                NodeBinding.Chest,
                NodeBinding.RightFoot,
                NodeBinding.LeftFoot,
                NodeBinding.RightHand,
                NodeBinding.LeftHand,
                NodeBinding.RightShoulder,
                NodeBinding.LeftShoulder,
                NodeBinding.Head,
                NodeBinding.Hips
            };
        }



        #region BindingClassification
        [Obsolete("Not in use anymore,Please use IsFreeNode instead")]
        internal static bool IsNodeObjectBinding(NodeBinding nodeBinding)
        {
            return nodeBinding == NodeBinding.NodeObject;
        }

        internal static bool IsMannequinBinding(NodeBinding nodeBinding)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return nodeBinding != NodeBinding.NodeObject && nodeBinding != NodeBinding.FreeNode;
#pragma warning restore CS0618 // Type or member is obsolete

        }

        private static bool IsHub(NodeBinding key)
        {
            return false;
            //return key == NodeBinding.WaistHub;
        }


        internal static bool IsFreeNode(NodeBinding key)
        {
            return key == NodeBinding.FreeNode;
        }


        //this array determines the ordering of mannequin parts 
        public static NodeBinding[] NodeBindingInOrder = new NodeBinding[]
        {
            NodeBinding.Hips,
            NodeBinding.Chest,
            NodeBinding.RightThigh,
            NodeBinding.RightCalf,
            NodeBinding.LeftThigh,
            NodeBinding.LeftCalf,
            NodeBinding.RightShoulder,
            NodeBinding.RightUpperArm,
            NodeBinding.RightForeArm,
            NodeBinding.LeftShoulder,
            NodeBinding.LeftUpperArm,
            NodeBinding.LeftForeArm,
            NodeBinding.RightFoot,
            NodeBinding.LeftFoot,
            NodeBinding.RightHand,
            NodeBinding.LeftHand,                           
            NodeBinding.Head

        };

        #endregion

    }


}
