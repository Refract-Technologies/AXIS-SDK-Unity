using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModeSelector : MonoBehaviour
{

    public RagdollTransition ragdollActivator;
    public BoneMirroringTransition boneMirrorer;
    public GameObject axisCharacter;
    public AfterRagdollGoTo afterRagdollState;
    public bool hideAxisCharacter = true;
    public enum AfterRagdollGoTo
    {
        BoneMirroring,
        Animation,
    }

    private void OnEnable()
    {
        ragdollActivator.OnTransitionFinished += HandleOnTransitionFinished;        
    }

    private void HandleOnTransitionFinished()
    {
        switch (afterRagdollState)
        {
            case AfterRagdollGoTo.BoneMirroring:
                
                boneMirrorer.enabled = true;
                break;
            case AfterRagdollGoTo.Animation:
                break;
            default:
                break;
        }
    }

    private void HideAxisCharacter()
    {
        var renderers = axisCharacter.GetComponentsInChildren<Renderer>();
        
        foreach (Renderer meshRenderer in renderers)
        {
            meshRenderer.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EnableRagdollMode();

        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            boneMirrorer.enabled = true;
            ragdollActivator.enabled = false;
        }
        
    }

    private void Start()
    {
        boneMirrorer.enabled = true;
        if (hideAxisCharacter == true)
        {
            HideAxisCharacter();
        }
    }

    public void EnableRagdollMode()
    {
        boneMirrorer.enabled = false;
        ragdollActivator.enabled = true;
    }
}
