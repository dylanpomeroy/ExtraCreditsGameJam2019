using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public int Value;

    public EnemyInstantiator EnemyInstantiator;
    public PlayerController PlayerController;
    public bool MoveToPlayerWhenStageEnds;

    private void Update()
    {
        if (MoveToPlayerWhenStageEnds && EnemyInstantiator.ActuallyActiveEnemyCount == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.transform.position, Time.deltaTime * 10);
        }
    }
}
