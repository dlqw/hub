using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isPause = false;
    public GameObject pauseMenuUI;
    public Image settingImage;

    void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPause = false;
    }
    public void MainMenu()
    {
        isPause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
    }
    public void Setting()
    {
        if (isPause)
        {
            pauseMenuUI.SetActive(false);
           
            isPause = false;
            Time.timeScale = 0.0f;
            settingImage.enabled = true;
        }
    }
}


