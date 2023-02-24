using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get 
        {
            if (instance is null)
                Debug.LogError("Game Manager is NULL");
            return instance; 
        }
    }
    private void Awake()
    {
        instance = this;
    }

    public bool GameOver = false;

    private UnityEvent playerConnected;

    private IEnumerator Start()
    {
        bool connected = false;

        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("Error starting session");
                return;
            }
            Debug.Log("Connected to LootLocker session");
            connected = true;
        });
        yield return new WaitUntil(() => connected);
        playerConnected.Invoke();
    }

    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI leaderBoardScoreText;
    [SerializeField] TextMeshProUGUI leaderBoardNameText;
    [SerializeField] TMP_InputField nameInput;

    public float score = 0f;
    private float scoreMultiplier = 10f;
    [SerializeField] private UnityEvent<int> scoreUpdateEvent;

    private void Update()
    {

        if (!GameOver)
        {
            score += scoreMultiplier * Time.deltaTime;
            scoreUpdateEvent.Invoke((int)score);
        }
    }

    private int leaderboardID = 11268;
    private int leaderboardTopCount = 10;

    public void EndGame(int score)
    {
        GameOver = true;
        gameOverCanvas.SetActive(true);
        this.score = score;
        scoreText.text = score.ToString();
        GetLeaderboard();
    }

    private void GetLeaderboard()
    {
        LootLockerSDKManager.GetScoreList(leaderboardID, leaderboardTopCount, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully got leaderboard");
                string leaderboardName = "";
                string leaderboardScore = "";
                LootLockerLeaderboardMember[] members = response.items;
                for (int i = 0; i < members.Length; i++)
                {
                    LootLockerPlayer player = members[i].player;
                    if (player == null) continue;

                    if (player.name != "")
                    {
                        leaderboardName += player.name + "\n";
                    }
                    else
                    {
                        leaderboardName += player.id + "\n";
                    }
                    leaderboardScore += members[i].score + "\n";
                }
                leaderBoardNameText.SetText(leaderboardName);
                leaderBoardScoreText.SetText(leaderboardScore);
            }
            else
            {
                Debug.Log("Unable to get leaderboard");
            }
        });
    }

    public void SubmitScore()
    {
        StartCoroutine(SubmitToLeaderboard());
    }

    IEnumerator SubmitToLeaderboard()
    {
        bool? nameSet = null;
        LootLockerSDKManager.SetPlayerName(nameInput.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Player name successfully submitted");
                nameSet = true;
            }
            else
            {
                Debug.Log("Player name not submitted");
                nameSet = false;
            }
        });
        yield return new WaitUntil(() => nameSet.HasValue);
        if (!nameSet.Value) yield break;
        bool? scoreSubmitted = null;
        LootLockerSDKManager.SubmitScore("", (int)score, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Player score submitted");
                scoreSubmitted = true;
            }
            else
            {
                Debug.Log("Unable to submit score");
                scoreSubmitted = true;
            }
        });
        yield return new WaitUntil(() => scoreSubmitted.HasValue);
        if (!scoreSubmitted.Value) yield break;
        GetLeaderboard();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
