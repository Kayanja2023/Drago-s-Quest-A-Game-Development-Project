using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EggCounterUI : MonoBehaviour
{
    private TextMeshProUGUI eggCounterText;

    private void Awake()
    {
        eggCounterText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateEggCount();
    }

    private void Update()
    {
        UpdateEggCount();
    }

    private void UpdateEggCount()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.remainingEggs <= 0)
            {
                eggCounterText.text = "You may now pass!";
            }
            else
            {
                eggCounterText.text = $"X{GameManager.Instance.remainingEggs} remaining";
            }
        }
    }
}



