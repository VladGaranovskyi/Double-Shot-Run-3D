using UnityEngine;

namespace States
{
    public class RunNScopingState : State
    {
        private Vector2 _currentPosition;
        private bool IsHold;
        private float _shootTime;
        private bool IsShoot;
        public RunNScopingState(PlayerController charachter, StateMachine stateMachine) : base(charachter, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            character.playerAnimator.SwitchToUpperLowerLayer();
            character.stateDrivenCameraAnimator.Play("Behind");
        }

        public override void HandleInput()
        {
            base.HandleInput();
            _currentPosition = Holder.instance.CurrentPosition;
            IsHold = Holder.instance.Hold;
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (IsHold && Time.time - _shootTime > character.playerAnimator.RShootPeriod) IsShoot = true;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            Vector3 dirMove = character.SpeedRNS * Vector3.forward;
            character.characterController.Move(dirMove * Time.deltaTime);
            if (IsShoot)
            {
                RaycastHit hit;
                Ray ray = character.Cam.ScreenPointToRay(_currentPosition);
                if(Physics.Raycast(ray, out hit, Mathf.Infinity, character._shootControls.BulletLayer))
                {
                    character._shootControls.shooter.Shoot(hit.point);
                    IsShoot = false;
                    _shootTime = Time.time;
                }
            }
        }
    }
}
