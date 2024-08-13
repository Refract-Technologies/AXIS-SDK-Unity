using Axis.Interfaces;
using Refract.AXIS;
using System.Diagnostics;

public class DoubleClickPublisher : IAxisDataPublisher<DoubleClickNodesInfo_t>
{
    public event PublishAxisData<DoubleClickNodesInfo_t> OnPublishData;

    public void PublishData(DoubleClickNodesInfo_t data)
    {
        OnPublishData?.Invoke(0, data);
    }
}

