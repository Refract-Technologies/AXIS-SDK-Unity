
using Axis.DataTypes;

using Axis.Events;
using UnityEngine;

//A class to demo the bare minimun for retrieving node Rotation Data
public class RotationFromAxisNode : MonoBehaviour
{  
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
        
        transform.rotation = axisOutputData.nodesDataList[nodeIndex].rotation;
    }
}
