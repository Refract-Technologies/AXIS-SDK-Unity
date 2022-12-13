using System.Collections;
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
                case NodeBinding.RightTigh:
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

                case NodeBinding.RightHand:
                    return HumanBodyBones.RightHand;

                case NodeBinding.LeftHand:
                    return HumanBodyBones.LeftHand;

                case NodeBinding.WaistHub:
                    return HumanBodyBones.Spine;

                case NodeBinding.Hips:
                    return HumanBodyBones.Hips;


                default:
                    //Debug.LogWarning($"Bone not found {nodeLimb}");
                    return HumanBodyBones.LastBone;
            }
        }

        internal static Quaternion ConvertRotationBasedOnKey(NodeBinding key, Quaternion rotation)
        {

            if (IsLegLimb(key))
            {
                return new Quaternion(
                            -rotation.x, //x
                            rotation.y, //z
                            -rotation.z, //y                           
                            -rotation.w  //w
                            );
            }

            else if (IsFootLimb(key))
            {

                return new Quaternion(
                            -rotation.x, //x
                            rotation.y, //z
                            -rotation.z, //y                           
                            -rotation.w  //w
                           );
            }
            else if (IsFreeNode(key))
            {
                return new Quaternion(
                           rotation.x, //x
                           rotation.y, //z
                           rotation.z, //y                          
                           -rotation.w  //w
                           );
            }
            else if (IsHub(key))
            {
                return new Quaternion(
                           rotation.x, //x
                           -rotation.y, //z
                           -rotation.z, //y                          
                           rotation.w  //w
                           );
            }
            else
            {
                return new Quaternion(
                           -rotation.x, //x
                           -rotation.y, //z
                           -rotation.z, //y                          
                           rotation.w  //w
                           );
            }
        }

        #endregion


        public static List<NodeBinding> GetDefaultNodeArrangement()
        {
            return new List<NodeBinding>
            {
                NodeBinding.RightTigh,
                NodeBinding.RightCalf,
                NodeBinding.LeftThigh,
                NodeBinding.LeftCalf,
                NodeBinding.RightUpperArm,
                NodeBinding.RightForeArm,
                NodeBinding.LeftUpperArm,
                NodeBinding.LeftForeArm,
                NodeBinding.Chest
            };
        }

        

        #region BindingClassification
        internal static bool IsNodeObjectBinding(NodeBinding nodeBinding)
        {
            return nodeBinding == NodeBinding.NodeObject;
        }

        internal static bool IsMannequinBinding(NodeBinding nodeBinding)
        {
            return nodeBinding != NodeBinding.NodeObject && nodeBinding != NodeBinding.FreeNode;

        }

        private static bool IsHub(NodeBinding key)
        {
            return key == NodeBinding.WaistHub;
        }


        private static bool IsFreeNode(NodeBinding key)
        {
            return key == NodeBinding.NodeObject;
        }

        private static bool IsFootLimb(NodeBinding key)
        {
            return key == NodeBinding.LeftFoot || key == NodeBinding.RightFoot;
        }

        public static bool IsLegLimb(NodeBinding limb)
        {
            return limb == NodeBinding.LeftThigh || limb == NodeBinding.RightTigh;
        }

        public static NodeBinding[] NodeBindingInOrder = new NodeBinding[]
        {
            NodeBinding.Hips,
            NodeBinding.WaistHub,
            NodeBinding.Chest,
            NodeBinding.RightTigh,
            NodeBinding.RightCalf,
            NodeBinding.LeftThigh,
            NodeBinding.LeftCalf,
            NodeBinding.RightUpperArm,
            NodeBinding.RightForeArm,
            NodeBinding.LeftUpperArm,
            NodeBinding.LeftForeArm,
            NodeBinding.LeftFoot,
            NodeBinding.RightFoot,
            NodeBinding.Head,
            NodeBinding.NodeObject,
            NodeBinding.RightHand,
            NodeBinding.LeftHand

        };

        #endregion

    }


}
