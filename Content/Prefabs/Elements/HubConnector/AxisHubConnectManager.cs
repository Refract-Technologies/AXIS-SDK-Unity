using AOT;
using Axis.Events;
using Refract.AXIS;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AxisHubConnectManager : MonoBehaviour
{
    public TMP_InputField HubIpText;
    public TMP_InputField HubPortText;

    public void StartAxisHubEvent(string hubIP, int port)
    {
        AxisRuntimeErrors result = AxisAPI.PairWithHub(hubIP, port, AxisHubErrorPairingCallback);
        if (result != AxisRuntimeErrors.OK)
        {
            Debug.LogError("Error when pairing hub: " + result);
        }
        AxisEvents.OnStartStreaming?.Invoke();
    }
    [MonoPInvokeCallback(typeof(AxisRuntimeErrorCallback))]
    private static void AxisHubErrorPairingCallback(AxisRuntimeErrors runtimeErrors, int errorCode, string errorString)
    {
        if (runtimeErrors != AxisRuntimeErrors.OK)
        {
            Debug.LogError("Error when pairing hub callback: " + errorString + " error Code: " + errorCode);
        }
    }

    public void StartHubStreaming()
    {

        StartAxisHubEvent(HubIpText.text.TrimEnd(), int.Parse(HubPortText.text.TrimEnd()));
    }
    public void StopHubStreaming()
    {
        AxisEvents.OnStopStreaming();
    }
}
