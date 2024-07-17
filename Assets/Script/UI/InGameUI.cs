using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject Interface;
    [SerializeField] private TMP_Text timerText;
    private Timer timer;
    private bool paused = false;

    void Start()
    {
        Interface.SetActive(true);
        timer = GetComponent<Timer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            Resume();
        }

        if (timer.CurrentTime >= 60)
        {
            int minutes = Mathf.FloorToInt(timer.CurrentTime / 60);
            int seconds = Mathf.FloorToInt(timer.CurrentTime % 60);
            timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
        }
        else
        {
            timerText.text = string.Format("{0:0}", Mathf.FloorToInt(timer.CurrentTime));
        }
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        PauseMenu.SetActive(true);
        Interface.SetActive(false);
        paused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        PauseMenu.SetActive(false);
        Interface.SetActive(true);
        paused = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        paused = false;
        SceneManager.LoadScene("MainMenu");
    }
}
