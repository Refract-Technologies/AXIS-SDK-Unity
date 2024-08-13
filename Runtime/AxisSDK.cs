using AOT;
using Axis.Events;
using Axis.Native;
using System.Collections.Concurrent;
using UnityEngine;

namespace Refract
{
    namespace AXIS
    {        
        public class AxisSDK
        {
            protected AxisSDKConfig_t config = new AxisSDKConfig_t();
            public IMUDataPublisher imuDataPublisher = new IMUDataPublisher();
            public AxisOutputDataPublisher axisOutputDataPublisher = new AxisOutputDataPublisher();
            public DevpacketPublisher devpacketPublisher = new DevpacketPublisher();
            public SuitInfoPublisher suitInfoPublisher = new SuitInfoPublisher();
            public BattInfoPublisher battInfoPublisher = new BattInfoPublisher();
            public VersionsInfoPublisher versionsInfoPublisher = new VersionsInfoPublisher();
            public NodeConfigResponsePublisher nodeConfigPublisher = new NodeConfigResponsePublisher();
            public SetWifiResponsePublisher setWifiResponsePublisher = new SetWifiResponsePublisher();
            public NodesInfoPublisher nodesInfoPublisher = new NodesInfoPublisher();
            public WifiResponsePublisher wifiResponsePublisher = new WifiResponsePublisher();
            public DoubleClickPublisher doubleClickPublisher = new DoubleClickPublisher();
            public PairedNodesPublisher pairedNodesPublisher = new PairedNodesPublisher();
            public VisibleNodesPublisher visibleNodesPublisher = new VisibleNodesPublisher();

            public bool isRunning { get; set; }

            protected AxisRuntimeNative runtimeNative = new AxisRuntimeNative();
            private struct AxisPacketWithType
            {
                public AxisPacketTypes packetTypes;
                public AxisPacket_t packet;
                public AxisPacketWithType(AxisPacketTypes _packetType, AxisPacket_t _packet)
                {
                    packetTypes = _packetType; 
                    packet = _packet; 
                }
            }
            private struct AxisError
            {
                public AxisRuntimeErrors runtimeErrors;
                public int errorCode;
                public string errorString;
                public AxisError(AxisRuntimeErrors _runtimeErrors, int _errorCode, string _errorString)
                {
                    runtimeErrors = _runtimeErrors;
                    errorCode = _errorCode;
                    errorString = _errorString;
                }
            }

            private static ConcurrentQueue<AxisPacketWithType> axisPacketQueue                 = new ConcurrentQueue<AxisPacketWithType>();
            private static ConcurrentQueue<AxisHubStatus_t>    axisHubStatusQueue              = new ConcurrentQueue<AxisHubStatus_t>();

            private static ConcurrentQueue<AxisError>          runtimeErrorsCallbackQueue      = new ConcurrentQueue<AxisError>();
            private static ConcurrentQueue<AxisError>          pairingHubErrorsCallbackQueue   = new ConcurrentQueue<AxisError>();
            private static ConcurrentQueue<bool>               dongleconnectionCallbackQueue   = new ConcurrentQueue<bool>();

            private static ConcurrentQueue<AxisHubsInfo_t>     searchingHubQueue               = new ConcurrentQueue<AxisHubsInfo_t>();
            private static ConcurrentQueue<AxisHubsInfo_t>     getHubPageQueue                 = new ConcurrentQueue<AxisHubsInfo_t>();

