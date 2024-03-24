using System;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform Spawn;
    
    [SerializeField] private float ShotPeriod = 0.2f;

    [SerializeField] private AudioSource ShotSound;
    [SerializeField] private GameObject Flash;

    private float _timer;

    private Coroutine _flashCoroutine;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > ShotPeriod)
        {
            if (Input.GetMouseButton(0))
            {
                _timer = 0f;
               Bullet newBullet = Instantiate(_bulletPrefab, Spawn.position, Spawn.rotation);
                newBullet.SetVelocity(Spawn.forward);
                ShotSound.Play();
                Flash.SetActive(true);

                _flashCoroutine = StartCoroutine(HideFlashlight());
            }
        }
    }

    private IEnumerator HideFlashlight()
    {
        yield return new WaitForSeconds(0.08f);
        
        Flash.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_flashCoroutine != null)
        {
            StopCoroutine(_flashCoroutine);
        }
    }
}
