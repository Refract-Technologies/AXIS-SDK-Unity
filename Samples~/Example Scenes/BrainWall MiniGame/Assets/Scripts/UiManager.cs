using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace AxisExampleScenes.Minigame.BrainWall
{
    public class UiManager : MonoBehaviour
    {
        public TextMeshProUGUI counterText;
        public TextMeshProUGUI resultText;

        public Action OnStartCounterFinished;

        private IEnumerator StartCounterCoroutine(int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                counterText.text = (seconds - i).ToString();
                yield return new WaitForSeconds(1);

            }

            counterText.text = "";
            ShowTextFor("Start!", 2f);
            OnStartCounterFinished?.Invoke();         
        }

        public void ShowTextFor(string text, float seconds)
        {
            StartCoroutine(ShowTextForCoroutine(text, seconds));
        }
        private IEnumerator ShowTextForCoroutine(string text ,float seconds)
        {
            resultText.text = text;
            yield return new WaitForSeconds(seconds);
            resultText.text = "";
        }
        public void BeginStartCounter(int seconds)
        {
            StartCoroutine(StartCounterCoroutine(seconds));
        }
    }

}
