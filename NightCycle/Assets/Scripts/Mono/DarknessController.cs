﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DarknessController : MonoBehaviour
{
    private bool shouldBeDark;

    public float darkAlpha;
    public float lightAlpha;

    public bool IsDark;
    public bool IsLight;

    private List<SpriteRenderer> groundRenders;
    
    public void MakeDark()
    {
        shouldBeDark = true;
    }

    public void MakeLight()
    {
        shouldBeDark = false;
    }

    void Start()
    {
        groundRenders = GetComponentsInChildren<SpriteRenderer>().Where(render => render.name.Contains("Dirt")).ToList();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            shouldBeDark = true;
        if (Input.GetKeyDown(KeyCode.M))
            shouldBeDark = false;

        var darkness = groundRenders[1];
        if (shouldBeDark && darkness.color.a > darkAlpha)
        {
            var tempColor = darkness.color;
            tempColor.a = darkAlpha;
            darkness.color = Color32.Lerp(darkness.color, tempColor, Time.deltaTime);
        }
        else if (!shouldBeDark && darkness.color.a < lightAlpha)
        {
            var tempColor = darkness.color;
            tempColor.a = lightAlpha;
            darkness.color = Color32.Lerp(darkness.color, tempColor, Time.deltaTime);
        }

        IsDark = darkness.color.a < darkAlpha + 0.3f;
        IsLight = darkness.color.a > lightAlpha - 0.3f;

        groundRenders.ForEach(render => render.color = darkness.color);
    }
}
