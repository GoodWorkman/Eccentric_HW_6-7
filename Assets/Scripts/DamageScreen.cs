using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageScreen : MonoBehaviour
{
    [SerializeField] private Image _damageImage;
    

    public void StartEffect()
    {
        StartCoroutine(ShowEffect());
    }

    public IEnumerator ShowEffect()
    {
        _damageImage.enabled = true;
        for (float t = 1; t > 0f; t -= Time.deltaTime * 2f)
        {
            _damageImage.color = new Color(1f, 0f, 0f, t);
            yield return null;
        }
        _damageImage.enabled = false;
    }
}
