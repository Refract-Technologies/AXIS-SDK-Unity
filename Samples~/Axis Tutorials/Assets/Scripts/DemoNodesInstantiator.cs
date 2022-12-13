using Axis.Elements;
using Axis.Tutorials.Elements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoNodesInstantiator : MonoBehaviour
{
    public GameObject nodePrefab;
    [Range(0, 1)] public float positionOffset;
    
    void Start()
    {
        for (int row = 0; row < 3; row++)   
        {
            for (int column = 0; column < 3; column++)
            {
                var node = Instantiate(nodePrefab);
                var nodeIndex = column + row * 3;
                AxisTutorialNode axisNode = node.GetComponent<AxisTutorialNode>();
                Debug.Log($"Creating node {nodeIndex}");

                axisNode.nodeIndex = nodeIndex;
                //axisNode.SetVisibility(true);
                node.transform.name = $"Node {nodeIndex}";
                node.transform.parent = transform;
                node.transform.localPosition = new Vector3(column * -positionOffset, row * -positionOffset, 0f);
            }
        }
    } 
}
