using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AxisExampleScenes.Minigame.BrainWall
{
    public class WallPartBehavior : MonoBehaviour
    {

        Rigidbody rb;
        BoxCollider boxCollider;
        MeshRenderer meshRenderer;

        public Color hitColor;

        private Vector3 startingPosition;
        private Quaternion startingRotation;
        private Vector3 startingScale;
        private Color startingColor;
        private int columnIndex;
        private int rowIndex;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            boxCollider = GetComponent<BoxCollider>();
            meshRenderer = GetComponent<MeshRenderer>();


        }

        private void FetchInitialValues()
        {
            startingPosition = transform.localPosition;
            startingRotation = transform.localRotation;
            startingScale = transform.localScale;
            startingColor = meshRenderer.materials[0].color;
        }

        // Update is called once per frame

        private void Start()
        {
            FetchInitialValues();
        }
        private IEnumerator ScaleToZero()
        {
            float scaleStep = 0.0001f;
            while (transform.localScale.x > 0)
            {
                Vector3 scale = transform.localScale - new Vector3(scaleStep, scaleStep, scaleStep);
                transform.localScale = scale.x > 0 ? scale : Vector3.zero;
                yield return null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.tag == "Player")
            {
                StartCoroutine(ScaleToZero());
                boxCollider.isTrigger = false;
                rb.isKinematic = false;
                meshRenderer.materials[0].color = Color.red;

                other.GetComponentInParent<CharacterModeSelector>().EnableRagdollMode();
            }

        }

        internal void SetColumnAndRow(int column, int row)
        {
            columnIndex = column;
            rowIndex = row;
        }

        internal void Reset(WallSetup wallSetups)
        {
            StopAllCoroutines();
            boxCollider.isTrigger = true;
            rb.isKinematic = true;
            transform.localPosition = startingPosition;
            transform.localRotation = startingRotation;
            transform.localScale = startingScale;
            meshRenderer.materials[0].color = startingColor;


            if (wallSetups.disabledWallParts[rowIndex].boolList[columnIndex] == true)
            {

                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);

            }

        }
    }

}
