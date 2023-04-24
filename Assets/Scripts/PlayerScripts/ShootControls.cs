using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShootControls : MonoBehaviour
{
    [SerializeField] private Transform _spine;
    [SerializeField] private List<ParticleSystem> shootEffects = new List<ParticleSystem>();
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private LayerMask _bulletLayer;
    [SerializeField] private Transform _pistol;
    public LineRenderer lineRenderer { get; private set; }
    public Holder holder { get; private set; }
    public LaserProjector laserProjector { get; private set; }
    public BodyRotater bodyRotater { get; private set; }
    public Shooter shooter { get; private set; }
    public LayerMask BulletLayer { get => _bulletLayer; }

    private void OnEnable()
    {
        laserProjector = GetComponent<LaserProjector>();
        bodyRotater = GetComponent<BodyRotater>();
        shooter = GetComponent<Shooter>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public RaycastHit GetHitFromPistol()
    {
        RaycastHit hit;
        Ray ray = new Ray(_shootPoint.position, _shootPoint.forward);
        Physics.Raycast(ray, out hit, Mathf.Infinity, _bulletLayer);
        return hit;
    }

    public RaycastHit[] GetHitsFromPistol()
    {
        RaycastHit[] rh = new RaycastHit[2];
        RaycastHit hit = GetHitFromPistol();
        rh[0] = hit;
        RaycastHit newHit;
        Ray ray = new Ray(hit.point, Vector3.Reflect((hit.point - _shootPoint.position).normalized, hit.normal));
        Physics.Raycast(ray, out newHit, Mathf.Infinity, _bulletLayer);
        rh[1] = newHit;
        return rh;
    }  

    public Transform GetShootPoint() { return _shootPoint; }
    public Transform GetSpine() { return _spine; }
    public Transform GetPistol() { return _pistol; }
    public List<ParticleSystem> GetEffects() { return shootEffects; }
}
