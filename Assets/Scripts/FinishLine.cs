using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishLine : MonoBehaviour
{
    public TextMeshProUGUI notEnoughEggsText; // Reference to the UI text element
    public int numberOfFlashes = 3; // The number of times the text will flash
    public float flashDuration = 0.5f; // The duration of a single flash

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.remainingEggs <= 0)
            {
                // The player can pass to the next level
                SceneManager.LoadScene("Editing Level"); // Load the next level
            }
            else
            {
                // The player can't pass, they need to collect all the eggs
                // Start the flashing text coroutine
                StartCoroutine(FlashNotEnoughEggsText());
            }
        }
    }

    private IEnumerator FlashNotEnoughEggsText()
    {
        for (int i = 0; i < numberOfFlashes; i++)
        {
            notEnoughEggsText.gameObject.SetActive(true);
            yield return new WaitForSeconds(flashDuration);
            notEnoughEggsText.gameObject.SetActive(false);
            yield return new WaitForSeconds(flashDuration);
        }
    }
}
