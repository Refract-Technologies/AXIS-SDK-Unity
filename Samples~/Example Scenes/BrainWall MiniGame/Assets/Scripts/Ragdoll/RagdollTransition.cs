using System;
using System.Collections;
using System.Collections.Generic;
using Axis.Elements.AnimatorLink;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
using UnityEngine;


public abstract class CharacterModeTransition: MonoBehaviour
{
    protected class BoneTransform
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

    }
}

public class RagdollTransition : CharacterModeTransition
{

    public Action OnTransitionFinished;

    private Rigidbody[] ragdollRigidbodies;
    private Animator animator;
    private RagdollState currentState;

    private float timeToWakeUp = 3f;
    [SerializeField] private float timeToResetBones;
    private Transform hipsBone;

    private BoneTransform[] standUpBoneTransforms;
    private BoneTransform[] ragdollBoneTransforms;
    private Transform[] bones;

    [SerializeField] private string standUpStateName;
    [SerializeField] private string standUpClipName;

    float elapsedTimeToWakeUp;

    private float elapsedResetBonesTime;
    private enum RagdollState
    {
        Ragdoll,
        StandingUp,
        ResettingBones
    }

    private void OnEnable()
    {
        EnterRagdollState();
    }

    private void OnDisable()
    {
        ExitRagdollState();
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        hipsBone = animator.GetBoneTransform(HumanBodyBones.Hips);
        ConfigureJoints();
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();

        bones = hipsBone.GetComponentsInChildren<Transform>();
        standUpBoneTransforms = new BoneTransform[bones.Length];
        ragdollBoneTransforms = new BoneTransform[bones.Length];

        for (int boneIndex = 0; boneIndex < bones.Length; boneIndex++)
        {
            standUpBoneTransforms[boneIndex] = new BoneTransform();
            ragdollBoneTransforms[boneIndex] = new BoneTransform();
        }

        PopulateAnimationStartBoneTransforms(standUpClipName, standUpBoneTransforms);
        ExitRagdollState();

        
    }

    private void AlignRotationToHips()
    {
        Vector3 originalHipsPosition = hipsBone.position;
        Quaternion originalHipsRotation = hipsBone.rotation;

        Vector3 desiredDirection = hipsBone.up * -1;
        desiredDirection.y = 0;
        desiredDirection.Normalize();

        Quaternion fromToRotation = Quaternion.FromToRotation(transform.forward, desiredDirection);

        transform.rotation *= fromToRotation;

        hipsBone.position = originalHipsPosition;
        hipsBone.rotation = originalHipsRotation;

    }
    private void AlignPositionToHips()
    {
        Vector3 originalHipsPosition = hipsBone.position;
        transform.position = hipsBone.position;

        Vector3 positionOffset = standUpBoneTransforms[0].Position;
        positionOffset.y = 0f;

        positionOffset = transform.rotation * positionOffset;
        transform.position -= positionOffset;

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo))
        {
            transform.position = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);

        }
        hipsBone.position = originalHipsPosition;
    }



    private void ConfigureJoints()
    {
        foreach (CharacterJoint joint in GetComponentsInChildren<CharacterJoint>())
        {
            joint.enableProjection = true;
        }
    }

    public void EnterRagdollState()
    {
        currentState = RagdollState.Ragdoll;

        elapsedTimeToWakeUp = timeToWakeUp;

        animator.enabled = false;
        foreach (var rigidbodie in ragdollRigidbodies)
        {
            rigidbodie.isKinematic = false;
        }

        
    }

    private void ExitRagdollState()
    {
        foreach (var rigidbody in ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }

        animator.enabled = true;
        
        enabled = false;
    }

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            EnterRagdollState();
            //currentState = CharacterState.Ragdoll;
            //hipsBone.GetComponent<Rigidbody>().AddForce(hipsBone.forward * 10, ForceMode.Force);

        }



        switch (currentState)
        {
           
            case RagdollState.Ragdoll:         
                RagdollBehaviour();
                break;

            case RagdollState.StandingUp:
                StandingUpBehavior();
                break;

            case RagdollState.ResettingBones:
                ResettingBonesBehavior();
                break;
            default:
                break;
        }
    }


    private void StandingUpBehavior()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName(standUpStateName) == false)
        {
            OnTransitionFinished?.Invoke();
            enabled = false;
            
        }
    }
    
    private void ResettingBonesBehavior()
    {
        elapsedResetBonesTime += Time.deltaTime;
        float elapsedPercentage = elapsedResetBonesTime / timeToResetBones;

        for (int boneIndex = 0; boneIndex < bones.Length; boneIndex++)
        {
            bones[boneIndex].localPosition = Vector3.Lerp(
                ragdollBoneTransforms[boneIndex].Position,
                standUpBoneTransforms[boneIndex].Position, 
                elapsedPercentage);

            bones[boneIndex].localRotation = Quaternion.Lerp(
                ragdollBoneTransforms[boneIndex].Rotation,
                standUpBoneTransforms[boneIndex].Rotation, 
                elapsedPercentage);


        }

        if(elapsedPercentage >= 1)
        {
            currentState = RagdollState.StandingUp;
            animator.enabled = true;
            animator.Play(standUpStateName);
        }
    }

    private void RagdollBehaviour()
    {
        

        elapsedTimeToWakeUp -= Time.deltaTime;
        if (elapsedTimeToWakeUp <= 0)
        {
            AlignRotationToHips();
            AlignPositionToHips();
            PopulateBoneTransforms(ragdollBoneTransforms);

            currentState = RagdollState.ResettingBones;
            elapsedResetBonesTime = 0f;
        }
    }

    private void PopulateBoneTransforms(BoneTransform[] boneTransforms)
    {
        for (int boneIndex = 0; boneIndex < bones.Length; boneIndex++)
        {
            boneTransforms[boneIndex].Position = bones[boneIndex].localPosition;
            boneTransforms[boneIndex].Rotation = bones[boneIndex].localRotation;
        }
    }

    private void PopulateAnimationStartBoneTransforms(string clipName, BoneTransform[] boneTransforms)
    {
        Vector3 positionBeforeSampling = transform.position;
        Quaternion rotationBeforeSampling = transform.rotation;
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if(clip.name == clipName)
            {
                clip.SampleAnimation(gameObject, 0f);
                PopulateBoneTransforms(standUpBoneTransforms);
                break;
            }
        }


        transform.position = positionBeforeSampling;
        transform.rotation = rotationBeforeSampling;
    }
}
