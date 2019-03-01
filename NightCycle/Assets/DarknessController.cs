using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DarknessController : MonoBehaviour
{
    private bool shouldBeDark;

    public float darkAlpha;
    public float lightAlpha;

    private List<SpriteRenderer> groundRenders;

    public void MakeDark()
    {
        shouldBeDark = true;
    }

    public void MakeLight()
    {
        shouldBeDark = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        groundRenders = GetComponentsInChildren<SpriteRenderer>().Where(render => render.name.Contains("Dirt")).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            MakeDark();
        if (Input.GetKeyDown(KeyCode.M))
            MakeLight();

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

        groundRenders.ForEach(render => render.color = darkness.color);
    }
}
