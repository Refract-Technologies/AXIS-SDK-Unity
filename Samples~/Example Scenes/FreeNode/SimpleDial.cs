using Axis.DataTypes;
using Axis.Elements;
using Axis.Enumerations;
using Axis.Events;
using Axis.Interfaces;
using UnityEngine;

public class SimpleDial : MonoBehaviour, IAxisDataSubscriber<AxisOutputData>
{
    public MeshRenderer ledMeshRenderer;
    int lastQuadrant;
    public AxisBrain connectedBrain;

    private void Awake()
    {
        connectedBrain = connectedBrain == null? AxisBrain.FetchBrainOnScene() : connectedBrain;
        connectedBrain.masterAxisBroker.RegisterSubscriber(0, this);
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

    public void OnChanged(AxisOutputData data)
    {
        Quaternion nodeRotation = data.nodesDataList[(int)NodeBinding.LeftThigh].rotation;
        Vector3 euler = nodeRotation.eulerAngles;
        transform.localEulerAngles = new Vector3(0f, euler.z, 0f);
    }
}
