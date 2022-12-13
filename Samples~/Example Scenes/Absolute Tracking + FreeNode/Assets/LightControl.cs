using System.Collections;
using System.Collections.Generic;
using Axis.DataTypes;
using Axis.Enumerations;
using Axis.Events;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public Light spotLight;
    public int nodeIndex = 0;

    private void OnEnable()
    {
        AxisEvents.OnAxisOutputDataUpdated += HandleOnAxisDataUpdated;
    }

    private void OnDisable()
    {
        AxisEvents.OnAxisOutputDataUpdated -= HandleOnAxisDataUpdated;

    }

    private void HandleOnAxisDataUpdated(AxisOutputData axisOutputData)
    {
        Quaternion nodeRotation = axisOutputData.nodesDataList[nodeIndex].rotation;
        Vector3 euler = nodeRotation.eulerAngles;

        float sin = Mathf.Cos(euler.y / 2 * Mathf.Deg2Rad);
        float intensity = Mathf.Abs(sin);
        spotLight.intensity = intensity * 50f;
    }

}
