using System;
using UnityEngine;

public class LaserProjector : MonoBehaviour
{
    [SerializeField] private ShootControls _shootControls;
    private Transform _shootPoint;

    private void Start()
    {
        _shootPoint = _shootControls.GetShootPoint();
    }

    public void ShowLaserPistol()
    {
        _shootControls.lineRenderer.SetPosition(0, _shootPoint.position);
        RaycastHit[] rh = _shootControls.GetHitsFromPistol();
        _shootControls.lineRenderer.SetPosition(1, rh[0].point);
        if (rh[0].collider.tag != "Border") _shootControls.lineRenderer.SetPosition(2, rh[1].point);
    }
}
