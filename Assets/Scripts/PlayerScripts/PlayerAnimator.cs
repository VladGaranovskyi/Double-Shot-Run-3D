using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Transform _spine;
    [SerializeField] private Transform _pistol;
    [SerializeField] private Transform _rightForeArm;
    [SerializeField] private float _shootAnimationDuration;
    [SerializeField] private float _raycastShootPeriod;
    public Animator animator { get; private set; }
    public float RShootPeriod { get => _raycastShootPeriod; }

    public float ShootDuration { get => _shootAnimationDuration; }

    private int _normalLayerIndex;
    private int _upperBodyIndex;
    private int _lowerBodyIndex;
    private float _startForeArmZ;
    private float _changeForeArmZ;
    private Vector3 _currentForward = Vector3.forward;
    private Vector3 _currentForwardWeapon = Vector3.forward;

    private void Start()
    {
        animator = GetComponent<Animator>();
        _normalLayerIndex = animator.GetLayerIndex("NormalLayer");
        _upperBodyIndex = animator.GetLayerIndex("UpperBody");
        _lowerBodyIndex = animator.GetLayerIndex("LowerBody");
    }

    public void SwitchToNormalLayer()
    {
        animator.SetLayerWeight(_normalLayerIndex, 1f);
        animator.SetLayerWeight(_upperBodyIndex, 0f);
        animator.SetLayerWeight(_lowerBodyIndex, 0f);
    }

    public void SwitchToUpperLowerLayer()
    {
        animator.SetLayerWeight(_normalLayerIndex, 0f);
        animator.SetLayerWeight(_upperBodyIndex, 1f);
        animator.SetLayerWeight(_lowerBodyIndex, 1f);
        Invoke("DisablePistolAnimation", _shootAnimationDuration);
    }

    private void LateUpdate()
    {
        if(animator.GetLayerWeight(_normalLayerIndex) == 0f)
        {
            _spine.forward = _currentForward;
            _pistol.forward = _currentForwardWeapon;
        }
    }

    public void PlayAnimationShoot()
    {
        _startForeArmZ = _rightForeArm.eulerAngles.z;
        _changeForeArmZ = _startForeArmZ + 15f;
        _rightForeArm.DORotate(new Vector3(_rightForeArm.eulerAngles.x,
            _rightForeArm.eulerAngles.y, _changeForeArmZ), 0.5f);
        Invoke("RotateBackward", 0.5f);
    }

    private void RotateBackward()
    {
        _rightForeArm.DORotate(new Vector3(_rightForeArm.eulerAngles.x,
            _rightForeArm.eulerAngles.y, _startForeArmZ), 0.5f);
    }

    private void DisablePistolAnimation() => animator.SetLayerWeight(_upperBodyIndex, 0f);

    public void StartShootAnimationCor(Vector3 forw, Vector3 forwWeapon) => StartCoroutine(ShootRaycastAnimationCor(forw, forwWeapon));

    public IEnumerator ShootRaycastAnimationCor(Vector3 forw, Vector3 forwWeapon)
    {
        _currentForward = forw;
        _currentForwardWeapon = forwWeapon;
        yield return new WaitForSeconds(_raycastShootPeriod);
        _currentForward = Vector3.forward;
        _currentForwardWeapon = Vector3.forward;
    }
}
