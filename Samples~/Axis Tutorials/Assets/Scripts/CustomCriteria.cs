using Axis.Communication;
using Axis.Elements;
using Axis.Elements.Linker;
using Axis.Elements.MirroredNode;
using Axis.Tutorials.Elements;
using System.Collections.Generic;
using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.Callbacks;
#endif
using UnityEngine;

[CreateAssetMenu(fileName ="Custom Criteria")]
public class CustomCriteria : ScriptableObject
{
#if UNITY_EDITOR
    public bool anyNodeWasClicked = false;
    public bool isThereAnHumanoidCharacterOnScene = false;
    public bool isAxisBrainConnectedToAvatar;
    private static bool wasAxisEventsOpened = false;
    private static bool wasRotationFromAxisNodeOpened = false;

    private bool wasLedColorSet = false;
    private bool wasNodeVibrationSet = false;

    private void OnEnable()
    {
        wasLedColorSet = false;
        wasNodeVibrationSet = false;
        wasAxisEventsOpened = false;
        wasRotationFromAxisNodeOpened = false;
        anyNodeWasClicked = false;
        isThereAnHumanoidCharacterOnScene = false;
        isAxisBrainConnectedToAvatar = false;
        AxisTutorialNode.OnNodeSelected += HandleOnNodeSelected;
        NodeInputHandler.OnSetNodeVibrationClicked += HandleOnSetNodeVibrationClicked;
        NodeInputHandler.OnSetLedColorClicked += HandleOnSetLedColorClicked;

    }

   

    private void OnDisable()
    {
        AxisTutorialNode.OnNodeSelected -= HandleOnNodeSelected;
        NodeInputHandler.OnSetNodeVibrationClicked -= HandleOnSetNodeVibrationClicked;
        NodeInputHandler.OnSetLedColorClicked -= HandleOnSetLedColorClicked;
    }


    private void HandleOnSetLedColorClicked()
    {
        wasLedColorSet = true;
    }

    private void HandleOnSetNodeVibrationClicked()
    {
        wasNodeVibrationSet = true;
    }

    public bool WasNodeColorSet()
    {
        return wasLedColorSet;
    }

    public bool WasNodeVibrationSet()
    {
        return wasNodeVibrationSet;
    }

    public bool isRotateToolSelected()
    {
        return Tools.current == Tool.Rotate;
    }
    private void HandleOnNodeSelected(AxisTutorialNode selectedNode, int nodeIndex)
    {
        RotationFromAxisNode rotatingCube = FindObjectOfType<RotationFromAxisNode>();
        if(rotatingCube != null)
        {
            
            rotatingCube.nodeIndex = nodeIndex;
           
        }
        anyNodeWasClicked = true;
    }

    Dictionary<string, Vector3> storedRotationsOfTransformsToCheck;
    public bool isTransformRotationChanged(string transformName)
    {
        if (storedRotationsOfTransformsToCheck == null)
        {
            //Debug.Log("Is null");
            storedRotationsOfTransformsToCheck = new Dictionary<string, Vector3>();
        }

        var gameobjectToCheckRotation = GameObject.Find(transformName);

        if (gameobjectToCheckRotation == null)
        {
            Debug.LogWarning($"Transform {transformName} not found");
            return false;
        }

        Transform transformToCheckRotation = GameObject.Find(transformName).transform;

        

        if (storedRotationsOfTransformsToCheck.ContainsKey(transformName) == false)
        {
            storedRotationsOfTransformsToCheck.Add(transformName, transformToCheckRotation.localRotation.eulerAngles);
        }

        //Debug.Log($"Stored {storedRotationsOfTransformsToCheck[transformName]} Checking {transformToCheckRotation.localRotation.eulerAngles}");

        return 
            storedRotationsOfTransformsToCheck[transformName].x != transformToCheckRotation.localRotation.eulerAngles.x ||
            storedRotationsOfTransformsToCheck[transformName].y != transformToCheckRotation.localRotation.eulerAngles.y || 
            storedRotationsOfTransformsToCheck[transformName].z != transformToCheckRotation.localRotation.eulerAngles.z;

    }

    public bool isTransformSelected(string transformName)
    {
        var gameobjectToCheckControl = GameObject.Find(transformName);
        if(gameobjectToCheckControl == null)
        {
            Debug.LogWarning($"Transform {transformName} not found");
            return false;
        }

        
        return Selection.activeTransform != null && Selection.activeTransform.name == transformName;
    }

    public bool isAxisConnected()
    {
        return AxisRuntimeUdpSocket.IsConnectedToAxis;
    }

