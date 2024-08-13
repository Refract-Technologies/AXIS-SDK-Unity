using Axis.DataTypes;
using Axis.Elements;
using Axis.Enumerations;
using Axis.Interfaces;
using UnityEngine;

//A class to demo the bare minimun for retrieving node Rotation Data
public class RotationFromAxisNode : MonoBehaviour,IAxisDataSubscriber<AxisOutputData>
{  
    public NodeBinding nodeIndex = 0;
    private AxisBrain connectedBrain;
   
    private void OnEnable()
    {
        connectedBrain = connectedBrain == null ? AxisBrain.FetchBrainOnScene() : connectedBrain;
        connectedBrain.masterAxisBroker.RegisterSubscriber(0, this);
    }

    private void OnDisable()
    {
        connectedBrain.masterAxisBroker.DeregisterSubscriber(0, this);        
    }
    public void OnChanged(AxisOutputData data)
    {
        transform.rotation = data.nodesDataList[(int)nodeIndex].rotation;
    }
}
