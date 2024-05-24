using Axis.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Must vibrate");
            AxisEvents.OnSetNodeVibration?.Invoke(0, 1f, 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Must vibrate");
            AxisEvents.OnSetNodeVibration?.Invoke(1, 1f, 0.5f);
        }

        
    }
}
