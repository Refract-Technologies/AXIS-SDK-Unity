using Axis.DataTypes;
using Axis.Enumerations;
using Axis.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDial : MonoBehaviour
{
    public MeshRenderer ledMeshRenderer;
    int lastQuadrant;


    private void OnEnable()
    {
        AxisEvents.OnAxisOutputDataUpdated += HandleOnAxisDataUpdated;
    }

    private void OnDisable()
    {
        AxisEvents.OnAxisOutputDataUpdated -= HandleOnAxisDataUpdated;

    }

    private void Start()
    {
        lastQuadrant = GetCurrentQuadrant();              
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Zero");
            AxisEvents.OnZeroAll?.Invoke();
        }

        int currentQuadrant = GetCurrentQuadrant();

        if(currentQuadrant == 1 & lastQuadrant == 2)
        {
            AxisEvents.OnSetNodeVibration?.Invoke((int)NodeBinding.LeftThigh, 1f, 0.1f);
        }

        lastQuadrant = currentQuadrant;
    }

    

    private int GetCurrentQuadrant()
    {
        float yAngle = transform.localEulerAngles.y;
        if (yAngle > 0 && yAngle < 90)
        {
            return 0;
        }

        if (yAngle > 90 && yAngle < 180)
        {
            return 1;
        }

        if (yAngle > 180 && yAngle < 270)
        {
            return 2;
        }

        return 3;
    }

    private void HandleOnAxisDataUpdated(AxisOutputData axisOutputData)
    {
        Quaternion nodeRotation = axisOutputData.nodesDataList[(int)NodeBinding.LeftThigh].rotation;
        Vector3 euler = nodeRotation.eulerAngles;
        transform.localEulerAngles = new Vector3(0f, euler.z, 0f);
    }

    

    

}
