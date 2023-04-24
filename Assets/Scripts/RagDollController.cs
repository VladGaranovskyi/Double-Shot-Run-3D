using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollController : MonoBehaviour
{
    [SerializeField] private Collider[] _colliders;
    [SerializeField] private Rigidbody[] _rigidbodies;
    [SerializeField] private float _forceKoefficient;
    [SerializeField] private Rigidbody _pelvis;

    public float Koefficient { get => _forceKoefficient; }

    public Rigidbody Pelvis { get => _pelvis; }

    public bool IsStopped
    {
        get => _pelvis.velocity.z < 0.01f;
    }

    public Vector3 SummaryPos
    {
        get
        {
            Vector3 sum = Vector3.zero;
            foreach(var col in _colliders)
            {
                sum += col.transform.position;
            }
            return sum / _colliders.Length;
        }
    }

    public void ChangeRagDollState(bool isActive)
    {
        foreach(Collider collider in _colliders)
        {
            collider.isTrigger = !isActive;            
        }
        foreach(Rigidbody rb in _rigidbodies)
        {
            rb.useGravity = isActive;
        }
        _pelvis.transform.parent = null;
    }

    public void ApplyForce(Vector3 dir)
    {
        foreach (Rigidbody rb in _rigidbodies)
        {
            rb.AddForce(dir * _forceKoefficient, ForceMode.Impulse);
        }
    }
}
