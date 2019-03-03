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

        Invoke("SwitchColor", 0.2f);
    }

    private void SwitchColor()
    {
        text.faceColor = text.faceColor == Color.black ? Color.white : Color.black;

        Invoke("SwitchColor", 0.2f);
    }
}
