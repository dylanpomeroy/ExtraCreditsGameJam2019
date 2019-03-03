using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private float healthBarInitialWidth;

    private void Start()
    {
        healthBarInitialWidth = GetComponent<RectTransform>().rect.width;
    }

    public void UpdateHealth(float healthPercent)
    {
        var rectTrans = GetComponent<RectTransform>();
        var oldRectTransform = GetComponent<RectTransform>().sizeDelta = new Vector2(healthPercent * healthBarInitialWidth, rectTrans.sizeDelta.y);
    }
}
