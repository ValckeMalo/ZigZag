using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public static GameData Instance;
    [SerializeField] private int[] collectibles;
    [SerializeField] private int score = 0;
    [SerializeField] private int partyPlaYED = 0;
    public bool isPlay = false;

    public static bool PlayerIsDead { get; set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        DontDestroyOnLoad(this);
        collectibles = new int[(int)CollectibleType.Count] { 0 };
        score = 0;
        PlayerIsDead = false;
    }

    public void Start()
    {
        HUD.OnScoreUpdate?.Invoke(score);
    }

    public void AddColectible(CollectibleType type)
    {
        collectibles[(int)type]++;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        HUD.OnScoreUpdate?.Invoke(score);
    }

    public void ResetGame()
    {
        isPlay = false;
        partyPlaYED++;
        PlayerPrefs.SetInt("partyPlaYED", partyPlaYED);
        if (score > PlayerPrefs.GetFloat("Score"))
        {
            PlayerPrefs.SetFloat("Score", score);
        }
        score = 0;
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        isPlay = true;
    }
}