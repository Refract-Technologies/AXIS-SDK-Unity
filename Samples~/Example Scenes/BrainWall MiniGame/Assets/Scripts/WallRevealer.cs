using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AxisExampleScenes.Minigame.BrainWall
{
    public class WallRevealer : MonoBehaviour
    {
        private Transform revealer;
        private MeshRenderer meshRenderer;

        public Action OnWallRevealed;

        private Vector3 initalScale;

        private void Start()
        {
            revealer = GetComponentInChildren<Transform>();
            meshRenderer = revealer.GetComponentInChildren<MeshRenderer>();
            initalScale = revealer.localScale;
        }

        private IEnumerator RevealWallCoroutine(float revealSpeed)
        {
            bool wallRevealed = false;
            while (wallRevealed == false)
            {

                revealer.localScale -= new Vector3(0, revealSpeed);
                yield return null;
                wallRevealed = revealer.localScale.y <= 0f ? true : false;


            }

            meshRenderer.enabled = false;
            OnWallRevealed?.Invoke();

        }

        public void RevealWall(float revealSpeed)
        {
            StartCoroutine(RevealWallCoroutine(revealSpeed));
        }

        public void Reset()
        {

        }

        internal void HideWall()
        {
            meshRenderer.enabled = true;
            revealer.localScale = initalScale;
        }
    }

}
