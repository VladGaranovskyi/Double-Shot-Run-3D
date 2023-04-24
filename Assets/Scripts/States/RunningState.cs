using UnityEngine;

namespace States
{
    public class RunningState : State
    {
        private Vector3 dirMove;
        public RunningState(PlayerController charachter, StateMachine stateMachine) : base(charachter, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            character._animator.SetBool("JumpOverBool", false);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            dirMove = character.Speed * Vector3.forward;
            if (!character.characterController.isGrounded)
            {
                dirMove += Physics.gravity;
            }
            character.characterController.Move(dirMove * Time.deltaTime);
        }

        public override void Exit()
        {
            base.Enter();
            dirMove = Vector3.zero;
        }
    }
}
