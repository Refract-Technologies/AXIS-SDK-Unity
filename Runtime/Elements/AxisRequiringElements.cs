using UnityEngine;
using Axis.Communication;



namespace Axis.Elements
{
    public class AxisRequiringElements : MonoBehaviour
    {
        [HideInInspector] public GameObject runtimeUdpSocketPrefab;

        protected virtual void OnEnable()
        {
            InstantiateAxisRuntimeData();
        }
        protected void InstantiateAxisRuntimeData()
        {
            if (FindObjectOfType<AxisRuntimeUdpSocket>() == null)
            {
                GameObject udpSocket = Instantiate(runtimeUdpSocketPrefab);
                udpSocket.hideFlags = HideFlags.HideInHierarchy;
            }
        }
    }
}

