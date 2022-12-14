using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Axis.Events;

namespace Axis._Editor.Styling
{
}

namespace Axis.Communication
{

    public class UdpSocket : MonoBehaviour
    {
        [HideInInspector] public bool isTxStarted = false;

        [SerializeField] string IP = "127.0.0.1"; // local host
        [SerializeField] int rxPort = 8000; // port to receive data from Python on
        [SerializeField] int txPort = 8001; // port to send data to Python on


        // Create necessary UdpClient objects
        UdpClient client;
        IPEndPoint remoteEndPoint;
        protected Thread receiveThread; // Receiving Thread
        protected byte[] dataInBytes;
        protected bool dataWaitingForProcessing = false;

        public void SendData(byte[] data)
        {
            try
            {
                //Debug.Log("Sending data");
                if (client != null)
                {
                    
                    client.Send(data, data.Length, remoteEndPoint);

                }
            }
            catch (Exception err)
            {
                if(err is ObjectDisposedException)
                {
                    //Debug.Log("Got it");
                }
                else
                {
                    print(err.ToString());
                }
                
            }
        }

        protected void StartReceiveThread()
        {
            remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), txPort);

            // Create local client
            client = new UdpClient(rxPort);
            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            
            // Create a new thread for reception of incoming messages
            receiveThread = new Thread(new ThreadStart(ReceiveData));
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }



        // Receive data, update packets received
        private void ReceiveData()
        {
            
            while (true && receiveThread!= null && receiveThread.IsAlive)
            {
                
                try
                {
                    IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                    dataInBytes = client.Receive(ref anyIP);

                    //Debug.Log(dataInBytes.Length);

                    dataWaitingForProcessing = true;

                    if (!isTxStarted) // First data arrived so tx started
                    {
                        isTxStarted = true;
                    }


                    //ProcessInput(data);

                }
                catch (Exception err)
                {
                    if(err is ThreadAbortException)
                    {
                        
                    } else
                    {
                        
                        print(err.ToString());
                    }
                    
                }
            }
        }



        protected virtual void ProcessInput(byte[] data)
        {
            dataWaitingForProcessing = true;

            string byteString = "Bytes: ";
            foreach (var item in data)
            {
                byteString += item.ToString() + " ";
            }

            Debug.Log("-----------------".ToString());
            Debug.Log(byteString);
            // PROCESS INPUT RECEIVED STRING HERE


        }

        //Prevent crashes - close clients and threads properly!


        protected void StopReceivingThread()
        {
            if (receiveThread != null)
            {
                receiveThread.Abort();
            }

            if (client != null)
            {
                client.Close();

            }
        }
    }
}
