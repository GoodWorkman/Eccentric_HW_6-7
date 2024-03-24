
using System;
using UnityEngine;

public class Rabbit : Enemy
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _shootRate = 4f;

    private float _timer;
    
    private void Start()
    {
        health = 5;
        damage = 1;
        handleCollision = false; // Заяц не обрабатывает коллизии
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _shootRate)
        {
            _timer = 0;
            
            _animator.SetTrigger("Attack");
        }
    }
}
