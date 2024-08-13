using Axis.Interfaces;
using Refract.AXIS;
public class VersionsInfoPublisher : IAxisDataPublisher<VersionsInfo_t>
{
    public event PublishAxisData<VersionsInfo_t> OnPublishData;

    public void PublishData(VersionsInfo_t data)
    {
        OnPublishData?.Invoke(0, data);
    }
}
