using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Import TextMeshPro namespace

public class MenuManager : MonoBehaviour
{
    private TMP_InputField playerNameInput; // TMP_InputField for player name
    private TextMeshProUGUI bestScoreText;  // TextMeshProUGUI for best score

    private void Start()
    {
        // Find the TMP_InputField and TextMeshProUGUI by their names
        playerNameInput = GameObject.Find("Name Input").GetComponent<TMP_InputField>();
        bestScoreText = GameObject.Find("Bestscore Text").GetComponent<TextMeshProUGUI>();

        // Display the best score from PlayerPrefs
        if (PlayerPrefs.HasKey("BestScore"))
        {
            int bestScore = PlayerPrefs.GetInt("BestScore");
            string bestScorePlayer = PlayerPrefs.GetString("BestScorePlayer", "Unknown");
            bestScoreText.text = "Bestscore: " + bestScore + " (" + bestScorePlayer + ")";
        }
        else
        {
            bestScoreText.text = "Bestscore: 0";
        }
    }

    public void StartGame()
    {
        // Save player name to PlayerPrefs
        string playerName = playerNameInput.text;
        PlayerPrefs.SetString("PlayerName", playerName);

        // Load the main game scene
        SceneManager.LoadScene("main");
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();

        // If running in the Unity editor, stop playing
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
