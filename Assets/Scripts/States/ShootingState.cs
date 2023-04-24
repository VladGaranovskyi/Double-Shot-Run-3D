using GameModes;
using Cinemachine;
using UnityEngine;

namespace States
{
    public class ShootingState : State
    {
        private GameMode _currentGameMode;
        private float _shootTime;
        public int bulletsCount;
        public ShootingState(PlayerController charachter, StateMachine stateMachine) : base(charachter, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            character.playerAnimator.PlayAnimationShoot();
            _currentGameMode = GMController.instance.currentGameMode;
            _shootTime = character.GetTime();
            character.characterController.enabled = false;
            switch (_currentGameMode.GetModeState())
            {
                case State_Type.VerticalScoping:
                    character.stateDrivenCameraAnimator.Play("Wall");
                    break;
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            switch (_currentGameMode.GetModeState())
            {
                case State_Type.VerticalScoping:
                    if(character.GetTime() - _shootTime > 15f)
                    {
                        ObjectPool.instance.DisablePooledObjects("PlayerBulletPool");
                    }
                    if (!character.wallCam.Follow.gameObject.activeInHierarchy && bulletsCount > 0)
                    {
                        character.wallCam.Follow = ObjectPool.instance.GetActivePooledObject<Transform>("PlayerBulletPool");
                    }
                    break;
            }

            if (bulletsCount <= 0 && !_currentGameMode.IsGameWon())
            {
                switch (_currentGameMode.GetModeState())
                {
                    case State_Type.VerticalScoping:
                        stateMachine.ChangeState(character.verticalScopingState);
                        break;
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
            ObjectPool.instance.DisablePooledObjects("BulletPartsPool");
            switch (_currentGameMode.GetModeState())
            {
                case State_Type.VerticalScoping:
                    character.characterController.enabled = true;
                    character._animator.enabled = true;
                    character._animator.SetBool("JumpOverBool", false);
                    character.stateDrivenCameraAnimator.Play("Default");
                    break;
            }
        }
    }
}
