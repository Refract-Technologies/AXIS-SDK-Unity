using System;
using System.Collections;
using System.Collections.Generic;
using Axis.Overrides;
using Axis.Solvers;
using UnityEngine;

public class BoneMirroringTransition : CharacterModeTransition
{
    public Animator animatorToCopyFrom;
    private Animator animator;

    private BoneTransform[] characterBones;
    private BoneTransform[] bonesToMirror;

    public CharacterRotationOverride axisCharacterRotationOverride;
    public AbsolutePositionSolver axisCharacterPositionSolver;
    public enum PoseTransferMode
    {
        CharacterCopyAxis,
        AxisCopyCharacter
    }

    public enum MirroringState
    {
        
        StartTransition,
        Transitioning,
        Mirroring
    }

    public MirroringState status;
    public PoseTransferMode positionTransferMode = PoseTransferMode.CharacterCopyAxis;
    public PoseTransferMode rotationTransferMode = PoseTransferMode.CharacterCopyAxis;
    [SerializeField] private float timeToTransfer = 0.25f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enabled = false;
    }

    private void OnEnable()
    {       
        status = MirroringState.StartTransition;
    }

    IEnumerator LerpPosition(Transform transformToLerpTo, Transform transform, float duration)
    {
        float time = 0;
        Vector3 startingPosition = transform.position;
        while(time < duration)
        {
            float elapsedTimePercent = time / duration;
            transform.position = Vector3.Lerp(startingPosition, transformToLerpTo.position, elapsedTimePercent);
            time += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator LerpRotation(Transform transformToLerpTo, Transform transform, float duration)
    {
        float time = 0;
        Quaternion startingRotaton = transform.rotation;
        while (time < duration)
        {
            float elapsedTimePercent = time / duration;
            transform.rotation = Quaternion.Lerp(startingRotaton, transformToLerpTo.rotation, elapsedTimePercent);
            time += Time.deltaTime;
            yield return null;
        }

    }

    IEnumerator LerpAllBones(Animator animatorToCopyFrom, Animator animatorToLerp, float duration)
    {
        float time = 0;       
        List<Quaternion> startingRotations = CopyRotationsFromAnimator(animatorToLerp);
        List<Transform> transformsToMirror = GetTransformsFromAnimator(animatorToCopyFrom);
        List<Transform> transforms = GetTransformsFromAnimator(animatorToLerp);
    
        while (time < duration)
        {
            for (int i = 0; i < transforms.Count; i++)
            {
                
                transforms[i].rotation = Quaternion.Lerp(startingRotations[i], transformsToMirror[i].rotation, time);

            }

            time += Time.deltaTime;
            yield return null;
        }

        status = MirroringState.Mirroring;
    }

    private List<Transform> GetTransformsFromAnimator(Animator animatorToCopyFrom)
    {
        List<Transform> transformsFromAnimator = new List<Transform>();
        
        foreach (HumanBodyBones bodyBone in Enum.GetValues(typeof(HumanBodyBones)))
        {
            if (bodyBone == HumanBodyBones.LastBone)
            {
                continue;
            }
            Transform bone = animatorToCopyFrom.GetBoneTransform(bodyBone);

            if (bone != null)
            {
                transformsFromAnimator.Add(bone);
            }
                

        }

        return transformsFromAnimator;
    }

    private List<Quaternion> CopyRotationsFromAnimator(Animator animatorToCopyFrom)
    {
        List<Quaternion> rotations = new List<Quaternion>();
        foreach (HumanBodyBones bodyBone in Enum.GetValues(typeof(HumanBodyBones)))
        {
            if (bodyBone == HumanBodyBones.LastBone)
            {
                continue;
            }
            Transform bone = animator.GetBoneTransform(bodyBone);

            if(bone != null)
            {
                rotations.Add(animatorToCopyFrom.GetBoneTransform(bodyBone).rotation);
            }
            

        }

        return rotations;
    }

    private void Update()
    {
        if (status == MirroringState.Mirroring)
        {
            foreach (HumanBodyBones bodyBone in Enum.GetValues(typeof(HumanBodyBones)))
            {
                if (bodyBone == HumanBodyBones.LastBone)
                {
                    continue;
                }


                Transform boneToCopy = animator.GetBoneTransform(bodyBone);

                if (boneToCopy != null)
                {
                    boneToCopy.rotation = animatorToCopyFrom.GetBoneTransform(bodyBone).rotation;
                    boneToCopy.position = animatorToCopyFrom.GetBoneTransform(bodyBone).position;
                }

            }
        }

        if (status == MirroringState.StartTransition)
        {
            animator.enabled = false;
            
            if(rotationTransferMode == PoseTransferMode.CharacterCopyAxis)
            {
                StartCoroutine(LerpRotation(animatorToCopyFrom.transform, transform, timeToTransfer));
                
            }
            else
            {
                Vector3 rotationDifference = animatorToCopyFrom.transform.eulerAngles - transform.eulerAngles;
                axisCharacterRotationOverride.xOffset -= rotationDifference.x;
                axisCharacterRotationOverride.yOffset -= rotationDifference.y;
                axisCharacterRotationOverride.zOffset -= rotationDifference.z;

            }

            if (positionTransferMode == PoseTransferMode.CharacterCopyAxis)
            {
                StartCoroutine(LerpPosition(animatorToCopyFrom.transform, transform, timeToTransfer));
                transform.position = animatorToCopyFrom.transform.position;
                
            } else
            {
                Vector3 positionDifference = animatorToCopyFrom.transform.position - transform.position;
                axisCharacterPositionSolver.xOffset -= positionDifference.x;
                //axisCharacterPositionSolver.yOffset -= positionDifference.y;
                axisCharacterPositionSolver.zOffset -= positionDifference.z;

            }



            StartCoroutine(LerpAllBones(animatorToCopyFrom, animator, 1f));
            //StartCoroutine(LerpOneBone(animatorToCopyFrom.GetBoneTransform(HumanBodyBones.RightUpperArm).localRotation, 1f, animator.GetBoneTransform(HumanBodyBones.RightUpperArm)));
            status = MirroringState.Transitioning;
        }

    }


}
