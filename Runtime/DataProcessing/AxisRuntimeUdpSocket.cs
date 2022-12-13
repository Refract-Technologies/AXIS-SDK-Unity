using Axis.DataTypes;
using Axis.Events;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Axis.Communication
{
    
    [ExecuteAlways, Serializable]
    public class AxisRuntimeUdpSocket : UdpSocket
    {
        
        public static AxisRuntimeUdpSocket instance;
        AxisOutputData axisOutputData = new AxisOutputData();

        [SerializeField] public static bool shouldConnectToAxis = false;
        [SerializeField] public static bool isConnectedToAxis = false;
        
        //Data Packet Caracteristics
        private const int dataStartOffset = 6;
        private const int nodeIndexOfsset = 1;
        private const int dataSize = 15;
        private const int dataPacketSizeInBytes = 284;
        protected override void ProcessInput(byte[] data)
        {
            base.ProcessInput(data);
            //Debug.Log(input);

        }

        protected void Awake()
        {

            shouldConnectToAxis = false;
            if (instance == null)
            {
                transform.parent = Application.isPlaying == true ? null : transform.parent;              
                instance = this;
            }
            else
            {
                if(Application.isPlaying == true)
                {
                    Destroy(gameObject);
                } else
                {
                    DestroyImmediate(gameObject);
                }
            }

            
        }

        private void HandleUpdate()
        {
#if UNITY_EDITOR
            // Ensure continuous Update calls.
            if (!Application.isPlaying)
            {
                UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
                UnityEditor.SceneView.RepaintAll();
            }
#endif
        }

        private void Update()
        {
            NullThreadIfNotAlive();
            
            if (CheckIfNeedsToConnect())
            {
                
                StopReceivingThread();
                StartReceiveThread();
                isConnectedToAxis = true;
                AxisEvents.OnStartStreaming.Invoke();

            }
            else if (CheckIfNeedsToDisconnect())
            {
                StopReceivingThread();
                isConnectedToAxis = false;
            }

            if (isConnectedToAxis == true)
            {
                ProcessDataIfReceived();

                if (dataWaitingForProcessing == true)
                {
                    dataWaitingForProcessing = false;
                }

            }           
        }

        private void NullThreadIfNotAlive()
        {
            receiveThread = receiveThread == null ? null : receiveThread.IsAlive == true ? receiveThread : null;
        }

        private bool CheckIfNeedsToDisconnect()
        {
            return shouldConnectToAxis == false && isConnectedToAxis == true;
        }

        private bool CheckIfNeedsToConnect()
        {
            shouldConnectToAxis = Application.isPlaying && receiveThread == null ? true : shouldConnectToAxis;
            return (shouldConnectToAxis == true && receiveThread == null);
        }





        private void ProcessDataIfReceived()
        {
            if (dataInBytes != null && dataInBytes.Length == dataPacketSizeInBytes)
            {
                string dataInBytesString = "";
                for (int i = dataInBytes.Length - 28; i < dataInBytes.Length; i += 2)
                {
                    int value = BitConverter.ToInt16(dataInBytes, i);
                    dataInBytesString = string.Concat(dataInBytesString, " ", value.ToString());
                }
                
                GetDataFromHub(axisOutputData);
                GetDataFromNodes(axisOutputData);
                AxisEvents.OnAxisOutputDataUpdated?.Invoke(axisOutputData);
            }
        }

        private void GetDataFromNodes(AxisOutputData axisOutputData)
        {
            axisOutputData.nodesDataList = new List<AxisNodeData>();

            

            for (int i = 0; i < 16; i++)
            {
                float x = (BitConverter.ToInt16(dataInBytes, dataStartOffset + (i * dataSize) + 0 + nodeIndexOfsset)) * 0.00006103f;
                float z = (BitConverter.ToInt16(dataInBytes, dataStartOffset + (i * dataSize) + 2 + nodeIndexOfsset)) * 0.00006103f;
                float y = (BitConverter.ToInt16(dataInBytes, dataStartOffset + (i * dataSize) + 4 + nodeIndexOfsset)) * 0.00006103f;
                float w = (BitConverter.ToInt16(dataInBytes, dataStartOffset + (i * dataSize) + 6 + nodeIndexOfsset)) * 0.00006103f;

                float xAccel = BitConverter.ToInt16(dataInBytes, dataStartOffset + (i * dataSize) + 8 + nodeIndexOfsset) * 0.00390625f;
                float yAccel = BitConverter.ToInt16(dataInBytes, dataStartOffset + (i * dataSize) + 10 + nodeIndexOfsset) * 0.00390625f;
                float zAccel = BitConverter.ToInt16(dataInBytes, dataStartOffset + (i * dataSize) + 12 + nodeIndexOfsset) * 0.00390625f;
                Quaternion nodeQuaternion = new Quaternion(x, y, z, w);
                Vector3 acceleration = new Vector3(xAccel, yAccel, zAccel);
                
               
                AxisNodeData axisNodeData = new AxisNodeData
                {
                    rotation = nodeQuaternion,
                    accelerations = acceleration
                };
                
                axisOutputData.nodesDataList.Add(axisNodeData);
            }
        }

        private void GetDataFromHub(AxisOutputData axisOutputData)
        {
            int hubDataStartingPosition = dataInBytes.Length - 28;

            float x = BitConverter.ToSingle(dataInBytes, hubDataStartingPosition);
            float y = BitConverter.ToSingle(dataInBytes, hubDataStartingPosition + 4);
            float z = BitConverter.ToSingle(dataInBytes, hubDataStartingPosition + 8);
            float w = BitConverter.ToSingle(dataInBytes, hubDataStartingPosition + 12);
            Quaternion rotation = new Quaternion(x, y, z, w);

            float xPos = BitConverter.ToSingle(dataInBytes, hubDataStartingPosition + 16);
            float yPos = BitConverter.ToSingle(dataInBytes, hubDataStartingPosition + 20);
            float zPos = BitConverter.ToSingle(dataInBytes, hubDataStartingPosition + 24);

  
            axisOutputData.hubData.absolutePosition = new Vector3(-xPos, yPos, zPos);          
            axisOutputData.hubData.rotation = rotation;

        }

        private void OnEnable()
        {


#if UNITY_EDITOR
            EditorApplication.update -= HandleUpdate;
            EditorApplication.update += HandleUpdate;
#endif
        }

        protected virtual void OnDisable()
        {
            if(Application.isPlaying == true)
            {
                shouldConnectToAxis = false;
            }
            StopReceivingThread();
        }

     
    }
}
