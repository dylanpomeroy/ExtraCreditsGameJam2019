using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float Speed;

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * Speed;
    }
}
