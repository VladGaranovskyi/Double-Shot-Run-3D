using System.Collections.Generic;
using UnityEngine;
using States;

namespace GameModes
{
    [SelectionBase]
    public class ShootWallMode : MonoBehaviour, GameMode
    {
        [SerializeField] private List<Transform> _children;
        [Space]
        [SerializeField] private Wall _wall;
        [SerializeField] private State_Type _state;
        [SerializeField] private ParticleSystem[] _transitionParticles;
        private PlayerController _playerController;

        private void Start()
        {
            _playerController = FindObjectOfType<PlayerController>();
        }

        public State_Type GetModeState() => _state;

        public void InitializeGame()
        {
            foreach(var part in _transitionParticles) part.Play();
            foreach (Transform child in _children)
            {
                child.gameObject.SetActive(true);
                child.parent = null;
            }
        }

        public void EndGame()
        {
            foreach (var part in _transitionParticles) part.Play();
            foreach (Transform child in _children)
            {
                child.parent = transform;
                child.gameObject.SetActive(false);
            }
            _playerController.stateMachine.ChangeState(_playerController.runningState);
        }

        public bool IsGameWon() => _wall.GetHealth() >= 100;

        public bool IsGameLost() => _wall.GetHealth() <= 0;

        public GameObject GetGameObject() => gameObject;
    }
}
