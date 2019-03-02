using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public CoinInstantiator CoinInstantiator;
    public EnemyInstantiator EnemyInstantiator;
    public DarknessController DarknessController;
    public GameObject MarketMenu;

    private List<Stage> stages;
    private int currentStageIndex;

    private void Start()
    {
        stages = new List<Stage>();

        var startingStage = new Stage(
            0,
            new List<StageStep>
            {
                new StageStep(
                    stepAction: () =>
                    {
                        for (var i = 0; i < 19; i++)
                        {
                            var randomVector = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
                            var coinPosition = (Vector2)transform.position + randomVector;

                            CoinInstantiator.InstantiateCoin(coinPosition);
                        }
                    },
                    checkCompleted: () => CoinInstantiator.ActuallyActiveCoinCount == 0),
                new StageStep(
                    stepAction: () => MarketMenu.SetActive(true),
                    checkCompleted: () => !MarketMenu.activeSelf),
            });

        var stage1 = new Stage(
            1,
            new List<StageStep>
            {
                new StageStep(
                    stepAction: () => DarknessController.MakeDark(),
                    checkCompleted: () => DarknessController.IsDark),
                new StageStep(
                    stepAction: () =>
                    {
                        EnemyInstantiator.SpawnEnemies(10);
                    },
                    checkCompleted: () => EnemyInstantiator.ActuallyActiveEnemyCount == 0),
                new StageStep(
                    stepAction: () => DarknessController.MakeLight(),
                    checkCompleted: () => DarknessController.IsLight),
                new StageStep(
                    stepAction: () =>
                    {

                    },
                    checkCompleted: () => CoinInstantiator.ActuallyActiveCoinCount == 0),
                new StageStep(
                    stepAction: () => MarketMenu.SetActive(true),
                    checkCompleted: () => !MarketMenu.activeSelf),
            });

        stages.Add(startingStage);
        stages.Add(stage1);
    }

    private void Update()
    {
        if (currentStageIndex == stages.Count)
        {
            Debug.Log("Game Complete!");
            return;
        }

        var currentStage = stages[currentStageIndex];
        if (!currentStage.IsStarted)
        {
            Debug.Log($"Starting stage {currentStageIndex}");
            currentStage.Start();
        }
        else if (currentStage.IsInProgress)
        {
            currentStage.Run();
        }
        else
        {
            currentStageIndex++;
        }
    }
}
