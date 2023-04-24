using UnityEngine;

namespace States
{
    public class VerticalScopingState : State
    {
        private Vector2 touchPos;
        private bool IsTouching;
        private bool IsScoping;
        private bool Touched;

        public VerticalScopingState(PlayerController charachter, StateMachine stateMachine) : base(charachter, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            character._animator.enabled = false;
            IsScoping = true;
        }

        public override void HandleInput()
        {
            base.HandleInput();
            touchPos = Holder.instance.CurrentPosition;
            IsTouching = Holder.instance.Hold;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if(!(IsTouching || IsScoping))
            {
                stateMachine.ChangeState(character.shootingState);               
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            if(IsScoping && IsTouching)
            {
                character._shootControls.bodyRotater.MoveSpine(touchPos.y);
                character._shootControls.laserProjector.ShowLaserPistol();
                Touched = true;
            }
            else if(Touched)
            {
                IsScoping = false;
                Touched = false;
            }
        }

        public override void Exit()
        {
            base.Exit();
            character._shootControls.shooter.Shoot();
            touchPos = Vector2.zero;
            IsTouching = false;
            IsScoping = false;
        }
    }
}
