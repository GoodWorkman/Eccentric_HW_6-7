using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
   [SerializeField] private GameObject _healthIconPrefab;

   private List<GameObject> _icons = new();

   public void Setup(int maxHealth)
   {
      for (int i = 0; i < maxHealth; i++)
      {
         GameObject icon = Instantiate(_healthIconPrefab, transform);
         
         _icons.Add(icon);
      }
   }
   
   public void DisplayCurrentHealth(int currentHealth)
   {
      for (int i = 0; i < _icons.Count; i++)
      {
         if (i < currentHealth)
         {
            _icons[i].SetActive(true);
         }
         else
         {
            _icons[i].SetActive(false);
         }
      }
   }
}
