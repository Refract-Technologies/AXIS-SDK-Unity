using Axis.Broker;
using Axis.DataTypes;
using Axis.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterAxisBroker
{
    Dictionary<Type, IAxisDataBroker> brokers = new Dictionary<Type, IAxisDataBroker>();
    public void Cleanup()
    {
        foreach (var broker in brokers.Values)
        {
            broker.Cleanup();
        }
    }
    public void RegisterPublisher<T>(IAxisDataPublisher<T> publisher) where T : IAxisData
    {
        Type type = typeof(T);
        if (!brokers.ContainsKey(type))
        {
            brokers.Add(type, new AxisDataBroker<T>());
        }
        var broker = brokers[type] as AxisDataBroker<T>;
        broker.RegisterPublisher(publisher);
    }
    public void DeregisterPublisher<T>(IAxisDataPublisher<T> publisher) where T : IAxisData
    {
        Type type = typeof(T);
        if (!brokers.ContainsKey(type))
        {
            brokers.Add(type, new AxisDataBroker<T>());
        }
        var broker = brokers[type] as AxisDataBroker<T>;
        broker.DeregisterPublisher(publisher);
    }
    public void RegisterSubscriber<T>(ulong channel, IAxisDataSubscriber<T> subscriber) where T : IAxisData
    {
        Type type = typeof(T);
        if (!brokers.ContainsKey(type))
        {
            brokers.Add(type, new AxisDataBroker<T>());
        }
        var broker = brokers[type] as AxisDataBroker<T>;
        broker.RegisterSubscriber(channel, subscriber);
    }
    public void DeregisterSubscriber<T>(ulong channel, IAxisDataSubscriber<T> subscriber) where T : IAxisData
    {
        Type type = typeof(T);
        if (!brokers.ContainsKey(type))
        {
            brokers.Add(type, new AxisDataBroker<T>());
        }
        var broker = brokers[type] as AxisDataBroker<T>;
        broker.DeregisterSubscriber(channel, subscriber);
    }
}
