using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Axis.Events;
using Axis.Tutorials.Elements;
using Axis.Elements;
using UnityEngine.UI;
using Axis.Enumerations;

public class NodeInputHandler : MonoBehaviour
{
    public static Action OnSetLedColorClicked;
    public static Action OnSetNodeVibrationClicked;

    public Slider durationSlider;
    public Slider intensitySlider;
    public Slider redColorSlider;
    public Slider blueColorSlider;
    public Slider greenColorSlider;
    public Slider brightnessSlider;
    public Image ledPreview;
    int selectedNodeIndex;
    AxisNode axisNode;
    private void OnEnable()
    {
        AxisTutorialNode.OnNodeSelected += HandleOnNodeSelected;

        redColorSlider.onValueChanged.AddListener(delegate
        {
            HandleColorSliderChanged();
        });

        blueColorSlider.onValueChanged.AddListener(delegate
        {
            HandleColorSliderChanged();
        });

        greenColorSlider.onValueChanged.AddListener(delegate
        {
            HandleColorSliderChanged();
        });

        
    }

    private void HandleColorSliderChanged()
    {    
        ledPreview.color = new Color(redColorSlider.value, greenColorSlider.value, blueColorSlider.value);
    }

    //This method updates the Node LED color when the Button is clicked
    public void HandleSetColorButtonClicked()
    {
        OnSetLedColorClicked.Invoke(); //this is just for the tutorial task

        //IMPORTANT FOR DEVS!!!
        //This is the event that will issue the command for the node to change color
        Color32 color = new Color(ledPreview.color.r, ledPreview.color.g, ledPreview.color.b);
        AxisEvents.OnSetNodeLedColor?.Invoke(selectedNodeIndex, color, brightnessSlider.value);
    }

    //This method handles the vibrate button press.
    public void HandleVibrateButtonPressed()
    {
        OnSetNodeVibrationClicked.Invoke(); //this is just for the tutorial task
        
        //IMPORTANT FOR DEVS!!!
        //This is the event that will issue the command for the node to vibrate
        AxisEvents.OnSetNodeVibration?.Invoke(selectedNodeIndex, intensitySlider.value, durationSlider.value);
    }

    private void HandleOnNodeSelected(AxisNode _axisNode, int _selectedNodeIndex)
    {
        GetComponent<Canvas>().enabled = true;
        axisNode = _axisNode;
        selectedNodeIndex = _selectedNodeIndex;
    }

    private void Update()
    { 

        if (Input.GetKeyDown(KeyCode.Z))
        {
            
                Debug.Log("Zero");
                AxisEvents.OnZeroAll?.Invoke();
            
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Calibration");
            AxisEvents.OnCalibration?.Invoke();
        }
    }


}
