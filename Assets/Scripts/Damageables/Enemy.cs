using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _spine;
    [SerializeField] private List<ParticleSystem> shootEffects = new List<ParticleSystem>();
    [SerializeField] private Transform _shootPoint;
    [SerializeField] protected Transform _gun;
    [SerializeField] protected Transform _rightForeArm;
    [SerializeField] protected float _shootAnimationInterval;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _shootCoolDown;
    [HideInInspector] public float bulletsCount;
    private float _startForeArmZ;
    private float _changeForeArmZ;
    private float _enableTime;
    private bool IsNotShot = true;

    private void Shoot()
    {
        Bullet _bullet = ObjectPool.instance.GetPooledObject<Bullet>("EnemyBulletPool");
        _bullet.transform.position = _shootPoint.position;
        _bullet.transform.forward = _shootPoint.forward;
        _bullet.enemy = this;
        _bullet.gameObject.SetActive(true);
        foreach (var effect in shootEffects) effect.Play();
        _animator.enabled = false;
        _startForeArmZ = _rightForeArm.eulerAngles.z;
        _changeForeArmZ = _startForeArmZ - 15f;
        _rightForeArm.DORotate(new Vector3(_rightForeArm.eulerAngles.x,
            _rightForeArm.eulerAngles.y, _changeForeArmZ), _shootAnimationInterval);
        Invoke("RotateBackward", _shootAnimationInterval);
    }

    private void SetRandomRotation() 
    {
        _spine.eulerAngles = new Vector3(_spine.eulerAngles.x, _spine.eulerAngles.y,
            Random.Range(-40f, 40f));
        _gun.forward = new Vector3(0f, _gun.forward.y, _gun.forward.z);
    }


    private void OnEnable()
    {
        _enableTime = Time.time;
    }

    private void RotateBackward()
    {
        _rightForeArm.DORotate(new Vector3(_rightForeArm.eulerAngles.x,
            _rightForeArm.eulerAngles.y, _startForeArmZ), _shootAnimationInterval);
    }

    private void Update()
    {
        if(Time.time - _enableTime > 1f && bulletsCount <= 0 && IsNotShot)
        {
            StartCoroutine(ShootCor());
        }
    }

    private IEnumerator ShootCor()
    {
        IsNotShot = false;
        yield return new WaitForSeconds(_shootCoolDown);
        _animator.enabled = false;
        SetRandomRotation();
        Shoot();
        IsNotShot = true;
    }
}
