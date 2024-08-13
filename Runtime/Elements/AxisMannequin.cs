using Axis.DataProcessing;
using Axis.Enumerations;
using Axis.Elements.AnimatorLink;
using System;
using System.Collections.Generic;
using UnityEngine;
using Axis.DataTypes;
using Axis.Events;
using Axis.Solvers;
using Axis.Overrides;
using Axis.Interfaces;
using Axis.Utils;

namespace Axis.Elements
{

    //Axis Mannequin purpose is to be a humanoid character that don't interact directly to the 
    //world, but serves as a model for any application using Axis as a body tracking system for example
    //the class CharacterAnimatorLink uses the AxisMannequin data to animate any humanoid character.
    public class AxisMannequin : NodeProcessor,IAxisDataSubscriber<AxisOutputData>, IAxisDataPublisher<AxisNodesRepresentation>
    {
        #region Class Variables


        public GameObject bodyModel;        
        public BodyModelAnimatorLink bodyModelAnimatorLink;

        public Action<AxisHubData> onHubDataUpdated;
        private HipsRotationSolver hipsRotationSolver;
        private TorsoRotationSolver torsoRotationSolver;
        private AxisBrain connectedBrain;
        private HipProvider hipProvider;
        #endregion

        #region Initialization
        public override void Initialize(string uniqueID)
        {
            brainUniqueId = uniqueID;
            connectedBrain = connectedBrain == null ? AxisBrain.FetchBrainOnScene() : connectedBrain;
            connectedBrain.masterAxisBroker.RegisterSubscriber(0, this);
            connectedBrain.masterAxisBroker.RegisterPublisher(this);
            ExecuteOverrides();
            SetupBodyModelAnimationLink(bodyModelAnimatorLink);
            LoadSolvers();
        }

        private void ExecuteOverrides()
        {
            foreach (AxisExecuteOnStartOverride axisOverride in GetComponents<AxisExecuteOnStartOverride>())
            {
                axisOverride.Execute();
            }
            
        }


        //Solvers are components that use specific strategies to handle the kinematic interpretation of the Axis Nodes data
        protected virtual void LoadSolvers()
        {
            hipsRotationSolver = GetComponent<HipsRotationSolver>() == null ? 
                gameObject.AddComponent<MannequinHipsFollowSpineSolver>() : 
                GetComponent<HipsRotationSolver>();

            torsoRotationSolver = GetComponent<TorsoRotationSolver>() == null ?
                gameObject.AddComponent<TorsoRotationFromAxisSolver>() :
                GetComponent<TorsoRotationSolver>();
        }

        #region Nodes Instantiation
        

        public void SetVisibility(bool isVisible)
        {
           foreach (Renderer renderer in bodyModel.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = isVisible;
            }
        }


        #endregion


        #region ObjectsSetup
        

        public void SetupBodyModelAnimationLink(BodyModelAnimatorLink bodyModelAnimatorLink)
        {
            bodyModelAnimatorLink.Animator = bodyModelAnimatorLink.transform.GetComponent<Animator>();
            bodyModelAnimatorLink.FetchTransformsRelatedToNodeLimbs();
            bodyModelAnimatorLink.GoToTPose();
            bodyModelAnimatorLink.StoreDefaultTransformsRotations();
        }

        #endregion

        #endregion

        #region DataUpdateHandling

		
        internal void UpdateNodesData(Dictionary<NodeBinding, AxisNodeData> nodesDataDictionary, Dictionary<NodeBinding, AxisNode> nodesByLimb)
        {
            foreach (var key in nodesDataDictionary.Keys)
            {
                if (nodesDataDictionary[key].isActive == true)
                {
                    nodesByLimb[key].SetRotation(AxisDataUtility.ConvertRotationBasedOnKey(key, nodesDataDictionary[key].rotation));
                    nodesByLimb[key].SetAcceleration(nodesDataDictionary[key].accelerations);
                    nodesByLimb[key].Active = nodesDataDictionary[key].isActive;
                }
            }
        }

        internal void UpdateHubData(AxisHubData hubData, Dictionary<NodeBinding, AxisNode> nodesByLimb)
        {
            //This callback is used by Solvers to update data
           
            onHubDataUpdated?.Invoke(hubData);
           // nodesByLimb[NodeBinding.WaistHub].SetRotation(AxisDataUtility.ConvertRotationBasedOnKey(NodeBinding.WaistHub, hubData.rotation));
               
        }
        internal void UpdateTorso(Dictionary<NodeBinding, AxisNode> nodesByLimb)
        {
            //nodesByLimb[NodeBinding.WaistHub].SetRotation(Quaternion.identity);


        }
        
        internal void UpdateHips(Dictionary<NodeBinding, AxisNode> nodesByLimb)
        {
           
        }


        public Action<BodyModelAnimatorLink> onBodyModelAnimatorLinkUpdated;

        public event PublishAxisData<AxisNodesRepresentation> OnPublishData;

        //After generating the pose by modifying the rotations on the BodyModelAnimatorLink, iterate through all the characters
        //and update the respectives transforms for mirroring the pose of the mannequin.
        public virtual void HandleNodeDataUpdated(Dictionary<NodeBinding, AxisNode> nodesByLimb)
        {
            AxisEvents.OnNodeByLimbsUpdated?.Invoke(brainUniqueId, nodesByLimb);
            bodyModelAnimatorLink.UpdateTransforms(nodesByLimb);    
            onBodyModelAnimatorLinkUpdated?.Invoke(bodyModelAnimatorLink);
            
        }
        public virtual void HandleAxisDataUpdated(Dictionary<NodeBinding, AxisNode> nodesByLimb,HipProvider hipProvider,AxisHubData axisHubData)
        {
            AxisEvents.OnNodeByLimbsUpdated?.Invoke(brainUniqueId, nodesByLimb);
            bodyModelAnimatorLink.UpdateHipTransform(nodesByLimb,hipProvider, axisHubData);
            bodyModelAnimatorLink.UpdateTransforms(nodesByLimb);
            onBodyModelAnimatorLinkUpdated?.Invoke(bodyModelAnimatorLink);
        }

        public void OnChanged(AxisOutputData data)
        {
            Dictionary<NodeBinding, AxisNodeData> mannequinNodesData;
            AxisNodesRepresentation representation = connectedBrain.axisNodesRepresentation;
            hipProvider = connectedBrain.hipProvider;
            //connectedBrain.activeNodesDetector.UpdateActiveNodes(data);
            mannequinNodesData = NodesDataSupplier.GetMannequinNodesData(data, connectedBrain.nodeBindings.nodeBindings);
            if (mannequinNodesData != null && mannequinNodesData.Count > 0)
            {
                UpdateHubData(data.hubData, representation.nodesByLimb);
                UpdateNodesData(mannequinNodesData, representation.nodesByLimb);
                UpdateTorso(representation.nodesByLimb);
                UpdateHips(representation.nodesByLimb);
                HandleAxisDataUpdated(representation.nodesByLimb, hipProvider,data.hubData);
                OnPublishData?.Invoke(0, representation);
            }
        }
        // public virtual void HandleHubDataUpdated()

        #endregion


    }

}



