using Axis.DataTypes;
using Axis.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CameraView
{
    Perspective,
    TopView

}


public class AxisBrainUI : MonoBehaviour
{
    public GameObject axisPosition;
    public Camera topViewCamera;
    public Camera perspectiveCamera;
    public Camera mannequinCamera;

    public GameObject mainPanel;

    private CameraView cameraView = CameraView.TopView;

    private void Awake()
    {
        var cameras = GetComponentsInChildren<Camera>();
        foreach (var camera in cameras)
        {
            camera.enabled = true;
        }

        
    }

    private void Start()
    {
        //HandleUpdatedView();
        //ToggleVisibility();
        SetVisibility(true);
    }


    public void ToggleView()
    {
        cameraView = cameraView == CameraView.TopView ? CameraView.Perspective : CameraView.TopView;
        HandleUpdatedView();
    }

    private void HandleUpdatedView()
    {
        if (cameraView == CameraView.TopView)
        {
            topViewCamera.enabled = true;
            perspectiveCamera.enabled = false;

        }
        else
        {
            perspectiveCamera.enabled = true;
            topViewCamera.enabled = false;

        }
    }

    public void ToggleVisibility()
    {
        if(mainPanel.activeSelf == true)
        {
            SetVisibility(false);
        }
        else
        {
            SetVisibility(true);
            //mainPanel.SetActive(true);
            //mannequinCamera.enabled = true;

            
            
        }
    }

    private void SetVisibility(bool value)
    {

        mainPanel.SetActive(value);
        
        mannequinCamera.enabled = value;

        if (value == true)
        {
            HandleUpdatedView();
        } else
        {
            topViewCamera.enabled = value;
            perspectiveCamera.enabled = value;
        }

    }
}
