using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private int _score;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void UpdateScore(int score)
    {
        _score += score;
        scoreText.text = "Score: " + _score;
    }

    public void EndGame()
    {
        PlayerPrefs.SetInt("HighScore", _score);
        SceneManager.LoadScene("EndScene");
    }
}
