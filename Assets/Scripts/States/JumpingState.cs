using UnityEngine;

namespace States
{
    public class JumpingState : State
    {
        private bool IsJumping;
        private float _jumpTime;
        private Vector3 dirMove;
        private float[] jumpPeriods;
        public State NewState { private get; set; }

        public JumpingState(PlayerController charachter, StateMachine stateMachine) : base(charachter, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            IsJumping = true;
            _jumpTime = character.GetTime();
            character._animator.SetBool("JumpOverBool", true);
            jumpPeriods = new float[4] { 0, 0, 0, 0};
            for(int i = 0; i < jumpPeriods.Length; i++)
            {
                for(int j = 0; j <= i; j++)
                {
                    jumpPeriods[i] += character.jumpPeriods[j];
                }
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (character.GetTime() - _jumpTime > jumpPeriods[0] && character.GetTime() - _jumpTime < jumpPeriods[1])
            {
                character.characterController.height = 2f;
            }
            else if (character.GetTime() - _jumpTime > jumpPeriods[1] && character.GetTime() - _jumpTime < jumpPeriods[2])
            {
                character.characterController.height = character._defaultCharachterHeight;
            }
            else if (character.GetTime() - _jumpTime > jumpPeriods[2] && character.GetTime() - _jumpTime < jumpPeriods[2])
            {
                character.Speed = 0f;
            }
            else if (character.GetTime() - _jumpTime > jumpPeriods[3])
            {
                stateMachine.ChangeState(NewState);
            }         
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            dirMove = character.Speed * Vector3.forward;
            if (IsJumping)
            {
                if (character.GetTime() - _jumpTime < 0.5f)
                {
                    dirMove += Vector3.up * character.GetJumpHeight();
                }
                else
                {
                    dirMove += Vector3.Lerp(Vector3.up * character.transform.position.y, Vector3.up * character.GetDefaultY(), 0.5f);
                    if (Mathf.Abs(character.transform.position.y - character.GetDefaultY()) < 0.1f)
                    {
                        dirMove += (Vector3.up * (character.GetDefaultY() - character.transform.position.y) * Time.deltaTime) - Physics.gravity;
                        IsJumping = false;
                    }
                }
            }
            if (!character.characterController.isGrounded)
            {
                dirMove += Physics.gravity;
            }
            character.characterController.Move(dirMove * Time.deltaTime);
        }

        public override void Exit()
        {
            base.Exit();
            _jumpTime = 0f;
            dirMove = Vector3.zero;            
            character.transform.eulerAngles = Vector3.zero;
        }
    }
}
