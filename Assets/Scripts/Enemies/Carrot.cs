using UnityEngine;

public class Carrot : Enemy
{
    [SerializeField] private Rigidbody Rigidbody;

    [SerializeField] private float Speed = 8f;

    private Transform _playerTransform;
    private Vector3 _toPlayer;

    private void Start()
    {
        health = 1;
        damage = 2;
        handleTrigger = false; //  не обрабатывает триггеры
        destroyOnAnyCollision = true;

        _playerTransform = FindObjectOfType<TargetForEnemies>().transform;
        _toPlayer = (_playerTransform.position - transform.position).normalized;

    }

    void FixedUpdate()
    {
        Rigidbody.velocity = _toPlayer * Speed;
    }
}