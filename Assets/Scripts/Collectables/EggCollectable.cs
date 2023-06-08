using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCollectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.CollectEgg();
            Destroy(gameObject);
        }
    }
}
