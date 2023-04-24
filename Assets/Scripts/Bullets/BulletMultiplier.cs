using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMultiplier : MonoBehaviour
{
    [SerializeField] private Transform _bulletPrefab;
    [SerializeField] private int n;
    private Bullet _currentBullet;
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Bullet>(out _currentBullet))
        {
            if(_currentBullet.BlockedMultiplierName != name) 
                _currentBullet.SplitIntoNBullets(n, name);
        }
    }
}
