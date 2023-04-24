using UnityEngine;
using GameModes;

public class ShootTrigger : MonoBehaviour
{
    [SerializeField] private Transform _nextGameMode;
    private PlayerController pc;

    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            GameMode nextGame = _nextGameMode.GetComponent<GameMode>();
            pc.stateMachine.ChangeState(pc.jumpingState);
            GMController.instance.currentGameMode = nextGame;
            pc.jumpingState.NewState = StateFactory.GetState(nextGame.GetModeState(), pc);
            nextGame.InitializeGame();
        }
    }
}
