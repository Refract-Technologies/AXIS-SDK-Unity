using Axis.DataTypes;
using Axis.Elements;
using Axis.Events;
using System;
using UnityEngine;
using TMPro;
using Axis.Enumerations;

namespace Axis.Tutorials.Elements
{
    public class AxisTutorialNode: AxisNode
    {
        #region Tutorial Related 
        public static Action<AxisTutorialNode, int> OnNodeSelected;

        [HideInInspector] public Material selectedMaterial;
        [HideInInspector] public Material notSelectedMaterial;
        [HideInInspector] public int nodeIndex;

        
        #endregion







        protected override void OnEnable()
        {
            base.OnEnable();
            OnNodeSelected += HandleOnNodeSelected;
            NodeBinding = (NodeBinding)nodeIndex;
            //IMPORTANT FOR DEVS!!!
            //AxisEvents.OnAxisOutputDataUpdate Action is raised everytime the node has new data coming in
            //Check the implementation below for more information
            AxisEvents.OnAxisOutputDataUpdated += HandleOnAxisOutputDataUpdated;

            
        }

        private void OnDisable()
        {
            OnNodeSelected += HandleOnNodeSelected;

            //IMPORTANT FOR DEVS!!!
            //Its a good practice to remove callbacks whenever the game object is disabled
            AxisEvents.OnAxisOutputDataUpdated -= HandleOnAxisOutputDataUpdated;
        }

        //IMPORTANT FOR DEVS!!!
        //This method is triggered by the action and it's executed everytime the node has new data available
        //AxisNodesOutputData nodesData.nodesDataList[index] contains all the data (Rotation and Acceleration) for the node at the respective index
        private void HandleOnAxisOutputDataUpdated(AxisOutputData nodesData)
        {
            SetAcceleration(nodesData.nodesDataList[nodeIndex].accelerations);
            SetRotation(nodesData.nodesDataList[nodeIndex].rotation);
        }

        private void HandleOnNodeSelected(AxisTutorialNode demoNode, int _nodeIndex)
        {
            if(nodeIndex == _nodeIndex)
            {
                HandleIsSelected(true);
            }
            else
            {
                HandleIsSelected(false);
            }
        }

        private void HandleIsSelected(bool value)
        {
            GetComponentInChildren<Renderer>().material = value == true ? selectedMaterial : notSelectedMaterial;
        }



        private void OnMouseDown()
        {
            OnNodeSelected.Invoke(this, nodeIndex);
        }
    }
}
