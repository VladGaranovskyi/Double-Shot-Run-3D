using UnityEngine;
namespace States
{
    public sealed class FinishingAnimationState : State
    {
        public RagDollController Doll { private get; set; }
        public Transform TargetPos { private get; set; }
        public float Score { private get; set; }
        private float _fixedStopTime = 0f;
        private bool IsFinishing;
        private bool IsFinished;
        public FinishingAnimationState(PlayerController character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            character.playerAnimator.SwitchToNormalLayer();
            character.playerAnimator.animator.SetBool("JumpOverBool", false);
            character._tilesSpawner.SpawnTiles();           
            Doll.transform.parent = null;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (character.transform.position.z > TargetPos.position.z)
            {
                _fixedStopTime = Time.time;
                character.playerAnimator.animator.SetBool("ShootBool", true);
                character.transform.forward = Vector3.forward;
            }
            else if(_fixedStopTime != 0f && !IsFinishing && Time.time - _fixedStopTime > character.playerAnimator.ShootDuration)
            {               
                _fixedStopTime = Time.time;
                IsFinishing = true;
            }
            if (IsFinishing && !IsFinished && Time.time - _fixedStopTime > character.playerAnimator.ShootDuration)
            {
                character.playerAnimator.animator.enabled = false;
                character._shootControls.shooter.PerformShoot();
                character.playerAnimator.PlayAnimationShoot();
                Doll.Pelvis.velocity = Vector3.forward;
                Doll.ChangeRagDollState(true);
                Doll.ApplyForce(Vector3.forward * (Score < character.defaultForceFinish / Doll.Koefficient ? character.defaultForceFinish / Doll.Koefficient : Score));
                character.stateDrivenCameraAnimator.Play("Finish");
                IsFinished = true;
            }
            if (Doll.IsStopped && IsFinished)
            {
                Debug.Log(character._tilesSpawner.GetMultiplierByPosition(Doll.SummaryPos.z));
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            if (_fixedStopTime == 0f)
                character.characterController.Move(Vector3.forward * character.Speed * Time.deltaTime);
        }
    }
}
