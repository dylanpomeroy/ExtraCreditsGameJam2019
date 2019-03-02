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
        stages = new List<Stage>
        {
            new Stage(
            0,
            new List<StageStep>
            {
                new StageStep(
                    stepAction: () =>
                    {
                        SetTexts(
                            "Pick up all the coins off the ground",
                            "You can move with the WASD keys");

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
                        SetTexts(
                            "Purchase a handgun and x900 ammo",
                            "Click the Purchase buttons and then Done when you're finished");

                        MarketMenu.SetActive(true);
                    },
                    checkCompleted: () => !MarketMenu.activeSelf),
            }),
            new Stage(
            1,
            new List<StageStep>
            {
                new StageStep(
                    stepAction: () => DarknessController.MakeDark(),
                    checkCompleted: () => DarknessController.IsDark),
                new StageStep(
                    stepAction: () =>
                    {
                        SetTexts(
                            "Shoot all the enemies before they get to you!",
                            "You can aim with the moue and shoot with left click");

                        EnemyInstantiator.SpawnEnemies(10);
                    },
                    checkCompleted: () => EnemyInstantiator.ActuallyActiveEnemyCount == 0),
                new StageStep(
                    stepAction: () => DarknessController.MakeLight(),
                    checkCompleted: () => DarknessController.IsLight),
                new StageStep(
                    stepAction: () =>
                    {
                        SetTexts(
                            "Finish collecting all the coints dropped by the enemies");

                        HintText.text = string.Empty;
                    },
                    checkCompleted: () => CoinInstantiator.ActuallyActiveCoinCount == 0),
                new StageStep(
                    stepAction: () =>
                    {
                        SetTexts(
                            "Prepare yourself for the text attack!",
                            "Once you earn enough money you can purchase better weapons");

                        MarketMenu.SetActive(true);
                    },
                    checkCompleted: () => !MarketMenu.activeSelf),
            })
        };
    }

    private void SetTexts(string goalText = null, string hintText = null)
    {
        if (goalText != null)
        {
            GoalText.text = $"Goal: {goalText}";
        }

        if (hintText != null)
        {
            HintText.text = $"Hint: {hintText}";
        }
    }

    private void Update()
    {
        if (currentStageIndex == stages.Count)
        {
            GoalText.text = "Congratulations, you beat the game!";
            return;
        }

        var currentStage = stages[currentStageIndex];
        if (!currentStage.IsStarted)
        {
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
