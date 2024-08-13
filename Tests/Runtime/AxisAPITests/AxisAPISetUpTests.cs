using AOT;
using Axis.Events;
using NUnit.Framework;
using Refract.AXIS;
using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class AxisAPISetUpTests
{
    
    const float FLOAT_TEST_VALUE = 3.142f;
    const int INT_TEST_VALUE = 123456789;
    [MonoPInvokeCallback(typeof(AxisPacketCallback))]
    private void HandlePacket(AxisPacketTypes packetType, in AxisPacket_t incomingPacket)
    {
        switch (packetType) 
        {
            case AxisPacketTypes.IMUData:
                for(byte i =0; i < (byte)AxisNodePositions.NODE_INDEX_COUNT - 1; i++)
                {
                    Assert.AreEqual(true, incomingPacket.imuData.nodeImuData[(AxisNodePositions)i].on);
                    Assert.AreEqual(-FLOAT_TEST_VALUE, incomingPacket.imuData.nodeImuData[(AxisNodePositions)i].quat.x, Mathf.Epsilon);
                    Assert.AreEqual(FLOAT_TEST_VALUE, incomingPacket.imuData.nodeImuData[(AxisNodePositions)i].quat.y, Mathf.Epsilon);
                    Assert.AreEqual(-FLOAT_TEST_VALUE, incomingPacket.imuData.nodeImuData[(AxisNodePositions)i].quat.z, Mathf.Epsilon);
                    Assert.AreEqual(FLOAT_TEST_VALUE, incomingPacket.imuData.nodeImuData[(AxisNodePositions)i].quat.w, Mathf.Epsilon);
                    Assert.AreEqual(-FLOAT_TEST_VALUE, incomingPacket.imuData.nodeImuData[(AxisNodePositions)i].linAccel.x, Mathf.Epsilon);
                    Assert.AreEqual(FLOAT_TEST_VALUE, incomingPacket.imuData.nodeImuData[(AxisNodePositions)i].linAccel.y, Mathf.Epsilon);
                    Assert.AreEqual(-FLOAT_TEST_VALUE, incomingPacket.imuData.nodeImuData[(AxisNodePositions)i].linAccel.z, Mathf.Epsilon);

                    Assert.AreEqual(-FLOAT_TEST_VALUE,incomingPacket.imuData.hubData.quat.x, Mathf.Epsilon);
                    Assert.AreEqual(FLOAT_TEST_VALUE,incomingPacket.imuData.hubData.quat.y, Mathf.Epsilon);
                    Assert.AreEqual(-FLOAT_TEST_VALUE,incomingPacket.imuData.hubData.quat.z, Mathf.Epsilon);
                    Assert.AreEqual(FLOAT_TEST_VALUE, incomingPacket.imuData.hubData.quat.w, Mathf.Epsilon);
                }
                break;
            case AxisPacketTypes.DevPacket:
                Assert.AreEqual(-FLOAT_TEST_VALUE,incomingPacket.devPacket.accelerometer.data.x, Mathf.Epsilon);
                Assert.AreEqual(FLOAT_TEST_VALUE,incomingPacket.devPacket.accelerometer.data.y, Mathf.Epsilon);
                Assert.AreEqual(-FLOAT_TEST_VALUE, incomingPacket.devPacket.accelerometer.data.z, Mathf.Epsilon);

                Assert.AreEqual(-FLOAT_TEST_VALUE,incomingPacket.devPacket.gyroscope.data.x, Mathf.Epsilon);
                Assert.AreEqual(FLOAT_TEST_VALUE,incomingPacket.devPacket.gyroscope.data.y, Mathf.Epsilon);
                Assert.AreEqual(-FLOAT_TEST_VALUE, incomingPacket.devPacket.gyroscope.data.z, Mathf.Epsilon);

                Assert.AreEqual(-FLOAT_TEST_VALUE, incomingPacket.devPacket.magnetometer.data.x, Mathf.Epsilon);
                Assert.AreEqual(FLOAT_TEST_VALUE, incomingPacket.devPacket.magnetometer.data.y, Mathf.Epsilon);
                Assert.AreEqual(-FLOAT_TEST_VALUE, incomingPacket.devPacket.magnetometer.data.z, Mathf.Epsilon);


                Assert.AreEqual(1, incomingPacket.devPacket.accelerometer.acc);
                Assert.AreEqual(2, incomingPacket.devPacket.gyroscope.acc);
                Assert.AreEqual(3, incomingPacket.devPacket.magnetometer.acc);
                Assert.AreEqual(4, incomingPacket.devPacket.linearAccelerometer.acc);

                Assert.AreEqual(AxisNodePositions.RightThigh, incomingPacket.devPacket.nodeIndex);
                Assert.AreEqual(INT_TEST_VALUE, incomingPacket.devPacket.timeStamp); 

                break;
            case AxisPacketTypes.PairedNodes:
            case AxisPacketTypes.DoubleClick:
            case AxisPacketTypes.NodeAssignAck:
            case AxisPacketTypes.VisibleNodes:
                Assert.AreEqual((byte)AxisNodePositions.NODE_INDEX_COUNT - 1, incomingPacket.nodesInfo.nodeCount);
                for (byte i = 0; i < incomingPacket.nodesInfo.nodeCount; i++)
                {
                    Assert.AreEqual((AxisNodePositions)i, incomingPacket.nodesInfo.nodeInfo[i].nodeIndex);
                    Assert.AreEqual(true, incomingPacket.nodesInfo.nodeInfo[i].on);
                }
                for (byte i = 0; i < 6; i++)
                {
                    Assert.AreEqual(i, incomingPacket.nodesInfo.nodeInfo[i].mac[i]);
                }
                break;
            case AxisPacketTypes.NodeConfigResponse:
                Assert.AreEqual(AxisNodeConfigCategories.AXIS_NODE_CONFIG_CATEGORIES_COUNT, incomingPacket.nodeConfigRes.category);
                Assert.AreEqual(5, incomingPacket.nodeConfigRes.payload.power.power);
                Assert.AreEqual(6, incomingPacket.nodeConfigRes.payload.power.frequency);
                break;

            case AxisPacketTypes.SuitInfo:
                Assert.AreEqual(5, incomingPacket.suitInfo.currentWifiChannel);
                Assert.AreEqual(6, incomingPacket.suitInfo.homeWifiChannel);
                Assert.AreEqual((byte)AxisNodePositions.NODE_INDEX_COUNT - 1, incomingPacket.suitInfo.nodeCount);

                Assert.AreEqual(5, incomingPacket.suitInfo.dongleVersion.firmware.patch);
                Assert.AreEqual(5, incomingPacket.suitInfo.dongleVersion.firmware.minor);
                Assert.AreEqual(5, incomingPacket.suitInfo.dongleVersion.firmware.major);

                Assert.AreEqual(5, incomingPacket.suitInfo.dongleVersion.hardware.patch);
                Assert.AreEqual(5, incomingPacket.suitInfo.dongleVersion.hardware.minor);
                Assert.AreEqual(5, incomingPacket.suitInfo.dongleVersion.hardware.major);

                for (byte i = 0; i < 6; i++)
                {
                    Assert.AreEqual(i, incomingPacket.suitInfo.dongleMAC[i]);
                }

                for (byte i = 0; i < incomingPacket.suitInfo.nodeCount; i++)
                {
                    Assert.AreEqual(5, incomingPacket.suitInfo.nodeSuitInfo[i].batteryPercent);

                    Assert.AreEqual(5, incomingPacket.suitInfo.nodeSuitInfo[i].firmware.patch);
                    Assert.AreEqual(5, incomingPacket.suitInfo.nodeSuitInfo[i].firmware.minor);
                    Assert.AreEqual(5, incomingPacket.suitInfo.nodeSuitInfo[i].firmware.major);

                    Assert.AreEqual((AxisNodePositions)i, incomingPacket.suitInfo.nodeSuitInfo[i].nodeIndex);

                    Assert.AreEqual(true, incomingPacket.suitInfo.nodeSuitInfo[i].on);
                    Assert.AreEqual(AxisNodeOperationModes.OPERATION_MODE_COUNT, incomingPacket.suitInfo.nodeSuitInfo[i].operationMode);

                    for (int j = 0; j < 6; j++)
                    {
                        Assert.AreEqual(j, incomingPacket.suitInfo.nodeSuitInfo[i].mac[j]);
                    }
                }
                break;
            case AxisPacketTypes.ReportWifi:
                Assert.AreEqual(5, incomingPacket.wifiRes.wifiChannel);
                
               
                break;
            case AxisPacketTypes.SetWifiResponse:
                Assert.AreEqual(5, incomingPacket.setWifiRes.nodeCount);
                Assert.AreEqual(true, incomingPacket.setWifiRes.success);
                break;
            case AxisPacketTypes.Versions:
                Assert.AreEqual(5, incomingPacket.versions.nodeCount);

                Assert.AreEqual(1, incomingPacket.versions.dongleVersion.firmware.major);
                Assert.AreEqual(2, incomingPacket.versions.dongleVersion.firmware.minor);
                Assert.AreEqual(3, incomingPacket.versions.dongleVersion.firmware.patch);

                Assert.AreEqual(4, incomingPacket.versions.dongleVersion.hardware.major);
                Assert.AreEqual(5, incomingPacket.versions.dongleVersion.hardware.minor);
                Assert.AreEqual(6, incomingPacket.versions.dongleVersion.hardware.patch);



                for(byte i = 0; i < incomingPacket.versions.nodeCount; i++)
                {
                    Assert.AreEqual(1, incomingPacket.versions.nodeVersions[i].firmware.major);
                    Assert.AreEqual(2, incomingPacket.versions.nodeVersions[i].firmware.minor);
                    Assert.AreEqual(3, incomingPacket.versions.nodeVersions[i].firmware.patch);

                    Assert.AreEqual(4, incomingPacket.versions.nodeVersions[i].hardware.major);
                    Assert.AreEqual(5, incomingPacket.versions.nodeVersions[i].hardware.minor);
                    Assert.AreEqual(6, incomingPacket.versions.nodeVersions[i].hardware.patch);
                    Assert.AreEqual((AxisNodePositions)i, incomingPacket.versions.nodeVersions[i].nodeIndex);
                }
                break;
            case AxisPacketTypes.ReportBatt:
                Assert.AreEqual((byte)AxisNodePositions.NODE_INDEX_COUNT - 1, incomingPacket.battInfo.nodeCount);
                for (byte i = 0; i < incomingPacket.battInfo.nodeCount; i++)
                {
                    Assert.AreEqual((AxisNodePositions)i, incomingPacket.battInfo.nodeBattInfo[i].nodeIndex);
                    Assert.AreEqual(5, incomingPacket.battInfo.nodeBattInfo[i].batteryLevel);
                    Assert.AreEqual(-FLOAT_TEST_VALUE, incomingPacket.battInfo.nodeBattInfo[i].voltageLevel,Mathf.Epsilon);
                }
                break;
            default: 
                break;
        }
    }
   
    [Test]
    [TestCase(AxisPacketTypes.IMUData)]
    [TestCase(AxisPacketTypes.DevPacket)]
    [TestCase(AxisPacketTypes.NodeConfigResponse)]
    [TestCase(AxisPacketTypes.ReportWifi)]
    [TestCase(AxisPacketTypes.ReportBatt)]
    [TestCase(AxisPacketTypes.PairedNodes)]
    [TestCase(AxisPacketTypes.NodeAssignAck)]
    [TestCase(AxisPacketTypes.SetWifiResponse)]
    [TestCase(AxisPacketTypes.Versions)]
    public void TestAxisPacketMarshalling(AxisPacketTypes packetType)
    {
        AxisAPI.TriggerTestAxisPacket(packetType);
    }
    [SetUp]
    public void Setup() 
    {
        // Use the Assert class to test conditions
        AxisSDKConfig_t config = new AxisSDKConfig_t();
        config.packetCallback = HandlePacket;
        var result = AxisAPI.SetConfig(config);
        Assert.AreEqual(AxisRuntimeErrors.OK, result);

        result = AxisAPI.Start();
        Assert.AreEqual(AxisRuntimeErrors.OK, result);
    }
    [TearDown]
    public void Teardown()
    {
        AxisAPI.Stop();
    }
    [Test]
    public void TestHubInfo() 
    {
        AxisAPI.TriggerTestAxisHubInfo(CheckHubInfo);
    }
    [Test]
    public void TestHubStatus()
    {
        AxisAPI.TriggerTestAxisHubStatus(CheckHubStatus);
    }  
    [Test]
    public void TestDongleConnection()
    {
        AxisAPI.TriggerTestDongleConnection(DongleConnectionCallback);
    }

    public void DongleConnectionCallback(bool connected)
    {
        Assert.AreEqual(true, connected);
    }
    public void CheckHubStatus(in AxisHubStatus_t status) 
    {
        Assert.AreEqual(FLOAT_TEST_VALUE, status.battery, Mathf.Epsilon);
        Assert.AreEqual(INT_TEST_VALUE, status.errorCode);
        Assert.AreEqual("FANTASTIC", status.deviceMac);
        Assert.AreEqual("Blur Psyduck", status.deviceName);
        Assert.AreEqual("Axis is Perfect!", status.errorString);
        Assert.AreEqual(AxisRuntimeErrors.RUNTIME_ERROR_COUNT, status.error);
    }
    public void CheckHubInfo(in AxisHubsInfo_t info) 
    {
       
        Assert.AreEqual("Axis is Perfect!", info.errorString);
        Assert.AreEqual(AxisRuntimeErrors.RUNTIME_ERROR_COUNT, info.error);
        Assert.AreEqual(5, info.hubsDetected);
        Assert.AreEqual(6, info.pageNumber);

        for (int i = 0; i < info.hubsDetected; i++)
        {
            Assert.AreEqual("FANTASTIC", info.hubsInfo[i].deviceMac);
            Assert.AreEqual("Blur Psyduck", info.hubsInfo[i].deviceName);
            Assert.AreEqual("Free Striker?", info.hubsInfo[i].deviceIp);
            Assert.AreEqual(INT_TEST_VALUE, info.hubsInfo[i].devicePort);
        }

    }
}
