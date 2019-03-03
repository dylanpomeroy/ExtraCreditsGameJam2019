using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    public PlayerController PlayerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController.OnTriggerEnter2D(collision);
    }
}
