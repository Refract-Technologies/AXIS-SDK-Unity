using Axis.DataProcessing;
using Axis.Enumerations;
using System.Collections.Generic;
using UnityEngine;
using Axis.ScriptableObjects;
using Axis.DataTypes;
using System;
using Axis.Interfaces;
using Refract.AXIS;
using Axis.Utils;

namespace Axis.Elements.FreeNodes
{

    public class NodeObjectsProcessor : NodeProcessor, IAxisDataSubscriber<AxisOutputData>
    {
        public GameObject objectPrefab;    
        public List<NodeObject> freeNodesObjects;
        public MotionDetectionParameters motionDetectionParameters;
        // Start is called before the first frame update

        // Update is called once per frame

        public Dictionary<NodeBinding, NodeObject> nodeObjects;
        private AxisBrain connectedBrain;
        public override void Initialize(string brainUniqueId)
        {
            connectedBrain = connectedBrain == null ? AxisBrain.FetchBrainOnScene() : connectedBrain;
            if (objectPrefab != null)
            {
                freeNodesObjects = new List<NodeObject>();
                nodeObjects = new Dictionary<NodeBinding, NodeObject>();
                var freeNodeObject = Instantiate(objectPrefab);
                freeNodeObject.transform.parent = transform;
                NodeObject freeNodeObjectAxisNode = freeNodeObject.GetComponent<NodeObject>();
                //freeNodeObjectAxisNode.runtimeCorrectionOffsets = runtimeCalibrationOffsets;
                freeNodeObjectAxisNode.motionDetectionParameters = motionDetectionParameters;
                freeNodeObjectAxisNode.SetNodeBinding(NodeBinding.FreeNode);
                freeNodesObjects.Add(freeNodeObject.GetComponent<NodeObject>());
                nodeObjects.Add(NodeBinding.FreeNode, freeNodeObjectAxisNode);

            }

            
            //if (NodeArrangement == NodeArrangement.LeftUpperArmAsGrabbableNode)
            //{

            //}
        }

        public void OnChanged(AxisOutputData data)
        {
            Dictionary<NodeBinding, AxisNodeData> nodeObjectsData;
            nodeObjectsData = NodesDataSupplier.GetNodeObjecstData(data, connectedBrain.nodeBindings.nodeBindings);

            if (nodeObjectsData != null && nodeObjectsData.Count > 0)
            {
                UpdateNodesData(nodeObjectsData);
            }
        }

        internal void UpdateNodesData(List<AxisNodeData> freeNodesDataList)
        {
            //Debug.Log($"Will update a list of {freeNodesDataList.Count}");
            //Debug.Log($"The freenodes objects count is {freeNodesObjects.Count}");
            for (int i = 0; i < freeNodesDataList.Count; i++)
            {
                freeNodesObjects[i].SetRotation(AxisDataUtility.ConvertRotationBasedOnKey(NodeBinding.FreeNode, freeNodesDataList[i].rotation));
                freeNodesObjects[i].SetAcceleration(freeNodesDataList[i].accelerations);
            }
        }

        internal void UpdateNodesData(Dictionary<NodeBinding, AxisNodeData> nodeObjectsData)
        {
            foreach (NodeBinding key in nodeObjectsData.Keys)
            {
                if (nodeObjects != null && nodeObjects.ContainsKey(key))
                {
                    nodeObjects[key].SetRotation(AxisDataUtility.ConvertRotationBasedOnKey(NodeBinding.FreeNode, nodeObjectsData[key].rotation));
                    nodeObjects[key].SetAcceleration(nodeObjectsData[key].accelerations);

                }
                //Debug.Log($"Must update {key}");
            }
        }
    }
}


