using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct Wave
{
    public List<int> WaveEnemNum;
}

public class GameManager : MonoBehaviour
{

    [SerializeField] List<Wave> Levels = new List<Wave>();
    public EnemySpawner Spawner;

    public TowerController Tower;
    public PlayerController Player;
    public GameObject UI_HUD;
    public GameObject UI_LevelUpgrades;

    public TextMeshProUGUI LevelTxt;
    public TextMeshProUGUI WaveTxt;
    public TextMeshProUGUI CurrencyTxt;
    public TextMeshProUGUI HealthTxt;

    public int LevelNum = 0;
    public int WaveNum = 0;

    public int CurrentEnemies;
    public int Currency;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Spawner.GameplayManager = this;
        Tower.GameplayManager = this;
        StartCoroutine(SpawnEnemGM());

        WaveTxt.text = $"Wave - {WaveNum + 1}";
        LevelTxt.text = $"Level - {LevelNum + 1}";
        CurrencyTxt.text = $"Currency - {Currency}";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        Player.transform.position = Tower.SpawnPlayerLocation();
    }

    public void GameEnd()
    {
        StartCoroutine(RestartLvl());
    }

    IEnumerator RestartLvl()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator SpawnEnemGM()
    {
        int CurrEnemNum = 0;
        //Debug.Log("SpawningEnem");
        CurrentEnemies = Levels[LevelNum].WaveEnemNum[WaveNum];

        while (CurrEnemNum < Levels[LevelNum].WaveEnemNum[WaveNum])
        {
            Spawner.SpawnEnem();
            CurrEnemNum++;

            yield return new WaitForSeconds(2f);
        }
    }

    public void EnemDeadCheck()
    {
        CurrentEnemies = CurrentEnemies - 1;

        Currency++;
        CurrencyTxt.text = $"Currency - {Currency}";

        if (CurrentEnemies <= 0)
        {
            WaveNum++;
            WaveTxt.text = $"Wave - {WaveNum + 1}";

            if (WaveNum >= Levels[LevelNum].WaveEnemNum.Count)
            {
                LevelNum++;
                WaveNum = 0;
                WaveTxt.text = $"Wave - {WaveNum + 1}";
                LevelTxt.text = $"Level - {LevelNum + 1}";

                Time.timeScale = 0f;
                UI_LevelUpgrades.SetActive(true);
            }
            StartCoroutine(SpawnEnemGM());
        }
    }

    

    public void UpgradeArrowIntervals()
    {
        
        if (Tower.ArrowInteraval > .5f)
        {
            Tower.ArrowInteraval = Tower.ArrowInteraval - 0.5f;
            Currency = Currency - 5;
            CurrencyTxt.text = $"Currency - {Currency}";
            UI_LevelUpgrades.SetActive(false);
            Time.timeScale = 1f;
        }
        

        if (LevelNum == Levels.Count)
        {
            RestartLvl();
            return;
        }
    }

    public void UpgradeRepairs()
    {
        if (Tower.Health != 10)
        {
            Currency = Currency - 3;
            CurrencyTxt.text = $"Currency - {Currency}";
            Tower.Health = 10;
            UI_LevelUpgrades.SetActive(false);
            Time.timeScale = 1f;
        }

        if (LevelNum == Levels.Count)
        {
            RestartLvl();
            return;
        }
    }

    public void UpgradeDefence()
    {
        UI_LevelUpgrades.SetActive(false);
        Time.timeScale = 1f;
        Currency = Currency - 5;
        CurrencyTxt.text = $"Currency - {Currency}";

        if (LevelNum == Levels.Count)
        {
            RestartLvl();
            return;
        }
    }

}
