using Axis.DataTypes;
using Axis.Elements;
using Axis.Events;
using System;
using UnityEngine;
using TMPro;
using Axis.Enumerations;
using Axis.Interfaces;

namespace Axis.Tutorials.Elements
{
    public class AxisTutorialNode: AxisNode, IAxisDataSubscriber<AxisOutputData>
    {
        #region Tutorial Related 
        public static Action<AxisTutorialNode, int> OnNodeSelected;

        [HideInInspector] public Material selectedMaterial;
        [HideInInspector] public Material notSelectedMaterial;
        [HideInInspector] public int nodeIndex;


        #endregion

        private AxisBrain connectedBrain;

        protected override void OnEnable()
        {
            base.OnEnable();
            OnNodeSelected += HandleOnNodeSelected;
            NodeBinding = (NodeBinding)nodeIndex;

            connectedBrain = connectedBrain ==null? AxisBrain.FetchBrainOnScene() : connectedBrain;
            connectedBrain.masterAxisBroker.RegisterSubscriber(0,this);
            
        }

        private void OnDisable()
        {
            OnNodeSelected -= HandleOnNodeSelected;

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

        public void OnChanged(AxisOutputData data)
        {
            SetAcceleration(data.nodesDataList[nodeIndex].accelerations);
            SetRotation(data.nodesDataList[nodeIndex].rotation);
        }
    }
}
