using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AxisExampleScenes.Minigame.BrainWall
{
    public class GameParameters : ScriptableObject
    {
        [Range(0, 1)] public float revealWallSpeed;
        [Range(0f, 5f)] public float wallSpeed;
        [Range(5f, 15f)] public float finishLineZ;
        [Range(-10f, 10f)] public float startLineZ;
        [Range(0, 5)] public int countDownTime;
    }

}
