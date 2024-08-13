using Axis.DataTypes;
using Axis.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Axis.Broker
{

    public class AxisDataBroker<T> : IAxisDataBroker where T : IAxisData
    {

        List<IAxisDataPublisher<T>> m_publishers = new List<IAxisDataPublisher<T>>();
        Dictionary<ulong, List<IAxisDataSubscriber<T>>> m_subscribers = new Dictionary<ulong, List<IAxisDataSubscriber<T>>>();


        public void Cleanup()
        {
            CleanUpSubscribers();
            CleanUpPublishers();
        }

        public void CleanUpPublishers()
        {               
            foreach (var p in m_publishers)
            {
                p.OnPublishData -= PublisherOnAxisData;               
            }            
            m_publishers.Clear();
        }

        public void CleanUpSubscribers()
        {
            m_subscribers.Clear();
        }

        public void DeregisterPublisher(IAxisDataPublisher<T> publisher)
        {
            if(m_publishers.Contains(publisher))
            {
                publisher.OnPublishData -= PublisherOnAxisData;
                 m_publishers.Remove(publisher);
            }
        }


        public void DeregisterSubscriber(ulong channel, IAxisDataSubscriber<T> subscriber)
        {
            if (m_subscribers.TryGetValue(channel, out var subList))
            {
                subList.Remove(subscriber);
            }
        }

        public void RegisterPublisher(IAxisDataPublisher<T> publisher) 
        {
            if (!m_publishers.Contains(publisher))
            {
                publisher.OnPublishData += PublisherOnAxisData;
                m_publishers.Add(publisher);
            }
        }

        public void RegisterSubscriber(ulong channel, IAxisDataSubscriber<T> subscriber)
        {
            if(!m_subscribers.ContainsKey(channel))
            {
                m_subscribers.Add(channel, new List<IAxisDataSubscriber<T>>());
            }
            var list = m_subscribers[channel];
            if(!list.Contains(subscriber))
            {
                list.Add(subscriber);
            }
        }


        private void PublisherOnAxisData(ulong channel, T axisData)
        {
            if(m_subscribers.TryGetValue(channel, out var subs))
            {
                foreach(var sub in subs)
                {
                    sub.OnChanged(axisData);
                }
            }
        }        
    }
}
