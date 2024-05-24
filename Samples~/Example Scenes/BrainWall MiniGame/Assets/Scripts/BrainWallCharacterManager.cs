using System;
using System.Collections;
using System.Collections.Generic;
using Axis.Elements.AnimatorLink;
using UnityEngine;

namespace AxisExampleScenes.Minigame
{
    public class BrainWallCharacterManager : MonoBehaviour
    {
        Rigidbody[] ragdollRigidBodies;
        private HumanoidCharacterAnimatorLink characterAnimatorLink;
        readonly List<RigidComponent> _rigids = new List<RigidComponent>();
        private void Awake()
        {
            ragdollRigidBodies = GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody ragdollRigidBodie in ragdollRigidBodies)
            {
                RigidComponent rigidCompontnt = new RigidComponent(ragdollRigidBodie);
                _rigids.Add(rigidCompontnt);
            }
        }

        private void Start()
        {
            Debug.Log("Fetching character Animator Link");
            characterAnimatorLink = GetComponent<HumanoidCharacterAnimatorLink>();
            SetAxisControl();
        }

        public void SetAxisControl()
        {
            EnableRagdoll(false);
            EnableAndResetAxis(true);
        }

        public void SetRagdollPhysicsControl()
        {
            EnableRagdoll(true);
            EnableAndResetAxis(false);
        }
        private void EnableAndResetAxis(bool value)
        {
            if (value == false)
            {
                characterAnimatorLink.enabled = false;
            }
            else
            {
                characterAnimatorLink.enabled = true;
                //characterAnimatorLink.LoadBoneGlobalPositions();
            }
        }

        struct RigidComponent
        {
            public readonly Rigidbody RigidBody;
            public readonly CharacterJoint Joint;
            public readonly Vector3 ConnectedAnchorDefault;

            public RigidComponent(Rigidbody rigid)
            {
                RigidBody = rigid;
                Joint = rigid.GetComponent<CharacterJoint>();
                if (Joint != null)
                    ConnectedAnchorDefault = Joint.connectedAnchor;
                else
                    ConnectedAnchorDefault = Vector3.zero;
            }
        }

        static IEnumerator FixTransformAndEnableJoint(RigidComponent joint)
        {
            if (joint.Joint == null || !joint.Joint.autoConfigureConnectedAnchor)
                yield break;

            SoftJointLimit highTwistLimit = new SoftJointLimit();
            SoftJointLimit lowTwistLimit = new SoftJointLimit();
            SoftJointLimit swing1Limit = new SoftJointLimit();
            SoftJointLimit swing2Limit = new SoftJointLimit();

            SoftJointLimit curHighTwistLimit = highTwistLimit = joint.Joint.highTwistLimit;
            SoftJointLimit curLowTwistLimit = lowTwistLimit = joint.Joint.lowTwistLimit;
            SoftJointLimit curSwing1Limit = swing1Limit = joint.Joint.swing1Limit;
            SoftJointLimit curSwing2Limit = swing2Limit = joint.Joint.swing2Limit;

            float aTime = 0.3f;
            Vector3 startConPosition = joint.Joint.connectedBody.transform.InverseTransformVector(joint.Joint.transform.position - joint.Joint.connectedBody.transform.position);

            joint.Joint.autoConfigureConnectedAnchor = false;
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
            {
                Vector3 newConPosition = Vector3.Lerp(startConPosition, joint.ConnectedAnchorDefault, t);
                joint.Joint.connectedAnchor = newConPosition;

                curHighTwistLimit.limit = Mathf.Lerp(177, highTwistLimit.limit, t);
                curLowTwistLimit.limit = Mathf.Lerp(-177, lowTwistLimit.limit, t);
                curSwing1Limit.limit = Mathf.Lerp(177, swing1Limit.limit, t);
                curSwing2Limit.limit = Mathf.Lerp(177, swing2Limit.limit, t);

                joint.Joint.highTwistLimit = curHighTwistLimit;
                joint.Joint.lowTwistLimit = curLowTwistLimit;
                joint.Joint.swing1Limit = curSwing1Limit;
                joint.Joint.swing2Limit = curSwing2Limit;


                yield return null;
            }
            joint.Joint.connectedAnchor = joint.ConnectedAnchorDefault;
            yield return new WaitForFixedUpdate();
            joint.Joint.autoConfigureConnectedAnchor = true;


            joint.Joint.highTwistLimit = highTwistLimit;
            joint.Joint.lowTwistLimit = lowTwistLimit;
            joint.Joint.swing1Limit = swing1Limit;
            joint.Joint.swing2Limit = swing2Limit;
        }

        private void EnableRagdoll(bool value)
        {
            foreach (var rigid in _rigids)
            {
                Collider partColider = rigid.RigidBody.GetComponent<Collider>();

                // fix for RagdollHelper (bone collider - BoneHelper.cs)
                //if (partColider == null)
                //{
                //    const string colliderNodeSufix = "_ColliderRotator";
                //    string childName = rigid.RigidBody.name + colliderNodeSufix;
                //    Transform transform = rigid.RigidBody.transform.Find(childName);
                //    partColider = transform.GetComponent<Collider>();
                //}

                partColider.isTrigger = !value;

                if (value == true)
                {
                    rigid.RigidBody.isKinematic = false;
                    StartCoroutine(FixTransformAndEnableJoint(rigid));
                }
                else
                    rigid.RigidBody.isKinematic = true;
            }


            //foreach (var rb in ragdollRigidBodies)
            //{
            //    if (value == true)
            //    {
            //
            //        rb.velocity = Vector3.zero;
            //        rb.angularVelocity = Vector3.zero;
            //        rb.ResetInertiaTensor();
            //        rb.ResetCenterOfMass();
            //        rb.isKinematic = false;
            //    }
            //    else
            //    {
            //        rb.isKinematic = true;
            //    }
            //}



        }

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    Debug.Log("Axis");
            //    SetAxisControl();
            //    //EnableRagdoll(characterAnimatorLink.enabled);
            //}
            //
            //if (Input.GetKeyDown(KeyCode.R))
            //{
            //    Debug.Log("Ragdoll");
            //    SetRagdollPhysicsControl();
            //}
        }
    }

}
