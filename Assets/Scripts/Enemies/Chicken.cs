using UnityEngine;

public class Chicken : Enemy
{
    [SerializeField] private Rigidbody Rigidbody;

    [SerializeField] private float Speed = 3f;
    [SerializeField] private float TimeToReachSpeed = 1f;
    
    private Transform _playerTransform; // в дальнейшем курица будет префаб, поэтому будет искать игрока


    private void Start()
    {
        // Установка здоровья и урона, наследованных от Enemy
        health = 5;
        damage = 1;
        handleTrigger = false; // Курица не обрабатывает триггеры
        
        _playerTransform = FindObjectOfType<TargetForEnemies>().transform;
    }

    void FixedUpdate()
    {
        // Движение к игроку
        Vector3 toPlayer = (_playerTransform.position - transform.position).normalized;
        Vector3 force = Rigidbody.mass * (toPlayer * Speed - Rigidbody.velocity) / TimeToReachSpeed;
        
        Rigidbody.AddForce(force);
    }
}
