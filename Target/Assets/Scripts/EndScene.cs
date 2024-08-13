using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    private int score;
    [SerializeField]    
    private TextMeshProUGUI scoreText;
    void Start()
    {
        score = PlayerPrefs.GetInt("HighScore");
        scoreText.text = score.ToString();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
