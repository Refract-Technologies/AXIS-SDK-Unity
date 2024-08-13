using Axis.Interfaces;
using Refract.AXIS;

public class SetWifiResponsePublisher : IAxisDataPublisher<SetWifiRes_t>
{
    public event PublishAxisData<SetWifiRes_t> OnPublishData;

    public void PublishData(SetWifiRes_t data)
    {
        OnPublishData?.Invoke(0, data);
    }
}