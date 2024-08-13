using Axis.Interfaces;
using Refract.AXIS;
public class BattInfoPublisher : IAxisDataPublisher<BattInfo_t>
{
    public event PublishAxisData<BattInfo_t> OnPublishData;

    public void PublishData(BattInfo_t data)
    {
        OnPublishData?.Invoke(0, data);
    }
}