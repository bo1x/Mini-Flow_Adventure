using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("TestScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Hover(GameObject go)
    {
        go.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
    }

    public void UnHover(GameObject go)
    {
        go.GetComponent<Image>().color = new Color(1f, 1f, 1f);
    }
}
