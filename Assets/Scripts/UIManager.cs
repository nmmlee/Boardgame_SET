using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameManger gameManger;
    public GameObject menuUI;
    public GameObject setUI;
    public GameObject pauseUI;

    public void GameStart()
    {
        gameManger.isGameStart = true;
        menuUI.SetActive(false);
        setUI.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        gameManger.isPause = true;
        pauseUI.SetActive(true);
        
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameManger.isPause = false;
        pauseUI.SetActive(false);
    }
}
