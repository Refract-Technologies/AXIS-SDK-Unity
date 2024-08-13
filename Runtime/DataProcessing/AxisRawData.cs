using System.Collections.Generic;
using UnityEngine;

namespace Axis.DataTypes
{
    public class AxisNodeData
    {
        public bool isActive = false;
        public Quaternion rotation;
        public Vector3 accelerations;
    }

    public class AxisHubData
    {
        public Quaternion rotation;
        public Vector3 absolutePosition;
        internal bool isActive;
    }

    public class AxisOutputData : IAxisData
    {
        public const int NodesCount = 17;
        public List<AxisNodeData> nodesDataList { get; set; }
        public AxisHubData hubData { get; set; }
        public bool isActive { get; set; }

        public AxisOutputData()
        {
            hubData = new AxisHubData();
            nodesDataList = new List<AxisNodeData>();
        }
    }

    public interface IAxisData
    {

    }

}


