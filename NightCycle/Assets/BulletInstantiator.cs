using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInstantiator : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform BulletParent;

    public int BulletPoolSize;

    private Queue<GameObject> BulletPool;
    private Queue<GameObject> ActiveBullets;

    private void Start()
    {
        BulletPool = new Queue<GameObject>();
        ActiveBullets = new Queue<GameObject>();

        for (var i = 0; i < BulletPoolSize; i++)
        {
            var newBullet = Instantiate(BulletPrefab);
            BulletPool.Enqueue(newBullet);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }
    }

    public void ShootBullet()
    {
        GameObject newBullet;
        if (BulletPool.Count == 0)
        {
            newBullet = ActiveBullets.Dequeue();
        }
        else
        {
            newBullet = BulletPool.Dequeue();
        }

        newBullet.SetActive(true);
        newBullet.transform.position = transform.position;
        newBullet.transform.rotation = transform.rotation;

        ActiveBullets.Enqueue(newBullet);
    }
}
