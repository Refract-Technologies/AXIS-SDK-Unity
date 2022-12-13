using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AxisExampleScenes.Minigame.BrainWall
{
    public class WallCreator : MonoBehaviour
    {
        public GameObject wallPartPrefab;
        public BoxCollider groundCollider;
        public static int partsPerSide = 16;

        public List<WallPartBehavior> wallParts;
        public void CreateWall()
        {
            wallParts = new List<WallPartBehavior>();
            float wallPartWidth = wallPartPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size.x * wallPartPrefab.transform.localScale.x;
            float groundWidth = groundCollider.bounds.size.x;
            float groundHeight = groundCollider.bounds.size.y;

            for (int column = 0; column < partsPerSide; column++)
            {
                for (int row = 0; row < partsPerSide; row++)
                {
                    var wallPart = Instantiate(wallPartPrefab);
                    wallPart.transform.position = new Vector3(-groundWidth / 2 + wallPartWidth / 2 + column * wallPartWidth, groundHeight / 2 + groundCollider.transform.position.y + wallPartWidth / 2 + row * wallPartWidth, 0f);
                    wallPart.transform.parent = transform;
                    wallPart.transform.localPosition = new Vector3(wallPart.transform.localPosition.x, wallPart.transform.localPosition.y, 0f);
                    wallPart.GetComponent<WallPartBehavior>().SetColumnAndRow(column, partsPerSide - row - 1);
                    wallParts.Add(wallPart.GetComponent<WallPartBehavior>());
                }
            }
        }


    }

}
