using Axis.Interfaces;
using Refract.AXIS;

public class NodeConfigResponsePublisher : IAxisDataPublisher<NodeConfigResponse_t>
{
    public event PublishAxisData<NodeConfigResponse_t> OnPublishData;

    public void PublishData(NodeConfigResponse_t data)
    {
        OnPublishData?.Invoke(0, data);
    }
}