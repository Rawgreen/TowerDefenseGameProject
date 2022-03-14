using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void BtnQuit()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Lvl1()
    {
        SceneManager.LoadScene("Map1");
    }

    public void Lvl2()
    {
        SceneManager.LoadScene("Map2");
    }
}