            private static ConcurrentQueue<AxisError>          startHubCallbackQueue           = new ConcurrentQueue<AxisError>();
            private static ConcurrentQueue<AxisError>          stopHubCallbackQueue            = new ConcurrentQueue<AxisError>();
            private static ConcurrentQueue<AxisError>          resetHubCallbackQueue           = new ConcurrentQueue<AxisError>();
            protected void ExecuteAxisPackets()
            {
                while (axisPacketQueue.Count > 0)
                {
                    AxisPacketWithType packetWithType;
                    if (axisPacketQueue.TryDequeue(out packetWithType))
                    {
                        switch (packetWithType.packetTypes)
                        {

                            case AxisPacketTypes.IMUData:
                                imuDataPublisher?.PublishData(packetWithType.packet.imuData);
                                axisOutputDataPublisher?.PublishData(packetWithType.packet.imuData);

                                break;
                            case AxisPacketTypes.DevPacket:
                                devpacketPublisher?.PublishData(packetWithType.packet.devPacket);
                                break;
                            case AxisPacketTypes.SuitInfo:
                                suitInfoPublisher?.PublishData(packetWithType.packet.suitInfo);
                                break;
                            case AxisPacketTypes.Versions:
                                versionsInfoPublisher?.PublishData(packetWithType.packet.versions);
                                break;
                            case AxisPacketTypes.PairedNodes:
                                pairedNodesPublisher?.PublishData(packetWithType.packet.pairedNodesInfo);
                                break;
                            case AxisPacketTypes.DoubleClick:
                                doubleClickPublisher?.PublishData(packetWithType.packet.doubleClickNodesInfo);
                                break;
                            case AxisPacketTypes.VisibleNodes:
                                visibleNodesPublisher?.PublishData(packetWithType.packet.visibleNodesInfo);
                                break;
                            case AxisPacketTypes.NodeAssignAck:
                                nodesInfoPublisher?.PublishData(packetWithType.packet.nodesInfo);
                                break;
                            case AxisPacketTypes.SetWifiResponse:
                                setWifiResponsePublisher?.PublishData(packetWithType.packet.setWifiRes);
                                break;
                            case AxisPacketTypes.ReportWifi:
                                wifiResponsePublisher?.PublishData(packetWithType.packet.wifiRes);
                                break;

                            case AxisPacketTypes.NodeConfigResponse:
                                nodeConfigPublisher?.PublishData(packetWithType.packet.nodeConfigRes);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            protected void ExecuteAxisHubPairingCallback()
            {
                while(pairingHubErrorsCallbackQueue.Count > 0) 
                {
                    AxisError error;
                    if(pairingHubErrorsCallbackQueue.TryDequeue(out error)) 
                    {
                        AxisEvents.OnAxisHubPairingReceived?.Invoke(error.runtimeErrors, error.errorCode, error.errorString);
                    }
                }
            }
            protected void ExecuteHubStatusCallback()
            {
                while (axisHubStatusQueue.Count > 0)
                {
                    AxisHubStatus_t status;
                    if (axisHubStatusQueue.TryDequeue(out status))
                    {
                        AxisEvents.OnAxisHubGetStatusCallback?.Invoke(status);
                    }
                }
            }
            protected void ExecuteAxisErrorCallback()
            {
                while (runtimeErrorsCallbackQueue.Count > 0)
                {
                    AxisError error;
                    if (runtimeErrorsCallbackQueue.TryDequeue(out error))
                    {
                        AxisEvents.OnAxisRuntimeErrorReceived?.Invoke(error.runtimeErrors, error.errorCode, error.errorString);
                    }
                }
            }
            protected void ExecuteSearchingHubCallback()
            {
                while (searchingHubQueue.Count > 0)
                {
                    AxisHubsInfo_t info;
                    if (searchingHubQueue.TryDequeue(out info))
                    {
                        AxisEvents.OnAxisHubSearchingCallback?.Invoke(info);
                    }
                }
            }
            protected void ExecuteGetHubPageCallback()
            {
                while (getHubPageQueue.Count > 0)
                {
                    AxisHubsInfo_t info;
                    if (getHubPageQueue.TryDequeue(out info))
                    {
                        AxisEvents.OnGetHubInfoPageNumberCallback?.Invoke(info);
                    }
                }
            }
           
            protected void ExecuteDongleConnectionCallback()
            {
                while (dongleconnectionCallbackQueue.Count > 0)
                {
                    bool connect;
                    if (dongleconnectionCallbackQueue.TryDequeue(out connect))
                    {
                        AxisEvents.OnDongleConnected?.Invoke(connect);
                    }
                }
            }

            protected void ExecuteStartHubStreamingCallback()
            {
                while (startHubCallbackQueue.Count > 0)
                {
                    AxisError error;
                    if (startHubCallbackQueue.TryDequeue(out error))
                    {
                        AxisEvents.OnAxisHubStartStreamReceived?.Invoke(error.runtimeErrors, error.errorCode, error.errorString);
                    }
                }
            }

            protected void ExecuteStopHubStreamingCallback()
            {
                while (stopHubCallbackQueue.Count > 0)
                {
                    AxisError error;
                    if (stopHubCallbackQueue.TryDequeue(out error))
                    {
                        AxisEvents.OnAxisHubStopStreamReceived?.Invoke(error.runtimeErrors, error.errorCode, error.errorString);
                    }
                }
            }

            protected void ExecuteResetHubStreamingCallback()
            {
                while (resetHubCallbackQueue.Count > 0)
                {
                    AxisError error;
                    if (resetHubCallbackQueue.TryDequeue(out error))
                    {
                        AxisEvents.OnAxisHubResetStreamReceived?.Invoke(error.runtimeErrors, error.errorCode, error.errorString);
                    }
                }
            }
            /// <summary>
            /// Wrapper to Register all axis publisher to specific master axis broker
            /// Create subsciber and register to specific master axis broker in channel 0 to retrieve their data when there is an update
            /// @return Refract::AXIS::AxisRuntimeErrors::OK if no errors
            /// @return Refract::AXIS::AxisRuntimeErrors::SDKNotStarted if Start has not been called yet
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidIPAddress if ipAddress is invalid
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidParameter if port is invalid
            ///  </summary>
            public void RegisterAllAxisPublishersToAxisBroker(MasterAxisBroker axisBroker)
            {
                axisBroker.RegisterPublisher(imuDataPublisher);
                axisBroker.RegisterPublisher(axisOutputDataPublisher);
                axisBroker.RegisterPublisher(devpacketPublisher);
                axisBroker.RegisterPublisher(suitInfoPublisher);
                axisBroker.RegisterPublisher(battInfoPublisher);
                axisBroker.RegisterPublisher(versionsInfoPublisher);
                axisBroker.RegisterPublisher(nodeConfigPublisher);
                axisBroker.RegisterPublisher(wifiResponsePublisher);
                axisBroker.RegisterPublisher(doubleClickPublisher);
                axisBroker.RegisterPublisher(pairedNodesPublisher);
                axisBroker.RegisterPublisher(visibleNodesPublisher);

            }

            /// <summary>
            /// @brief Wrapper to Start the SDK with a prefixed config
            ///  </summary>
            /// @return Refract::AXIS::AxisRuntimeErrors::OK if no errors
            /// @return Refract::AXIS::AxisRuntimeErrors::SDKNotStarted if Start has not been called yet
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidIPAddress if ipAddress is invalid
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidParameter if port is invalid
            public AxisRuntimeErrors StartSDK()
            {
                config.dongleConnectionCallback = DongleConnectionCallback;
                config.packetCallback = HandlePacket;
                config.runtimeErrorCallback = HandleAxisError;
                AxisRuntimeErrors result = AxisAPI.SetConfig(config);


                if (result != AxisRuntimeErrors.OK)
                {
                    Debug.LogError("AxisRuntime Error encounter when setting up config: " + result);
                    return result;
                }
#if UNITY_ANDROID && !UNITY_EDITOR
                runtimeNative.Start(); 
#endif
                result = AxisAPI.Start();
                if (result != AxisRuntimeErrors.OK)
                {
                    Debug.LogError("AxisRuntime Error encounter when starting Axis SDK: " + result);
                    return result;
                }
                isRunning = true;
                return result;
            }

            /// <summary>
            /// Enables AxisSDK Publish out informations received from C++ dll axis packets and callbacks.
            /// Inherit from IAxisSubscriber<T> and subscribe to your broker to get the relevant data 
            /// </summary>
            public void Update()
            {
                ExecuteAxisPackets();
                ExecuteAxisHubPairingCallback();
                ExecuteAxisErrorCallback();
                ExecuteHubStatusCallback();
                ExecuteDongleConnectionCallback();

                ExecuteAxisHubPairingCallback();
                ExecuteStartHubStreamingCallback();
                ExecuteStopHubStreamingCallback();
                ExecuteResetHubStreamingCallback();
                ExecuteGetHubPageCallback();                
            }

            /// <summary>
            /// Wrapper to stop the SDK. 
            /// </summary>
            public void StopSDK()
            {
                axisPacketQueue.Clear();
                axisHubStatusQueue.Clear();


                runtimeErrorsCallbackQueue.Clear();
                pairingHubErrorsCallbackQueue.Clear();
                dongleconnectionCallbackQueue.Clear();

                searchingHubQueue.Clear();
                getHubPageQueue.Clear();

                startHubCallbackQueue.Clear();
                stopHubCallbackQueue.Clear();
                resetHubCallbackQueue.Clear();

                AxisAPI.Stop();
                isRunning = false;
            }

            /// <summary>
            /// Wrapper to Pair With Axis Hub. 
            /// Use AxisEvents.OnAxisHubPairingReceived to receive the pairing callback info such as error.
            /// </summary>
            /// <param name="hubIP">hub to connect to. Enter Hub IP is shown on Axis Hub </param>
            /// <param name="port"> Default to 8080</param>
            /// @return Refract::AXIS::AxisRuntimeErrors::OK if no errors
            /// @return Refract::AXIS::AxisRuntimeErrors::SDKNotStarted if Start has not been called yet
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidIPAddress if ipAddress is invalid
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidParameter if port is invalid
            public AxisRuntimeErrors PairWithHub(string hubIP,int port = 8080)
            {
                var result = AxisAPI.PairWithHub(hubIP, port, AxisHubPairingCallback);
                if(result != AxisRuntimeErrors.OK)
                {
                    Debug.LogError("PairWithHub Error Encountered: " + result);
                }
                return result;
            }
            /// <summary>
            /// Wrapper to AxisAPI.PairWithHub and AxisAPI.StartHubStream.  
            /// Use AxisEvents.OnAxisHubPairingReceived to receive the pairing callback info such as error.
            /// Use AxisEvents.OnAxisHubStartStreamReceived to received StartStream callback info such as error.
            /// </summary>
            /// <param name="hubIP">hub to connect to. Enter Hub IP is shown on Axis Hub </param>
            /// <param name="port"> Default to 8080</param>
            /// @return Refract::AXIS::AxisRuntimeErrors::OK if no errors
            /// @return Refract::AXIS::AxisRuntimeErrors::SDKNotStarted if Start has not been called yet
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidIPAddress if ipAddress is invalid
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidParameter if port is invalid
            public AxisRuntimeErrors PairAndStartHub(string hubIP, int port=8080) 
            {
                AxisRuntimeErrors result = PairWithHub(hubIP, port);
                if(result == AxisRuntimeErrors.OK)
                {
                    StartHubStream();
                }
                return result;
            }           

            /// <summary>
            /// Wrapper to AxisAPI.StartHubStreaming
            /// Use AxisEvents.OnAxisHubStartStreamReceived to received StartStream callback info such as error.
            /// </summary>
            /// @return Refract::AXIS::AxisRuntimeErrors::OK if no errors
            /// @return Refract::AXIS::AxisRuntimeErrors::SDKNotStarted if Start has not been called yet
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidIPAddress if ipAddress is invalid
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidParameter if port is invalid
            public AxisRuntimeErrors StartHubStream()
            {
                var result = AxisAPI.StartHubStream(AxisHubStartStreamCallback);
                if (result != AxisRuntimeErrors.OK)
                {
                    Debug.LogError("HubStartHubStream Error Encountered: " + result);
                }
                return result;
            }
            /// <summary>
            /// Wrapper to AxisAPI.StopHubStream
            /// Use AxisEvents.OnAxisHubStopStreamReceived to received StopStream callback info such as error.
            /// </summary>
            /// @return Refract::AXIS::AxisRuntimeErrors::OK if no errors
            /// @return Refract::AXIS::AxisRuntimeErrors::SDKNotStarted if Start has not been called yet
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidIPAddress if ipAddress is invalid
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidParameter if port is invalid
            public AxisRuntimeErrors StopHubStream() 
            {
                var result = AxisAPI.StopHubStream(AxisHubStopStreamCallback);
                if (result != AxisRuntimeErrors.OK)
                {
                    Debug.LogError("HubStartHubStream Error Encountered: " + result);
                }
                return result;
            }

            /// <summary>
            /// Wrapper to AxisAPI.ResetHubStreaming to reset hub position and rotation
            /// Use AxisEvents.OnAxisHubStopStreamReceived to received StopStream callback info such as error.
            /// </summary>
            /// @return Refract::AXIS::AxisRuntimeErrors::OK if no errors
            /// @return Refract::AXIS::AxisRuntimeErrors::SDKNotStarted if Start has not been called yet
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidIPAddress if ipAddress is invalid
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidParameter if port is invalid
            public AxisRuntimeErrors ResetHubStream()
            {
                var result = AxisAPI.ResetHubStream(AxisHubResetStreamCallback);
                if (result != AxisRuntimeErrors.OK)
                {
                    Debug.LogError("ResetHubStream Error Encountered: " + result);
                }
                return result;
            }
            /// <summary>
            /// Wrapper to AxisAPI.SearchForAxisHub
            /// Use AxisEvents.OnAxisHubSearchingCallback to received AxisHub callback info such as AxisHubsInfo_t.
            /// </summary>
            /// @return Refract::AXIS::AxisRuntimeErrors::OK if no errors
            /// @return Refract::AXIS::AxisRuntimeErrors::SDKNotStarted if Start has not been called yet
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidIPAddress if ipAddress is invalid
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidParameter if port is invalid
            public AxisRuntimeErrors SearchForAxisHub()
            {
                AxisRuntimeErrors result = AxisAPI.SearchForAxisHub(AxisHubSearchingCallback);
                if (result != AxisRuntimeErrors.OK)
                {
                    Debug.LogError("AxisRuntime Error encounter when SearchForAxisHub: " + result);                    
                }
                return result;
            }
            /// <summary>
            /// Wrapper to AxisAPI.GetHubStatus
            /// Use AxisEvents.OnAxisHubSearchingCallback to received AxisHub callback info such as AxisHubsInfo_t.
            /// </summary>
            /// @return Refract::AXIS::AxisRuntimeErrors::OK if no errors
            /// @return Refract::AXIS::AxisRuntimeErrors::SDKNotStarted if Start has not been called yet
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidIPAddress if ipAddress is invalid
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidParameter if port is invalid
            public AxisRuntimeErrors GetHubStatus()
            {
                AxisRuntimeErrors result = AxisAPI.GetHubStatus(GetAxisHubStatusCallback);
                if(result != AxisRuntimeErrors.OK)
                {
                    Debug.LogError("AxisRuntime Error encounter when GetHubStatus: " + result);
                }
                return result;
            }
            /// <summary>
            /// Wrapper to AxisAPI.GetHubInfoPageNumber
            /// Use AxisEvents.OnGetHubInfoPageNumberCallback to received AxisHub callback info such as AxisHubsInfo_t.
            /// </summary>
            /// @return Refract::AXIS::AxisRuntimeErrors::OK if no errors
            /// @return Refract::AXIS::AxisRuntimeErrors::SDKNotStarted if Start has not been called yet
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidIPAddress if ipAddress is invalid
            /// @return Refract::AXIS::AxisRuntimeErrors::InvalidParameter if port is invalid
            public AxisRuntimeErrors GetHubInfoPageNumber(byte page)
            {
                AxisRuntimeErrors result = AxisAPI.GetHubInfoPageNumber(page, AxisHubGetPageCallback);
                if(result != AxisRuntimeErrors.OK)
                {
                    Debug.LogError("AxisRuntime Error encounter when GetHubInfoPageNumber: " + result);
                }
                return result;
            }
            [MonoPInvokeCallback(typeof(AxisRuntimeErrorCallback))]
            private static void AxisHubPairingCallback(AxisRuntimeErrors runtimeErrors, int errorCode, string errorString)
            {
                AxisError error = new AxisError(runtimeErrors, errorCode, errorString);
                pairingHubErrorsCallbackQueue.Enqueue(error);
            }

            [MonoPInvokeCallback(typeof(AxisHubInfoCallback))]
            private static void AxisHubSearchingCallback(in AxisHubsInfo_t axisHubsInfo)
            {
                searchingHubQueue.Enqueue(axisHubsInfo);              
            }
            [MonoPInvokeCallback(typeof(AxisHubInfoCallback))]
            private static void AxisHubGetPageCallback(in AxisHubsInfo_t axisHubsInfo)
            {
                getHubPageQueue.Enqueue(axisHubsInfo);
            }

            [MonoPInvokeCallback(typeof(AxisRuntimeErrorCallback))]
            private static void HandleAxisError(AxisRuntimeErrors runtimeErrors, int errorCode, string errorString)
            {
                AxisError error = new AxisError(runtimeErrors, errorCode, errorString);
                runtimeErrorsCallbackQueue.Enqueue(error);
            }

            [MonoPInvokeCallback(typeof(AxisDongleConnectionCallback))]
            private static void DongleConnectionCallback(bool connected)
            {
                dongleconnectionCallbackQueue.Enqueue(connected);
            }
            [MonoPInvokeCallback(typeof(AxisPacketCallback))]
            private static void HandlePacket(AxisPacketTypes packetType, in AxisPacket_t incomingPacket)
            {
                AxisPacketWithType pack = new AxisPacketWithType(packetType, incomingPacket);
                axisPacketQueue.Enqueue(pack);
            }

            [MonoPInvokeCallback(typeof(AxisRuntimeErrorCallback))]
            private static void AxisHubStartStreamCallback(AxisRuntimeErrors runtimeErrors, int errorCode, string errorString)
            {
                AxisError error = new AxisError(runtimeErrors, errorCode, errorString);
                startHubCallbackQueue.Enqueue(error);
            }
            [MonoPInvokeCallback(typeof(AxisRuntimeErrorCallback))]
            private static void AxisHubStopStreamCallback(AxisRuntimeErrors runtimeErrors, int errorCode, string errorString)
            {
                AxisError error = new AxisError(runtimeErrors, errorCode, errorString);
                stopHubCallbackQueue.Enqueue(error);
            }
            [MonoPInvokeCallback(typeof(AxisRuntimeErrorCallback))]
            private static void AxisHubResetStreamCallback(AxisRuntimeErrors runtimeErrors, int errorCode, string errorString)
            {
                AxisError error = new AxisError(runtimeErrors, errorCode, errorString);
                resetHubCallbackQueue.Enqueue(error);
            }
            [MonoPInvokeCallback(typeof(AxisHubStatusCallback))]
            private static void GetAxisHubStatusCallback(in AxisHubStatus_t hubStatus)
            {
                axisHubStatusQueue.Enqueue(hubStatus);
            }

        }

    }
}
