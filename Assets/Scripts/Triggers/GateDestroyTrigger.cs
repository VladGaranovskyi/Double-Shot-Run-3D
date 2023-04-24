using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDestroyTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _gate;
    [SerializeField] private ParticleSystem _parts;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            Destroy(_gate);
            _parts.Play();
        }    
    }
}
