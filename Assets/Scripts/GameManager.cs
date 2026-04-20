using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject winPopup;
    public GameObject losePopup;
    public TextMeshProUGUI timerText;

    public int timeToWin = 30;
    public int timeRemaining;
    float timer = 0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        winPopup.SetActive(false);
        losePopup.SetActive(false);
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                timeRemaining--;
                timerText.text = "Time: " + timeRemaining + "s";
                timer = 0f;
            }
        }
    }

    public void ShowWinPopup()
    {
        winPopup.SetActive(true);
        losePopup.SetActive(false);
    }

    public void ShowLosePopup()
    {
        winPopup.SetActive(false);
        losePopup.SetActive(true);
    }

    public void RestartGame()
    {
        Instance = null;
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
