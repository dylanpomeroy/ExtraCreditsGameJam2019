using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverFlasher : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Color32 goalColor;

    private void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        goalColor = Color.black;

        Invoke("SwitchColorGoal", 0.2f);
    }

    private void Update()
    {
        text.faceColor = Color32.Lerp(text.faceColor, goalColor, 0.1f);
    }

    private void SwitchColorGoal()
    {
        if (goalColor == Color.black)
        {
            goalColor = Color.white;
        }
        else
        {
            goalColor = Color.black;
        }

        Invoke("SwitchColorGoal", 0.2f);
    }
}
