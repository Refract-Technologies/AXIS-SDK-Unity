using Axis.Interfaces;
using Refract.AXIS;
public class DevpacketPublisher : IAxisDataPublisher<DevPacket_t>
{
    public event PublishAxisData<DevPacket_t> OnPublishData;

    public void PublishData(DevPacket_t data)
    {
        OnPublishData?.Invoke(0, data);
    }
}