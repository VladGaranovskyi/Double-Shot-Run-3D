using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Wall : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform _enemy;
    [SerializeField] private Transform _player;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _damageMultiplier = 1f;
    [SerializeField] private WallModeProgressDisplayer _progressDisplayer;
    private float health = 50f;
    private float StepDivider;
    private float _step;

    private void OnEnable()
    {
        StepDivider = health * 2f;
        _progressDisplayer.gameObject.SetActive(true);
    }

    private void Update()
    {
        _step = Mathf.Abs(_player.position.z - _enemy.position.z) / StepDivider;
    }

    public void ChangeHealth(float val)
    {
        float i = val > 0 ? 1f : -1f;
        health += i * _damageMultiplier;
        _progressDisplayer.DisplayProgress(health);
        transform.position += Vector3.forward * i * _step;
        if(health >= 100f)
        {
            _progressDisplayer.ResetPositions();
            _progressDisplayer.gameObject.SetActive(false);
        }
    }

    public float GetHealth() => health;
}
