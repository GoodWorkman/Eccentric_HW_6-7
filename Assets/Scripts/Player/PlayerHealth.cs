using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public UnityEvent OnTakeDamage;
    public UnityEvent OnHealed;
    
    [SerializeField] private int _health = 5;
    [SerializeField] private int _maxHealth = 8;

    [SerializeField] private HealthUI _healthUI;

    private bool _isInvincible = false;
    private Coroutine _coroutine;

    private void Start()
    {
        _healthUI.Setup(_maxHealth);
        _healthUI.DisplayCurrentHealth(_health);
    }

    private void OnDestroy()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    public void TakeDamage(int damageValue)
    {
        if (_isInvincible == false)
        {
            _health -= damageValue;

            if (_health <= 0)
            {
                _health = 0;
                Die();
            }

            _isInvincible = true;

            _coroutine = StartCoroutine(EndingInvincibility());
            
            OnTakeDamage?.Invoke();
            
            _healthUI.DisplayCurrentHealth(_health);
        }
    }

    public void AddHealth(int value)
    {
        _health += value;

        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        
        OnHealed?.Invoke();
        
        _healthUI.DisplayCurrentHealth(_health);
    }

    private IEnumerator EndingInvincibility()
    {
        yield return new WaitForSeconds(1.5f);

        _isInvincible = false;
    }

    private void Die()
    {
        Debug.Log("player die");
    }
}
