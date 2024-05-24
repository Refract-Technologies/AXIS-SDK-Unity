using System;
using System.Collections.Generic;
using UnityEngine;

namespace AxisExampleScenes.Minigame.BrainWall
{
    [Serializable]
    public class ListWrapper
    {
        public List<bool> boolList;
        public ListWrapper()
        {
            boolList = new List<bool>();
        }
    }

    public class WallSetup : ScriptableObject
    {

        [HideInInspector] public List<ListWrapper> disabledWallParts;
        private void OnEnable()
        {
            if (disabledWallParts == null || disabledWallParts.Count == 0)
            {
                disabledWallParts = new List<ListWrapper>();

                for (int i = 0; i < WallCreator.partsPerSide; i++)
                {
                    var column = new ListWrapper();
                    for (int j = 0; j < WallCreator.partsPerSide; j++)
                    {
                        column.boolList.Add(false);
                    }

                    disabledWallParts.Add(column);
                }
            }
        }
    }

}
