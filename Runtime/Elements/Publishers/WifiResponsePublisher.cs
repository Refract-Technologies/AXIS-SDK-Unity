using Axis.Interfaces;
using Refract.AXIS;


public class WifiResponsePublisher : IAxisDataPublisher<WifiRes_t>
{
    public event PublishAxisData<WifiRes_t> OnPublishData;

    public void PublishData(WifiRes_t data)
    {
        OnPublishData?.Invoke(0, data);
    }
}