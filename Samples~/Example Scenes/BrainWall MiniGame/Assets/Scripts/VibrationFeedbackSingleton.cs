using System;
using System.Collections;
using System.Collections.Generic;
using Axis.Events;
using UnityEngine;


namespace AxisExampleScenes.Minigame.BrainWall
{
    //Singleton pattern for all Vibration Handlers
    public class VibrationFeedbackSingleton : MonoBehaviour
    {
        public static VibrationFeedbackSingleton instance;
        public static Action OnPlayerHit;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        [Range(0, 5)] public float cooldownTime;
        [Range(0, 3)] public float vibrationDuration;
        [Range(0, 1)] public float vibrationIntensity;

        public void HandleHitAtNode(int nodeToVibrate)
        {
            OnPlayerHit?.Invoke();
            AxisEvents.OnSetNodeVibration(nodeToVibrate, vibrationIntensity, vibrationDuration);

        }

    }
}

