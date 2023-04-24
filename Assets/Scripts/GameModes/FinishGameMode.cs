using UnityEngine;
using States;

namespace GameModes
{
    [SelectionBase]
    public class FinishGameMode : MonoBehaviour, GameMode
    {
        [SerializeField] private Transform _finishPos;
        [SerializeField] private Transform _animationPos;
        [SerializeField] private State_Type _currentState;        
        [SerializeField] private FinishTarget[] _targets;
        [SerializeField] private RagDollController _doll;
        public float score;
        private PlayerController _playerController;

        private void Start()
        {
            _playerController = FindObjectOfType<PlayerController>();
        }

        public void InitializeGame()
        {
            foreach(FinishTarget target in _targets)
            {
                target.gameObject.SetActive(true);
                target.SetRandomPos();
            }
        }

        public void EndGame()
        {
            _playerController.finishingAnimationState.Doll = _doll;
            _playerController.finishingAnimationState.Score = score;
            _playerController.finishingAnimationState.TargetPos = _animationPos;
            _playerController.stateMachine.ChangeState(_playerController.finishingAnimationState);
        }

        public bool IsGameWon() => _playerController.transform.position.z > _finishPos.position.z;

        public bool IsGameLost() => false;//(can't loose)

        public State_Type GetModeState() => _currentState;

        public GameObject GetGameObject() => gameObject;
    }
}
