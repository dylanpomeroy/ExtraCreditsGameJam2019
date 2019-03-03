using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpriteController : MonoBehaviour
{
    public SpriteRenderer Pistol;
    public SpriteRenderer Shotgun;
    public SpriteRenderer Machinegun;

    private void Start()
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

        Pistol.flipY = transform.position.x > mousePosition.x;
        Shotgun.flipY = transform.position.x > mousePosition.x;
        Machinegun.flipY = transform.position.x > mousePosition.x;
    }

    public void EquipPistol()
    {
        Pistol.gameObject.SetActive(true);
        Shotgun.gameObject.SetActive(false);
        Machinegun.gameObject.SetActive(false);
    }

    public void EquipShotgun()
    {
        Pistol.gameObject.SetActive(false);
        Shotgun.gameObject.SetActive(true);
        Machinegun.gameObject.SetActive(false);
    }

    public void EquipMachinegun()
    {
        Pistol.gameObject.SetActive(false);
        Shotgun.gameObject.SetActive(false);
        Machinegun.gameObject.SetActive(true);
    }
}
