using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void HandlePlayBtn() {
        SceneManager.LoadScene("MainGame");
    }

    public void HandleExitBtn() {
        Application.Quit();
    }
}
