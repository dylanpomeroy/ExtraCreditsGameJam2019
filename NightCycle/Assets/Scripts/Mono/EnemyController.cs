using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed;
    public int Health;

    public CoinInstantiator CoinInstantiator; 
    public PlayerController PlayerController;

    private Vector2 RandomSpotInUnitCircle;

    public MovementType typeOfMovement;

    public enum MovementType
    {
        Direct,
        Twitch,
    }

    private bool disableCollisionDetection;

    void OnEnable()
    {
        disableCollisionDetection = false;

        RandomSpotInUnitCircle = Random.insideUnitCircle;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, PlayerController.transform.position) <= 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerController.transform.position, Time.deltaTime * Speed);
        }
        else
        {
            switch (typeOfMovement)
            {
                case MovementType.Direct:
                    transform.position = Vector2.MoveTowards(transform.position, (Vector2)PlayerController.transform.position + RandomSpotInUnitCircle, Time.deltaTime * Speed);
                    break;

                case MovementType.Twitch:
                    transform.position = Vector2.MoveTowards(transform.position, (Vector2)PlayerController.transform.position + RandomSpotInUnitCircle, Time.deltaTime * Speed)
                        + new Vector2(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f));
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            HandleBulletCollision(other);
        }
    }

    private void HandleBulletCollision(Collider2D bulletCollider)
    {
        if (disableCollisionDetection) return;

        transform.position += (transform.position - PlayerController.transform.position).normalized * 10 * Time.deltaTime;

        BulletInstantiator.DestroyBullet(bulletCollider.gameObject);

        Health--;
        if (Health <= 0)
            HandleDeath();
    }

    private void HandleDeath()
    {
        CoinInstantiator.InstantiateCoin(transform.position);

        EnemyInstantiator.DestroyEnemy(this.gameObject);

        disableCollisionDetection = true;
    }
}
