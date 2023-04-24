using UnityEngine;

namespace States
{
    public enum State_Type
    {
        [InspectorName("RunningState")]
        Running,
        [InspectorName("JumpingState")]
        Jumping,
        [InspectorName("VerticalScopingState")]
        VerticalScoping,
        [InspectorName("ShootingState")]
        Shooting,
        [InspectorName("RunNScopingState")]
        RunNScopingState,
        [InspectorName("FinishingState")]
        FinishingAnimationState
    }

    public abstract class State
    {
        protected PlayerController character;
        protected StateMachine stateMachine;

        protected State(PlayerController character, StateMachine stateMachine)
        {
            this.character = character;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
            
        }

        public virtual void HandleInput()
        {

        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Exit()
        {

        }
    }
}