    public bool isLocalRotation(string transformName, float x, float y, float z)
    {
        var gameObjectToCheckRotation = GameObject.Find(transformName);

        if (gameObjectToCheckRotation == null)
        {
            Debug.LogWarning($"Transform {transformName} not found");
            return false;
        }

        Vector3 rotationToCheck = gameObjectToCheckRotation.transform.localEulerAngles;

        return rotationToCheck.x == x && rotationToCheck.y == y && rotationToCheck.z == z;
    }

    public bool isMirroredNodeBindedToCorrectNode(string transformName, string nodeBinding)
    {
        var gameobjectToCheckControl = GameObject.Find(transformName);

        if (gameobjectToCheckControl == null)
        {
            Debug.LogWarning($"Transform {transformName} not found");
            return false;
        }

        var mirroredNode = gameobjectToCheckControl.transform.parent.GetComponent<MirroredNode>();
        if (mirroredNode == null)
        {
            return false;
        }

        if(string.Equals( mirroredNode.BindedToNode.ToString(),nodeBinding) == true)
        {
            return true;
        }


        return false;
        
    }
    public bool isTransformControlledByNode(string transformName)
    {
        var gameobjectToCheckControl = GameObject.Find(transformName);

        if(gameobjectToCheckControl == null)
        {
            Debug.LogWarning($"Transform {transformName} not found");
            return false;
        }

        if(gameobjectToCheckControl.transform.parent.GetComponent<MirroredNode>() == true)
        {
            return true;
        }

        return false;
    }

    public bool IsMannequinShowing()
    {
        var axisMannequin = FindObjectOfType<AxisMannequin>();
        
        if(axisMannequin == null)
        {
            return false;
        }
        
        var meshRenderer = axisMannequin.bodyModel.GetComponentInChildren<Renderer>();
        
        if(meshRenderer == null)
        {
            return false;
        }

        if(meshRenderer.enabled == false)
        {
            return false;
        }

        return true;
    }
    public bool WasAnyNodeClicked()
    {
        return anyNodeWasClicked;
    }

    public void ResetWasAnyNodeClicked()
    {
        anyNodeWasClicked = false;
    }

    public bool WasRotationScriptOpened()
    {
        return wasRotationFromAxisNodeOpened;
    }

    [OnOpenAssetAttribute(0)]
    public static bool AssetOpened(int instanceID, int line)
    {
        
        string name = EditorUtility.InstanceIDToObject(instanceID).name;
        if (name == "AxisEvents")
        {
            wasAxisEventsOpened = true;
        }

        if(name == "RotationFromAxisNode")
        {
            wasRotationFromAxisNodeOpened = true;
        }
        return false;
    }

    public bool WasAxisEventsOpened()
    {
        return wasAxisEventsOpened;
    }

    public bool IsAxisBrainConnectedToAvatar()
    {
        return isAxisBrainConnectedToAvatar;
    }

    public bool DoeshCharacterToBrainLinkerIsConnectedToBrain() 
    {
        GameObject characterGameObject = FetchCharacterGameObject();

        if (characterGameObject == null)
        {
            return false;
        }

        if (characterGameObject.GetComponent<CharacterToBrainLinker>() == null)
        {
            return false;
        }

        if(characterGameObject.GetComponent<CharacterToBrainLinker>().linkedBrain == null){
            return false;
        }

        return true;
    }

    public GameObject FetchCharacterGameObject()
    {

        Animator[] animatorsOnScene = FindObjectsOfType<Animator>();
        GameObject characterGameObject = null;
        foreach (var animator in animatorsOnScene)
        {
            if (animator.name != "Y Bot")
            {
                characterGameObject = animator.gameObject;
            }
        }

        return characterGameObject;
    }
    public bool DoesCharacterHaveCharacterToBrainLinker()
    {
        GameObject characterGameObject = FetchCharacterGameObject();
      

        if(characterGameObject == null)
        {
            return false;
        }

        if(characterGameObject.GetComponent<CharacterToBrainLinker>() == null)
        {
            return false;
        }

        return true;
    }

    public bool IsThereAnHumanoidCharacterOnScene()
    {
        return isThereAnHumanoidCharacterOnScene;
    }

    private bool wasInPlayModeOnThisStep = false;

    public void ResetWasInPlayModeOnThisStep()
    {
        wasInPlayModeOnThisStep = false;
    }

    public void SetWasInPlayModeOnThisStep()
    {
        wasInPlayModeOnThisStep = true;
    }
    public bool WasInPlaymodeOnThisStep()
    {
        return wasInPlayModeOnThisStep;
    }
    #endif
}
