using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _effectPrefab;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private int _damageValue = 1;
    [SerializeField] private Rigidbody _rigidbody;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    public void SetVelocity(Vector3 direction)
    {
        _rigidbody.velocity = direction * _bulletSpeed;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision.rigidbody);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            HandleCollision(other.attachedRigidbody);
        }
        else
        {
            HandleCollisionWithoutRigidbody(other.gameObject);
        }
    }

    private void HandleCollision(Rigidbody collidedRigidbody)
    {
        Enemy enemy = collidedRigidbody?.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(_damageValue); 
        }

        InstantiateEffectAndDestroy();
    }

    private void HandleCollisionWithoutRigidbody(GameObject collidedObject)
    {
        Enemy enemy = collidedObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(_damageValue); 
        }

        InstantiateEffectAndDestroy();
    }

    private void InstantiateEffectAndDestroy()
    {
        Instantiate(_effectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
