using Axis.DataTypes;

namespace Axis.Interfaces
{
    public delegate void PublishAxisData<T>(ulong channel, T data) where T : IAxisData;
    public interface IAxisDataPublisher<T> where T : IAxisData
    {
        event PublishAxisData<T> OnPublishData;
    }
    public interface IAxisDataSubscriber<T> where T : IAxisData
    {
        void OnChanged(T data);
    }
    interface IAxisDataBroker
    {
        void Cleanup();
    }
}