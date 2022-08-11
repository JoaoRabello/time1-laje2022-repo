using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void HandleRestartBtn() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainGame");
    }

    public void HandleQuitBtn() {
        Application.Quit();
    }
}
