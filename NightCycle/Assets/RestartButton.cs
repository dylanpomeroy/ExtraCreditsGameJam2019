using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public TextMeshProUGUI ButtonText;

    private Color goalColor = Color.white;

    public void Pressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void MouseEnter()
    {
        goalColor = Color.black;
    }

    public void MouseExit()
    {
        goalColor = Color.white;
    }

    private void Update()
    {
        ButtonText.faceColor = Color.Lerp(ButtonText.faceColor, goalColor, Time.deltaTime * 5);
    }
}
