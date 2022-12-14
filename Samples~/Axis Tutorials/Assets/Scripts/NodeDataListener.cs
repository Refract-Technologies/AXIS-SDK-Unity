using System;
using System.Collections;
using System.Collections.Generic;
using Axis.Tutorials.Elements;
using UnityEngine;
using TMPro;

namespace Axis.Tutorials
{
    public class NodeDataListener : MonoBehaviour
    {
        AxisTutorialNode selectedNode;
        public TextMeshProUGUI inputDataText;
        public TextMeshProUGUI selectedNodeText;
        private void OnEnable()
        {
            AxisTutorialNode.OnNodeSelected += HandleNodeSelected;
        }

        private void OnDisable()
        {
            AxisTutorialNode.OnNodeSelected -= HandleNodeSelected;

        }

        private void HandleNodeSelected(AxisTutorialNode _selectedNode, int nodeIndex)
        {
            selectedNode = _selectedNode;
            selectedNodeText.text = $"{selectedNode.gameObject.name}";

        }

        private void Update()
        {
            if(selectedNode != null)
            {
                inputDataText.text = $"Rotation Quaternions{selectedNode.transform.rotation.ToString("F2")} \n\n" +
                    $"Rotation Euler {selectedNode.transform.eulerAngles.ToString("F3")} \n\n" +
                    $"Acceleration {selectedNode.Accelerations}";
            }
             
        }
    }
}

