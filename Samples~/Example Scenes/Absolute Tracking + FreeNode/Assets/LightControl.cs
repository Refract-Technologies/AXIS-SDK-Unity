using System.Collections;
using System.Collections.Generic;
using Axis.DataTypes;
using Axis.Elements;
using Axis.Enumerations;
using Axis.Events;
using Axis.Interfaces;
using Refract.AXIS;
using UnityEngine;

public class LightControl : MonoBehaviour,IAxisDataSubscriber<AxisOutputData>
{
    public Light spotLight;
    public NodeBinding nodeIndex = 0;

    private AxisBrain connectedBrain;
    public void Awake()
    {
        connectedBrain = connectedBrain == null ? AxisBrain.FetchBrainOnScene() : connectedBrain;
        connectedBrain.masterAxisBroker.RegisterSubscriber(0, this);
    }

    public void OnChanged(AxisOutputData data)
    {
        Quaternion nodeRotation = data.nodesDataList[(int)nodeIndex].rotation;
        Vector3 euler = nodeRotation.eulerAngles;

        float sin = Mathf.Cos(euler.y / 2 * Mathf.Deg2Rad);
        float intensity = Mathf.Abs(sin);
        spotLight.intensity = intensity * 50f;
    }
}
