using Axis.DataTypes;
using Axis.Events;
using Axis.Interfaces;
using Refract.AXIS;

public class IMUDataPublisher : IAxisDataPublisher<IMUData_t>
{
    public event PublishAxisData<IMUData_t> OnPublishData;

    public void PublishData(IMUData_t data)
    {
        OnPublishData?.Invoke(0, data);
    }
}
public class AxisOutputDataPublisher : IAxisDataPublisher<AxisOutputData>
{
    public event PublishAxisData<AxisOutputData> OnPublishData;

    public void PublishData(IMUData_t data)
    {
        AxisOutputData axisOutputData = new AxisOutputData();
        for (ushort i = 0; i < (ushort)AxisNodePositions.NODE_INDEX_COUNT - 1; i++) 
        {
            AxisNodeData nodeData = new AxisNodeData();
            
            NodeIMUData_t nodeIMUData = data.nodeImuData[(AxisNodePositions)i];
            nodeData.isActive = nodeIMUData.on;
            nodeData.rotation = AxisUtils.ConvertAxisQuatToUnityQuat(nodeIMUData.quat);
            nodeData.accelerations = AxisUtils.ConvertAxisVector3toUnityVector3(nodeIMUData.linAccel);
            axisOutputData.nodesDataList.Add(nodeData);
        }
        axisOutputData.hubData.rotation = AxisUtils.ConvertAxisQuatToUnityQuat(data.hubData.quat);
        axisOutputData.hubData.absolutePosition = AxisUtils.ConvertAxisVector3toUnityVector3(data.hubData.pos);
        OnPublishData?.Invoke(0, axisOutputData);
        AxisEvents.OnAxisOutputDataUpdated?.Invoke(axisOutputData);
    }
}

