
using Axis.Events;
using NUnit.Framework;
using Refract.AXIS;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AxisSDKTest : AxisSDK
{
    public MasterAxisBroker broker;
    private bool dongleConnected = false;
   // [SetUp]
    public void SetUpSDKEnvironment()
    {
        StartSDK();
        AxisEvents.OnDongleConnected += DongleConnection;
    }
    public void DongleConnection(bool connected)
    {
        dongleConnected = connected;
    }
    public void CallDongleEvent(bool connected)
    {
        AxisEvents.OnDongleConnected?.Invoke(connected);
    }
    [Test]
    public void TestSDK()
    {
        SetUpSDKEnvironment();
        dongleConnected = AxisAPI.IsDongleConnected();
        Assert.IsFalse(dongleConnected);
        AxisAPI.TriggerTestDongleConnection(CallDongleEvent);
        Assert.IsTrue(dongleConnected);
        TearDownSDK();
        
    }
    public void TearDownSDK() 
    {
        StopSDK();
        AxisEvents.OnDongleConnected -= DongleConnection;
    }

}
