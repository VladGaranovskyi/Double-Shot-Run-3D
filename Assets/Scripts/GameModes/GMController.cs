using UnityEngine;
using GameModes;

public class GMController : MonoBehaviour
{
    public GameMode currentGameMode;
    public static GMController instance;
    private PlayerController _playerController;

    private void Awake()
    {
        instance = this;
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if(currentGameMode != null)
        {
            if (currentGameMode.IsGameWon())
            {
                currentGameMode.EndGame();
                currentGameMode = null;
                if(_playerController.shootingState.bulletsCount > 0)
                {
                    ObjectPool.instance.DisablePooledObjects("PlayerBulletPool");
                    ObjectPool.instance.DisablePooledObjects("EnemyBulletPool");
                }
            }
            else if (currentGameMode.IsGameLost())
            {
                Debug.Log("Loose");
            }           
        }
    }
}
