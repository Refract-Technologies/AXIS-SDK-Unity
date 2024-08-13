using Axis.Interfaces;
using Refract.AXIS;


public class SuitInfoPublisher : IAxisDataPublisher<SuitInfo_t>
{
    public event PublishAxisData<SuitInfo_t> OnPublishData;

    public void PublishData(SuitInfo_t data)
    {
        OnPublishData?.Invoke(0, data);
    }
}
