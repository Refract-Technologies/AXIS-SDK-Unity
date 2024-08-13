using Axis.Interfaces;
using Refract.AXIS;

public class VisibleNodesPublisher : IAxisDataPublisher<VisibleNodesInfo_t>
{
    public event PublishAxisData<VisibleNodesInfo_t> OnPublishData;

    public void PublishData(VisibleNodesInfo_t data)
    {
        OnPublishData?.Invoke(0, data);
    }
}
