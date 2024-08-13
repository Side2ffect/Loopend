using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float currentTime = 0f;
    [SerializeField] private float startingTime = 90f;
    [SerializeField] private Text currentTimeText;

    private GameManager gameManager;
    void Start()
    {
        currentTime = startingTime;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = 0f;
            gameManager.EndGame();
        }

        DisplayTime(currentTime);
    }

    void DisplayTime(float displayTime)
    { 
        if (displayTime < 0)
        {
            displayTime = 0f;
        }

        float minute = Mathf.FloorToInt(displayTime / 60);
        float second = Mathf.FloorToInt(displayTime % 60);

        currentTimeText.text = string.Format("{0:00}:{1:00}", minute, second);
    }

    public void AddTime(float addTime)
    {
        currentTime += addTime;
    }

    public void ReduceTime(float reduceTime)
    {
        currentTime -= reduceTime;
    }
}
