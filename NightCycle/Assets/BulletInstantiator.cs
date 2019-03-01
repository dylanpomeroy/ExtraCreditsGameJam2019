using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInstantiator : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform BulletParent;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }
    }

    public void ShootBullet()
    {
        var bullet = GameObject.Instantiate(BulletPrefab, BulletParent);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
    }
}
