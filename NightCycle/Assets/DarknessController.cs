using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarknessController : MonoBehaviour
{
    private RawImage image;

    public void MakeDark()
    {
        var color = image.color;
        color.a = 0.7f;
        image.color = color;
    }

    public void MakeLight()
    {
        var color = image.color;
        color.a = 0.0f;
        image.color = color;
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            MakeDark();
        if (Input.GetKeyDown(KeyCode.M))
            MakeLight();
    }
}
