using System;

namespace Axis.Enumerations
{
    public enum GroundLevelContraintMode
    {
        NoContraints,
        MinContraint,
        MinMaxConstraint,
    }
    public enum CharacterPositioningMode
    {
        None,
        ArCore,
        MannequinAnchoring,
        CharacterAnchoring
    }
    public enum GroundDragbackMode
    {
        None,
        Instant
    }

    //public enum HipsRotationSolver
    //{
    //    FollowSpine,
    //    EstimateLowerLimbs,
    //    Compose
    //}

    public enum CalibrationPose
    {
        Standing,
        Sitting
    }
    public enum RotationInputAxis
    {
        X,
        Y,
        Z,
        All,
        AllEuler
    }

    //public enum NodeArrangement
    //{
    //    Default,
    //    ForearmAsFeet,
    //    LeftUpperArmAsHead,
    //    LeftUpperArmAsGrabbableNode,
    //    RightUpperArmAsHead,
    //}

    public enum FreeNodeTypes
    {
        GrabbableFreeNode,
        Dial
    }

    public enum MirroredNodeBindingMode
    {
        Local,
        World
    }
    public enum NodeBinding
    {
        RightTigh,  
        RightCalf,  
        LeftThigh,  
        LeftCalf,
        RightUpperArm,
        RightForeArm,
        LeftUpperArm,
        LeftForeArm,
        Chest,
        LeftFoot,
        RightFoot,
        Head,     
        RightHand,
        LeftHand,
        WaistHub,
        Hips,
        NodeObject,
        FreeNode
    }
}


