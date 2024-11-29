using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI partyPlaYED;

    public delegate void EventUpdateScore(int scoreAmount);
    public static EventUpdateScore OnScoreUpdate;

    public void Awake()
    {
        OnScoreUpdate += UpdateScore;
    }

    public void Start()
    {
        bestScore.text = "Best Score : " + PlayerPrefs.GetFloat("Score").ToString();
        partyPlaYED.text = "Party played : " + PlayerPrefs.GetInt("partyPlaYED").ToString();
    }

    public void UpdateScore(int scoreAmount)
    {
        scoreText.text = scoreAmount.ToString();
    }

    public void StartGame()
    {
        GameData.Instance.StartGame();
    }
}