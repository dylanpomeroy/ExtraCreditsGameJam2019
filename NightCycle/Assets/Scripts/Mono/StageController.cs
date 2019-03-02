using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    public CoinInstantiator CoinInstantiator;
    public EnemyInstantiator EnemyInstantiator;
    public DarknessController DarknessController;
    public Text GoalText;
    public Text HintText;
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
                        GoalText.text = "Goal: Pick up all the coins off the ground";
                        HintText.text = "Hint: You can move with the WASD keys";
                        for (var i = 0; i < 19; i++)
                        {
                            var randomVector = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
                            var coinPosition = (Vector2)transform.position + randomVector;

                            CoinInstantiator.InstantiateCoin(coinPosition);
                        }
                    },
                    checkCompleted: () => CoinInstantiator.ActuallyActiveCoinCount == 0),
                new StageStep(
                    stepAction: () =>
                    {
                        GoalText.text = "Goal: Purchase a handgun and some ammo";
                        HintText.text = "Hint: Click the purchase buttons and then Done when you're finished";
                        MarketMenu.SetActive(true);
                    },
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
                        GoalText.text = "Goal: Shoot all the enemies before they get to you!";
                        HintText.text = "Hint: You can aim with the mouse and shoot with left click";
                        EnemyInstantiator.SpawnEnemies(10);
                    },
                    checkCompleted: () => EnemyInstantiator.ActuallyActiveEnemyCount == 0),
                new StageStep(
                    stepAction: () => DarknessController.MakeLight(),
                    checkCompleted: () => DarknessController.IsLight),
                new StageStep(
                    stepAction: () =>
                    {
                        GoalText.text = "Goal: Finish collecting all the coins dropped by the enemies";
                        HintText.text = string.Empty;
                    },
                    checkCompleted: () => CoinInstantiator.ActuallyActiveCoinCount == 0),
                new StageStep(
                    stepAction: () =>
                    {
                        GoalText.text = "Goal: Prepare yoursle for the next attack!";
                        HintText.text = "Hint: Once you earn enough money you can purchase better weapons";
                        MarketMenu.SetActive(true);
                    },
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
            GoalText.text = "Congratulations, you beat the game!";
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
