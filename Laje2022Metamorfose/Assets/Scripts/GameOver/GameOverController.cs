using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void HandleRestartBtn() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void HandleQuitBtn() {
        Application.Quit();
    }
}
