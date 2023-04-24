using UnityEngine;
using GameModes;
using DG.Tweening;

public class FinishTarget : MonoBehaviour, IDamageable
{
    [SerializeField] private FinishGameMode _finishGM;
    [SerializeField] private GateDestroyTrigger _gateTrigger;
    [SerializeField] private Transform _leftBottomCorner;
    [SerializeField] private Transform _rightTopCorner;
    [SerializeField] private float _multiplier;
    [SerializeField] private Transform _gate;
    [SerializeField] private Vector3 _rotateEndValue;
    [SerializeField] private float _rotationDuration;
    private bool IsNotShot = true;

    public void ChangeHealth(float hp) 
    {
        if (IsNotShot)
        {
            _finishGM.score += hp * _multiplier * -1f;
            _gate.DORotate(_rotateEndValue, _rotationDuration);
            Destroy(_gateTrigger.gameObject);
            IsNotShot = false;
        }
    }

    public float GetHealth() => _finishGM.score;

    public void SetRandomPos()
    {
        transform.position = Vector3.up * Random.Range(_leftBottomCorner.position.y, _rightTopCorner.position.y) +
        Vector3.right * Random.Range(_leftBottomCorner.position.x, _rightTopCorner.position.x) + Vector3.forward * transform.position.z;
    }
}
