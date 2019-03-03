using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelController : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating($"{nameof(FlipTowardsMouse)}", 0.1f, 0.1f);
    }

    private void FlipTowardsMouse()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = 5.23f;
        var objectPosition = Camera.main.WorldToScreenPoint(transform.position);
        mousePosition.x = mousePosition.x - objectPosition.x;
        mousePosition.y = mousePosition.y - objectPosition.y;

        GetComponent<SpriteRenderer>().flipX = transform.position.x > mousePosition.x;
    }
}
