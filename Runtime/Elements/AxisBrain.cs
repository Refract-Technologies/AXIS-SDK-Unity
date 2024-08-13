using Axis.Enumerations;
using Axis.Elements.FreeNodes;
using Axis.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using Axis.DataTypes;
using Axis.Utils;
using Axis.Bindings;
using Axis.Interfaces;
using Refract.AXIS;
using UnityEditor;
using AOT;
using Axis.Communication.Commander;

namespace Axis.Elements
{

 

    //Axis Brain is the center of the Axis System. It holds references to:
    //Axis Mannequin - A reference humanoid that replicates the joints angles of the person wearing Axis
    //NodeObjectsProcessor - If you are using Axis Nodes by holding it to your hand or attaching to an object
    //Output Characters - Any character that you want to move as the Axis Mannequin + Absolute Tracking

    [RequireComponent(typeof(AxisBrainNodeBindings)), ExecuteAlways, Serializable]
    public class AxisBrain : AxisRequiringElements
    {

        #region Class Variables
        [HideInInspector] public string uniqueID = null;
        public AxisMannequin axisMannequin;
        [HideInInspector] public AxisNodesRepresentation axisNodesRepresentation;
        [HideInInspector] public NodeObjectsProcessor nodeObjectsHandler;
        [HideInInspector] public AxisBrainNodeBindings nodeBindings;
        [HideInInspector] public AxisRuntimeCommander runtimeCommander;
        public HipProvider hipProvider;
        
        [SerializeField, HideInInspector] public bool isMannequinVisible;
        [SerializeField, HideInInspector] public bool isNodesRepresentationVisible = false;
       
        public MasterAxisBroker masterAxisBroker = new MasterAxisBroker();

        
        public AxisHubLocation axisHubLocations = AxisHubLocation.HubLocationBack;
        [Header("Debug Axis Hub IP")]
        public string axisHubIP;
        public int axisHubPort = 8080;

        public AxisSDK axisSDK = new AxisSDK();
        #endregion

        #region Static Utils

        public static AxisBrain FetchBrainOnScene()
        {
            AxisBrain[] axisBrainOnScene = GameObject.FindObjectsOfType<AxisBrain>();

            if (axisBrainOnScene.Length == 1)
            {
                return axisBrainOnScene[0];
            }
            else
            {
                string warningMessage = axisBrainOnScene.Length == 0 ?
                    "Automatic brain linking failed. No axis brain found on scene" :
                    "Automatic brain linking failed. More then one Axis Brain on the scene. Please, assign mannually";

                Debug.LogWarning(warningMessage);
            }

            return null;
        }

        #endregion

        #region Initialization

        void Start()
        {
#if UNITY_EDITOR
            AssemblyReloadEvents.beforeAssemblyReload += AxisAPI.Stop;
            AssemblyReloadEvents.afterAssemblyReload -= AxisAPI.Stop;
#endif
            if (Application.isPlaying && !axisSDK.isRunning)
            {               
                axisSDK.StartSDK();  
            }
        }
        

        protected override void OnEnable()
        {
            base.OnEnable();

            uniqueID = string.IsNullOrEmpty(uniqueID) == true ? Guid.NewGuid().ToString() : uniqueID;
            
            GetReferences();
            ResetPosition();
            SetupAxisNodesRepresentation();
            SetupAxisMannequin();
            SetupFreeNodesHandler();
            RegisterCallbacks();
            SetVisibility();
        }


        
        private void SetupAxisNodesRepresentation()
        {
            axisNodesRepresentation.Initialize(uniqueID);
        }
        private void GetReferences()
        {
            nodeBindings = GetComponent<AxisBrainNodeBindings>();
            runtimeCommander = GetComponent<AxisRuntimeCommander>();
        }

        public void SetVisibility()
        {
            axisNodesRepresentation.SetVisibility(isNodesRepresentationVisible);
            axisMannequin.SetVisibility(isMannequinVisible);
        }

        #region InitializationUtils
        private void ResetPosition()
        {
            transform.position = Vector3.zero;
        }



        #endregion

        #region ObjectsSetup
        private void SetupFreeNodesHandler()
        {
            nodeObjectsHandler = GetComponentInChildren<NodeObjectsProcessor>();
            nodeObjectsHandler.transform.parent = transform;
            nodeObjectsHandler.transform.parent = transform;
            nodeObjectsHandler.Initialize(uniqueID);
        }

        private void SetupAxisMannequin()
        {
            axisMannequin.Initialize(uniqueID);
        }

        #endregion

        private void Update()
        {
            axisSDK.Update();
        }


        #endregion

        #region Callbacks

        private void RegisterCallbacks()
        {
            axisSDK.RegisterAllAxisPublishersToAxisBroker(masterAxisBroker);
            masterAxisBroker.RegisterSubscriber(0, nodeObjectsHandler);

            //AxisAPI.SendNodeInfoCommand(NodeInfoCommandPayload_t.NodeInfoCommandModes.Visible);
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            AssemblyReloadEvents.afterAssemblyReload -= AxisAPI.Stop;
#endif
            masterAxisBroker?.Cleanup();
            axisSDK.StopSDK();
        }
        private void OnDestroy()
        {
#if UNITY_EDITOR
            AssemblyReloadEvents.afterAssemblyReload -= AxisAPI.Stop;
#endif
            masterAxisBroker?.Cleanup();
            axisSDK.StopSDK();
        }
        #endregion



    }
}

