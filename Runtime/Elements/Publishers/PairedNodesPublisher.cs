using Axis.Interfaces;
using Refract.AXIS;

public class PairedNodesPublisher : IAxisDataPublisher<PairedNodesInfo_t>
{
    public event PublishAxisData<PairedNodesInfo_t> OnPublishData;

    public void PublishData(PairedNodesInfo_t data)
    {
        OnPublishData?.Invoke(0, data);
    }
}
