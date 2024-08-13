using System;
using System.Collections;
using System.Collections.Generic;
using Axis.Solvers;
using UnityEngine;

namespace AxisExampleScenes.Minigame.BrainWall
{
    public class GameManager : MonoBehaviour
    {
        public GameParameters gameParameters;

        private CharacterModeSelector characterModeSelector;
        private AbsolutePositionSolver characterAbsolutePositionSolver;

        public WallBehavior wallBehavior;
        public WallRevealer wallRevealer;

        public UiManager uiManager;
        private bool playerWasHit = false;

        public Transform character;
        public Transform startingPosition;

        IEnumerator Start()
        {
            characterModeSelector = character.GetComponent<CharacterModeSelector>();
            characterAbsolutePositionSolver = character.GetComponentInChildren<AbsolutePositionSolver>();

            wallBehavior.SetWallPosition(gameParameters.startLineZ);
            StartCoroutine(StartRoundAfter(0));
            
            characterModeSelector.afterRagdollState = CharacterModeSelector.AfterRagdollGoTo.BoneMirroring;

            yield return new WaitForSeconds(0.5f);
            wallBehavior.ResetWall(gameParameters.startLineZ);
            ResetCharacterPosition();

        }

        private void OnEnable()
        {
            VibrationFeedbackSingleton.OnPlayerHit += HandleOnPlayerHit;           
        }

        private void HandleOnPlayerHit()
        {
            playerWasHit = true;
        }

        private void OnDisable()
        {
            VibrationFeedbackSingleton.OnPlayerHit -= HandleOnPlayerHit;
        }

        private IEnumerator StartRoundAfter(float time)
        {
            yield return new WaitForSeconds(time);
            
            //characterManager.SetAxisControl();

            uiManager.BeginStartCounter(gameParameters.countDownTime);
            playerWasHit = false;
            uiManager.OnStartCounterFinished -= StartReveallingWallStage;
            uiManager.OnStartCounterFinished += StartReveallingWallStage;

        }

        private void StartReveallingWallStage()
        {
            //wallBehavior.ResetWall(gameParameters.startLineZ);
            wallRevealer.RevealWall(gameParameters.revealWallSpeed);
            wallRevealer.OnWallRevealed -= StartMovingWallStage;
            wallRevealer.OnWallRevealed += StartMovingWallStage;
        }

        private void StartMovingWallStage()
        {
            wallBehavior.StartMovingWall(gameParameters.wallSpeed, gameParameters.finishLineZ);
            wallBehavior.OnWallRunned -= HandleOnWallRunned;
            wallBehavior.OnWallRunned += HandleOnWallRunned;
        }

        private void HandleOnWallRunned()
        {
            wallRevealer.HideWall();
            wallBehavior.ResetWall(gameParameters.startLineZ);
            uiManager.ShowTextFor(playerWasHit == true ? "Not Passed!" : "Passed!", 1.5f);
            StartCoroutine(StartRoundAfter(4));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                //ResetCharacterPosition();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                StartRoundAfter(0);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                wallBehavior.ResetWall(gameParameters.startLineZ);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                wallRevealer.RevealWall(gameParameters.revealWallSpeed);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                wallRevealer.HideWall();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                StartMovingWallStage();
            }
        }

        private void ResetCharacterPosition()
        {
            AbsolutePositionSolver[] absolutePositionSolvers = GameObject.FindObjectsOfType<AbsolutePositionSolver>();

            foreach (var absolutePositionSolver in absolutePositionSolvers)
            {
                Vector3 difference = absolutePositionSolver.transform.position - startingPosition.position;
                absolutePositionSolver.xOffset -= difference.x;
                absolutePositionSolver.zOffset -= difference.z;
            }

            
        }
    }
}

