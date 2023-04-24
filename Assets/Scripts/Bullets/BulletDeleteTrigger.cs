using UnityEngine;

public class BulletDeleteTrigger : MonoBehaviour
{
    private Bullet _currentBullet;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Bullet>(out _currentBullet))
        {
            _currentBullet.gameObject.SetActive(false);
        }
    }
}
