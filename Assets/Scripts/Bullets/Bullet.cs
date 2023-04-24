using UnityEngine;
using System.Collections.Generic;

internal class BulletVectors
{
    private float sinOfAngle;
    private float cosOfAngle;

    public BulletVectors(float angle)
    {
        sinOfAngle = Mathf.Sin((Mathf.PI * angle)/ 180f);
        cosOfAngle = Mathf.Cos((Mathf.PI * angle) / 180f);
    }

    internal List<Vector3> GetVectors(int count, Vector3 dir)
    {
        List<Vector3> outputVectors = new List<Vector3>();
        void AddVectorsToOutput(float sinus, float cosinus)
        {
            outputVectors.Add(
                new Vector3(0f, -dir.z * sinus + dir.y * cosinus,
                dir.z * cosinus + dir.y * sinus).normalized
                );
            outputVectors.Add(
                new Vector3(0f, -dir.z * -sinus + dir.y * cosinus,
                dir.z * cosinus + dir.y * -sinus).normalized
                );
        }
        switch (count)
        {
            case 2:
                AddVectorsToOutput(sinOfAngle, cosOfAngle);
                break;
            case 3:
                AddVectorsToOutput(sinOfAngle, cosOfAngle);
                outputVectors.Add(dir);
                break;
            case 4:
                AddVectorsToOutput(sinOfAngle, cosOfAngle);
                AddVectorsToOutput(2 * sinOfAngle * cosOfAngle,
                    Mathf.Pow(cosOfAngle, 2) - Mathf.Pow(sinOfAngle, 2));
                break;
            case 5:
                AddVectorsToOutput(sinOfAngle, cosOfAngle);
                AddVectorsToOutput(2 * sinOfAngle * cosOfAngle,
                    Mathf.Pow(cosOfAngle, 2) - Mathf.Pow(sinOfAngle, 2));
                outputVectors.Add(dir);
                break;
        }
        return outputVectors;
    }
}

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private string _currentTag;
    [SerializeField] private float _firstSplitAngle;
    [SerializeField] private Transform _bulletPrefab;
    private float _startDamage;
    public PlayerController playerController { private get; set; }
    public Enemy enemy { private get; set; }
    public float Damage { get => _damage; set => _damage = value; }
    
    private Vector3 _surfaceNormal;
    private BulletVectors _bulletVectors;
    private IDamageable _damageable;
    public void SetBulletTag(string newTag) => _currentTag = newTag;

    public string GetBulletTag() => _currentTag;

    public string BlockedMultiplierName { get; private set; }

    private void Awake()
    {
        _startDamage = _damage;
    }

    private void OnEnable()
    {
        if (GMController.instance.currentGameMode != null)
        {
            ChangeBulletCount(1);
            _bulletVectors = new BulletVectors(_firstSplitAngle);
        }
    }

    private void OnDisable()
    {
        if(GMController.instance.currentGameMode != null)
        {
            ChangeBulletCount(-1);
        }
        _damage = _startDamage;
    }

    private void ChangeBulletCount(int c)
    {
        switch (_currentTag)
        {
            case "EnemyBullet":
                enemy.bulletsCount += c;
                break;
            case "PlayerBullet":
                playerController.shootingState.bulletsCount += c;
                break;
        }
    }

    private void SetDirection()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, Mathf.Infinity, _layer);
        _surfaceNormal = hit.normal;
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime * _speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.GetComponent<Bullet>() == null)
        {
            ParticleSystem parts = ObjectPool.instance.GetPooledObject<ParticleSystem>("BulletPartsPool");
            parts.transform.position = transform.position;
            parts.gameObject.SetActive(true);
            parts.Play();
            if(collision.transform.TryGetComponent<IDamageable>(out _damageable))
            {
                _damageable.ChangeHealth(-_damage);
                gameObject.SetActive(false);
            }
            else
            {
                SetDirection();
                transform.forward = Vector3.Reflect(transform.forward, _surfaceNormal);
            }
        }
    }

    public void SplitIntoNBullets(int count, string multiplierName)
    {
        List<Vector3> normals = _bulletVectors.GetVectors(count, transform.forward);
        foreach(var normal in normals)
        {
            Bullet bullet = ObjectPool.instance.GetPooledObject<Bullet>(_currentTag + "Pool");
            bullet.transform.position = transform.position;
            bullet.transform.forward = normal;
            bullet.BlockedMultiplierName = multiplierName;
            bullet.playerController = playerController;
            bullet.Damage = _damage;
            bullet.gameObject.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
