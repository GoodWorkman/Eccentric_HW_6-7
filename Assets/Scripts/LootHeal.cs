using UnityEngine;

public class LootHeal : MonoBehaviour
{
    [SerializeField] private int _healValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
        {
            PlayerHealth playerHealth = other.attachedRigidbody.GetComponent<PlayerHealth>();

            if (playerHealth)
            {
                playerHealth.AddHealth(_healValue);
                
                Destroy(gameObject);
            }
        }
    }
}
