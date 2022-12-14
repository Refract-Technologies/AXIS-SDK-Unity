using Axis.DataProcessing;
using Axis.Enumerations;
using Axis.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Axis.Elements.AnimatorLink
{

    [ExecuteAlways]
    public class BodyModelAnimatorLink : AxisAnimatorLink
    {
        public PoseStorage tPoseLocalRotations;
        
        private Dictionary<NodeBinding, Quaternion> defaultLimbsRotations;
        private Dictionary<NodeBinding, Quaternion> lastLimbRotations;

        private HumanPose humanPose;
        private HumanPoseHandler humanPoseHandler;

        [HideInInspector] public Rigidbody[] limbsRigidBodies;
        [HideInInspector] public bool enforceDefaultRotation;
        [HideInInspector] public bool storeTPoseRotations;


        public void GoToTPose()
        {
            foreach (HumanBodyBones humanBodyBone in tPoseLocalRotations.poseLocalRotations.Keys)
            {
                Animator.GetBoneTransform(humanBodyBone).localRotation = tPoseLocalRotations.poseLocalRotations[humanBodyBone];
            }
        }

        protected override void Awake()
        {
            base.Awake();

            DisableColliders();
           
            humanPoseHandler = new HumanPoseHandler(Animator.avatar, transform);
            limbsRigidBodies = GetComponentsInChildren<Rigidbody>();
        }

        private void DisableColliders()
        {
            foreach (Collider collider in GetComponentsInChildren<Collider>())
            {
                collider.enabled = false;
            }
        }

        public virtual void UpdateTransforms(Dictionary<NodeBinding, AxisNode> nodesByLimb)
        {
            foreach (NodeBinding limb in AxisDataUtility.NodeBindingInOrder)
            {
                var nodeTransform = nodesByLimb[limb].transform;

                if (nodeTransform.rotation.w != 1)
                {
                    if (nodeTransform.rotation != Quaternion.identity)
                    {
                        Vector3 upDirectionForArm;
                        Vector3 rightDirectionForArm;                       

                        if(AxisDataUtility.IsLegLimb(limb) || limb == NodeBinding.RightCalf || limb == NodeBinding.LeftCalf)                    
                        {
                            upDirectionForArm = Animator.transform.InverseTransformDirection(-nodeTransform.up);
                            rightDirectionForArm = Animator.transform.InverseTransformDirection(-nodeTransform.forward);

                        }
                        else if (limb == NodeBinding.LeftFoot)
                        {
                            upDirectionForArm = Animator.transform.InverseTransformDirection(nodeTransform.up);
                            rightDirectionForArm = Animator.transform.InverseTransformDirection(-nodeTransform.forward);
                        }

                        else
                        {
                            upDirectionForArm = Animator.transform.InverseTransformDirection(nodeTransform.right);
                            rightDirectionForArm = Animator.transform.InverseTransformDirection(nodeTransform.up);
                        }

                        Quaternion rotation = Quaternion.LookRotation(-rightDirectionForArm, upDirectionForArm);
                        transformsByNodeLimbs[limb].rotation = rotation;

                        if (enforceDefaultRotation == true)
                        {
                            GoToTPose();                   
                        }                      
                        lastLimbRotations[limb] = transformsByNodeLimbs[limb].rotation;                     
                    }
                }               
            }
        }

        internal void StoreDefaultTransformsRotations()
        {
            lastLimbRotations = new Dictionary<NodeBinding, Quaternion>();
            defaultLimbsRotations = new Dictionary<NodeBinding, Quaternion>();
            foreach (var limb in transformsByNodeLimbs.Keys)
            {
                defaultLimbsRotations.Add(limb, transformsByNodeLimbs[limb].rotation);
                lastLimbRotations.Add(limb, transformsByNodeLimbs[limb].rotation);
            }
        }      

        internal HumanPose GetHumanPose()
        {
            if(humanPoseHandler == null)
            {
                if(Animator == null)
                {
                    Animator = GetComponent<Animator>();
                }
                humanPoseHandler = new HumanPoseHandler(Animator.avatar, transform);
            }
            humanPoseHandler.GetHumanPose(ref humanPose);
            return humanPose;
        }

    }
}


