using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AmmoCreator : MonoBehaviour
{
   [SerializeField] private GameObject _ammoPrefab;
   [SerializeField] private Transform _spawnPoint;

   public void CreateAmmo()
   {
      Instantiate(_ammoPrefab, _spawnPoint.position, quaternion.identity, transform);
   }
}
