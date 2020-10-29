using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject controlsSub;
    public GameObject creditsSub;
    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void Controls()
    {
        controlsSub.SetActive(true);
    }

    public void BackControls()
    {
        controlsSub.SetActive(false);
    }

    public void Credits()
    {
        creditsSub.SetActive(true);
    }

    public void BackCredits()
    {
        creditsSub.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
