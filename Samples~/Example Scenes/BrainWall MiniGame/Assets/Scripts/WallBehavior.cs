using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AxisExampleScenes.Minigame.BrainWall
{
    public class WallBehavior : MonoBehaviour
    {
        public Action OnWallRunned;
        public List<WallSetup> wallSetups;
        private static int round = 0;
        public WallCreator wallCreator;
        private List<WallPartBehavior> wallParts;
        private Rigidbody rb;
        private float finishLineZ;

        





        private void Start()
        {
            wallCreator.CreateWall();
            
            wallParts = wallCreator.wallParts;
            rb = GetComponent<Rigidbody>();
            
        }

        internal void SetWallPosition(float startLineZ)
        {
            transform.position = new Vector3(0, 0, startLineZ);
        }

        internal void StartMovingWall(float wallSpeed, float _finishLineZ)
        {
            finishLineZ = _finishLineZ;
            rb.velocity = new Vector3(0, 0, -wallSpeed);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Collided with {other.gameObject.name}");
        }

        internal void ResetWall(float startLineZ)
        {

            transform.position = new Vector3(0,0, startLineZ);
            rb.velocity = Vector3.zero;
            foreach (var wallPart in wallParts)
            {
                //Debug.Log($"Reseting");
                wallPart.Reset(wallSetups[round]);
            }

            //Debug.Log($"Round {round} STC{wallSetups.Count} ST-1{wallSetups.Count - 1} ");
            round = round + 1 > wallSetups.Count - 1 ? 0 : round + 1;
            //round++;
        }
        void Update()
        {
            //rb.velocity = new Vector3(0, 0, -wallSpeed);
            if (transform.position.z < -finishLineZ)
            {
                OnWallRunned?.Invoke();
            }
        }


    }

}
