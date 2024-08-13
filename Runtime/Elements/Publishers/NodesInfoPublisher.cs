using Axis.Interfaces;
using Refract.AXIS;


public class NodesInfoPublisher : IAxisDataPublisher<NodesInfo_t>
{
    public event PublishAxisData<NodesInfo_t> OnPublishData;

    public void PublishData(NodesInfo_t data)
    {
        OnPublishData?.Invoke(0, data);
    }
}
