using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInstantiator : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform BulletParent;

    public int BulletPoolSize;

    private static Queue<GameObject> BulletPool;
    private static Queue<GameObject> ActiveBullets;

    public static void DestroyBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        BulletPool.Enqueue(bullet);
    }

    private void Start()
    {
        BulletPool = new Queue<GameObject>();
        ActiveBullets = new Queue<GameObject>();

        for (var i = 0; i < BulletPoolSize; i++)
        {
            var newBullet = Instantiate(BulletPrefab, BulletParent);
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
        newBullet.transform.position = new Vector3(newBullet.transform.position.x, newBullet.transform.position.y, 0);

        ActiveBullets.Enqueue(newBullet);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Enemy ran OnCollisionEnter");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enemy ran OnTriggerEnter");
    }
}
