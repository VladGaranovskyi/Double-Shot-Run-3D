using States;
using UnityEngine;

namespace GameModes
{
    public interface GameMode
    {
        public bool IsGameWon();

        public bool IsGameLost();

        public void InitializeGame();

        public State_Type GetModeState();

        public void EndGame();

        public GameObject GetGameObject();
    }
}
