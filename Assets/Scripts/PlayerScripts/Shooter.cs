using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private ShootControls _shootControls;
    [SerializeField] private PlayerController _playerController;
    private Transform _shootPoint;
    private List<ParticleSystem> shootEffects;

    private void Start()
    {
        _shootPoint = _shootControls.GetShootPoint();
        shootEffects = _shootControls.GetEffects();
    }

    public void Shoot()
    {
        Bullet bullet = ObjectPool.instance.GetPooledObject<Bullet>("PlayerBulletPool");
        bullet.transform.position = _shootPoint.position;
        bullet.transform.forward = _shootPoint.forward;
        bullet.playerController = _playerController;
        bullet.gameObject.SetActive(true);
        bullet.Damage = -bullet.Damage;
        _playerController.wallCam.Follow = bullet.transform;
        PerformShoot();
        _shootControls.lineRenderer.SetPosition(0, Vector3.zero);
        _shootControls.lineRenderer.SetPosition(1, Vector3.zero);
        _shootControls.lineRenderer.SetPosition(2, Vector3.zero);
    }

    public void Shoot(Vector3 point)
    {
        Bullet bullet = ObjectPool.instance.GetPooledObject<Bullet>("PlayerBulletPool");
        _shootControls.GetSpine().forward = (point - _shootControls.GetSpine().position).normalized;
        _shootControls.GetPistol().forward = (point - _shootControls.GetPistol().position).normalized;
        _playerController.playerAnimator.StartShootAnimationCor(_shootControls.GetSpine().forward, _shootControls.GetPistol().forward);
        bullet.transform.position = _shootPoint.position;
        bullet.transform.forward = _shootPoint.forward;
        bullet.playerController = _playerController;
        bullet.gameObject.SetActive(true);
        PerformShoot();
    }

    public void PerformShoot()
    {
        foreach (var effect in shootEffects) effect.Play();
    }
}
