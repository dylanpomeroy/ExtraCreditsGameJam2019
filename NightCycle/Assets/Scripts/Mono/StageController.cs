using Assets.Scripts;
using ExtensionMethods;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    public CoinInstantiator CoinInstantiator;
    public EnemyInstantiator EnemyInstantiator;
    public DarknessController DarknessController;
    public PlayerController PlayerController;
    public TextMeshProUGUI StageText;
    public TextMeshProUGUI GoalText;
    public TextMeshProUGUI HintText;
    public GameObject MarketMenu;

    private List<Stage> stages;
    private int currentStageIndex;

    public AudioSource SoundPlayer;
    public List<AudioClip> StageStartSounds;

    private void Start()
    {
        Debug.Log("Starting stage controller.");
        stages = new List<Stage>();

        CoinInstantiator.ActuallyActiveCoinCount = 0;
        EnemyInstantiator.ActuallyActiveEnemyCount = 0;

        SetInitialStages();

        for (var i = 2; i <= 10; i++)
        {
            SetRecurringStages(
                stageId: i,
                enemiesToSpawn: i * 10);
        }
    }

    private void SetRecurringStages(int stageId, int enemiesToSpawn)
    {
        stages.Add(new Stage(
            stageId,
            new List<StageStep>
            {
                new StageStep(
                    stepAction: () => DarknessController.MakeDark(),
                    checkCompleted: () => DarknessController.IsDark),
                new StageStep(
                    stepAction: () =>
                    {
                        SetTexts($"{stageId}",
                            "Shoot all the enemies before they get to you!",
                            string.Empty);

                        SoundPlayer.PlayOneShot(StageStartSounds.GetRandom(), 2f);

                        EnemyInstantiator.SpawnEnemies(enemiesToSpawn);
                    },
                    checkCompleted: () => EnemyInstantiator.ActuallyActiveEnemyCount == 0),
                new StageStep(
                    stepAction: () => DarknessController.MakeLight(),
                    checkCompleted: () => DarknessController.IsLight),
                new StageStep(
                    stepAction: () =>
                    {
                        SetTexts(null,
                            "Finish collecting all the coints dropped by the enemies");

                        PlayerController.CurrentHealth = 100;
                    },
                    checkCompleted: () => CoinInstantiator.ActuallyActiveCoinCount == 0),
                new StageStep(
                    stepAction: () =>
                    {
                        SetTexts(null,
                            "Prepare yourself for the next attack!");

                        MarketMenu.SetActive(true);
                    },
                    checkCompleted: () => !MarketMenu.activeSelf),
            }));
    }

    private void SetInitialStages()
    {
        stages.Add(new Stage(
            0,
            new List<StageStep>
            {
                new StageStep(
                    stepAction: () =>
                    {
                        SetTexts("0",
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
                        SetTexts(null,
                            "Purchase a handgun and x900 ammo",
                            "Click the Purchase buttons and then Done when you're finished");

                        MarketMenu.SetActive(true);
                    },
                    checkCompleted: () => !MarketMenu.activeSelf),
            }));

        stages.Add(new Stage(
            1,
            new List<StageStep>
            {
                new StageStep(
                    stepAction: () => DarknessController.MakeDark(),
                    checkCompleted: () => DarknessController.IsDark),
                new StageStep(
                    stepAction: () =>
                    {
                        SetTexts("1",
                            "Shoot all the enemies before they get to you!",
                            "You can aim with the mouse and shoot with left click");

                        SoundPlayer.PlayOneShot(StageStartSounds.GetRandom(), 2f);

                        EnemyInstantiator.SpawnEnemies(10);
                    },
                    checkCompleted: () => EnemyInstantiator.ActuallyActiveEnemyCount == 0),
                new StageStep(
                    stepAction: () => DarknessController.MakeLight(),
                    checkCompleted: () => DarknessController.IsLight),
                new StageStep(
                    stepAction: () =>
                    {
                        SetTexts(null,
                            "Finish collecting all the coints dropped by the enemies");

                        PlayerController.CurrentHealth = 100;
                    },
                    checkCompleted: () => CoinInstantiator.ActuallyActiveCoinCount == 0),
                new StageStep(
                    stepAction: () =>
                    {
                        SetTexts(null,
                            "Prepare yourself for the next attack!",
                            "Once you earn enough money you can purchase better weapons");

                        MarketMenu.SetActive(true);
                    },
                    checkCompleted: () => !MarketMenu.activeSelf),
            }));
    }

    private void SetTexts(string stageText = null, string goalText = null, string hintText = null)
    {
        if (stageText != null)
        {
            StageText.text = $"Stage: {stageText}";
        }
        
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
