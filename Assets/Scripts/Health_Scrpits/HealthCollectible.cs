using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Health playerHealth = collision.GetComponent<Health>();

            if (playerHealth.currentHealth < playerHealth.GetStartingHealth())
            {
                playerHealth.AddHealth(healthValue);
                gameObject.SetActive(false);
            }
        }
    }
}