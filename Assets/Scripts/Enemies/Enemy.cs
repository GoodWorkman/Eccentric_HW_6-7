using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    protected int health = 5;
    protected int damage = 1;
    protected bool handleCollision = true;
    protected bool handleTrigger = true;
    protected bool destroyOnAnyCollision = false;

    private bool hasCollided = false; // Флаг для проверки первого столкновения

    public UnityEvent OnDamage;

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        OnDamage?.Invoke();

        if (health <= 0)
        {
            health = 0;

            Die();
        }
    }

    protected void DamagePlayer(Rigidbody playerRigidbody)
    {
        PlayerHealth playerHealth = playerRigidbody.GetComponent<PlayerHealth>();
        if (playerHealth)
        {
            hasCollided = true; // Устанавливаем флаг, так как урон нанесен
            playerHealth.TakeDamage(damage);
        }
    }

    protected void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasCollided) return;

        if (handleCollision && collision.rigidbody)
        {
            DamagePlayer(collision.rigidbody);
        }

        if (destroyOnAnyCollision)
        {
            TakeDamage(100);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasCollided) return;

        if (handleTrigger && other.attachedRigidbody)
        {
            DamagePlayer(other.attachedRigidbody);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        hasCollided = false; // Сбрасываем флаг при выходе из столкновения
    }

    private void OnTriggerExit(Collider other)
    {
        hasCollided = false; // Сбрасываем флаг при выходе из триггера
    }
}