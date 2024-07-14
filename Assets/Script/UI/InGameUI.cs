using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject Interface;
    private bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        Interface.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && paused ==false)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            Resume();
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
        Time.timeScale += 1.0f;
        paused = false;
        SceneManager.LoadScene("MainMenu");
        
    }
}
