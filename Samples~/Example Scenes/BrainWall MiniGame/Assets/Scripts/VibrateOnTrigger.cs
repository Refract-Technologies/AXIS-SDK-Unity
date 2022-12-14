using System.Collections;
using System.Collections.Generic;
using Axis.Enumerations;
using Axis.Events;
using UnityEngine;

namespace AxisExampleScenes.Minigame.BrainWall
{
    public class VibrateOnTrigger : MonoBehaviour
    {

        public NodeBinding nodeToVibrate;
        bool cooledDown = true;

        private IEnumerator CooldownAfter(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            cooledDown = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent.tag != "Player")
            {
                if (cooledDown == true)
                {
                    VibrationFeedbackSingleton.instance.HandleHitAtNode((int)nodeToVibrate);
                    cooledDown = false;
                }
                StartCoroutine(CooldownAfter(VibrationFeedbackSingleton.instance.cooldownTime));
            }
        }

    }

}
