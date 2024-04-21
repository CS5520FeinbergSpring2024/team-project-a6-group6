using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class JetMultiSimulator : MonoBehaviour
{
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject player3;
    [SerializeField] GameObject player4;
    [SerializeField] JetGameManagerLogic jetGameManagerLogic;
    [SerializeField] GameObject multiUI;
    [SerializeField] GameObject[] objectsToHide;
    [SerializeField] Button[] buttonsToDisable;
    [SerializeField] TMP_Text gameStartCD;
    [SerializeField] TMP_Text inGameCD;
    [SerializeField] int scoreScale = 100;
    [SerializeField] float minTimeScore = 1.2f;
    [SerializeField] float maxTimeScore = 9f;
    [SerializeField] float ratioMinTimeSpeedUp = 0.9955f;
    [SerializeField] float ratioMaxTimeSpeedUp = 0.9965f;
    private Dictionary<string, int> playerScores = new Dictionary<string, int>();
    public float gameDuration = 180f;
    public bool isGameOver = false;
    public bool isBossOneSpawned = false;
    public bool isBossSecondSpawned = false;
    public void Start()
    {
        //AudioManager.Instance.PlayMusic("BGM For Snake");
        player1.GetComponentInChildren<TMP_Text>().text = "YOU";
        if (MultiSingleManager.Instance.isMulti)
        {
            playerScores.Add("YOU", 0);
            playerScores.Add("P2", 0);
            playerScores.Add("P3", 0);
            playerScores.Add("P4", 0);

            player1.SetActive(true);
            player2.SetActive(true);
            player3.SetActive(true);
            player4.SetActive(true);
            player1.transform.Find("Score").GetComponentInChildren<TMP_Text>().text = "0";
            player2.transform.Find("Score").GetComponentInChildren<TMP_Text>().text = "0";
            player3.transform.Find("Score").GetComponentInChildren<TMP_Text>().text = "0";
            player4.transform.Find("Score").GetComponentInChildren<TMP_Text>().text = "0";
            setButtonToDisable(true);
            setObjectsToHide(true);
            StartCoroutine(StartGameCountdown());
            Time.timeScale = 0;
        }
        else
        {
            player1.SetActive(true);
            player2.SetActive(false);
            player3.SetActive(false);
            player4.SetActive(false);
            multiUI.SetActive(false);
        }
    }


    private void setObjectsToHide(bool makeHide)
    {
        if (makeHide)
        {
            foreach (GameObject o in objectsToHide)
            {
                o.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject o in objectsToHide)
            {
                o.SetActive(true);
            }
        }
    }
    public void setButtonToDisable(bool makeDisable)
    {
        if (makeDisable)
        {
            foreach (Button button in buttonsToDisable)
            {
                button.enabled = false;
            }
        }
        else
        {
            foreach (Button button in buttonsToDisable)
            {
                button.enabled = true;
            }
        }
    }

    private void Update()
    {
        if (!isGameOver && Time.timeScale == 1)
        {
            if (MultiSingleManager.Instance.isMulti)
            {
                gameDuration -= Time.deltaTime;
            }
            if (gameDuration <= 0)
            {
                GameOver();
            }
            else
            {
                UpdateInGameCountdown();
            }
        }
        UpdateLeaderboard();
    }

    private IEnumerator StartGameCountdown()
    {
        gameStartCD.text = "Three";
        yield return new WaitForSecondsRealtime(1f);

        gameStartCD.text = "Two";
        yield return new WaitForSecondsRealtime(1f);

        gameStartCD.text = "One";
        yield return new WaitForSecondsRealtime(1f);

        gameStartCD.text = "GO!";
        yield return new WaitForSecondsRealtime(1f);

        gameStartCD.gameObject.SetActive(false);

        jetGameManagerLogic.PressToStart();

        setButtonToDisable(false);

        // Start in-game countdown
        StartCoroutine(InGameCountdown());

        // Start scoring for players 2 to 4
        StartCoroutine(SimulateCPUPlayersScoring("P2"));
        StartCoroutine(SimulateCPUPlayersScoring("P3"));
        StartCoroutine(SimulateCPUPlayersScoring("P4"));
    }

    private IEnumerator InGameCountdown()
    {
        while (gameDuration > 0)
        {
            UpdateInGameCountdown();
            yield return null;
        }

    }

    private void UpdateInGameCountdown()
    {
        int minutes = Mathf.FloorToInt(gameDuration / 60);
        int seconds = Mathf.FloorToInt(gameDuration % 60);
        inGameCD.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void GameOver()
    {
        isGameOver = true;
        inGameCD.enabled = false;
        Time.timeScale = 0;
        multiUI.GetComponent<MultiUIController>().GameOver(playerScores);
    }

    private IEnumerator SimulateCPUPlayersScoring(string player)
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(Random.Range(minTimeScore, maxTimeScore));
            UpdateScore(player, scoreScale);
            minTimeScore *= ratioMinTimeSpeedUp;
            maxTimeScore *= ratioMaxTimeSpeedUp;
            float randomBossDelay = Random.Range(8f, 15f);
            if (!isBossOneSpawned && 180 - gameDuration + randomBossDelay > 70)
            {
                isBossOneSpawned = true;
                float cpuKillBossRoll = Random.Range(0f, 6f);
                if (cpuKillBossRoll > 0.5f) 
                {
                    UpdateScore(player, 450);
                }
            } 
            else if (!isBossSecondSpawned && 180 - gameDuration + randomBossDelay > 140)
            {
                isBossSecondSpawned = true;
                float cpuKillBossRoll = Random.Range(0f, 6f);
                if (cpuKillBossRoll > 3.75f)
                {
                    UpdateScore(player, 450);
                }
            }
        }
    }

    public void UpdateScore(string playerName, int scoreToAdd)
    {
        if (playerName == "YOU")
        {
            playerScores[playerName] = scoreToAdd;
        }
        else
        {
            playerScores[playerName] += scoreToAdd;
        }
    }

    private void UpdateLeaderboard()
    {
        var sortedPlayers = playerScores.OrderByDescending(x => x.Value);
        int rank = 1;
        foreach (var player in sortedPlayers)
        {
            GameObject playerUI = GetPlayerUI("Player"+rank.ToString());
            Transform playerNameUI = playerUI.transform.Find("Name");
            Transform playerScoreUI = playerUI.transform.Find("Score");
            playerNameUI.GetComponent<TMP_Text>().text = player.Key;
            playerScoreUI.GetComponentInChildren<TMP_Text>().text = player.Value.ToString();
            rank++;
        }
    }
    private GameObject GetPlayerUI(string playerName)
    {
        switch (playerName)
        {
            case "Player1":
                return player1;
            case "Player2":
                return player2;
            case "Player3":
                return player3;
            case "Player4":
                return player4;
            default:
                return null;
        }
    }
}
