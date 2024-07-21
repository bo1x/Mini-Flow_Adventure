using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseBehabiur : MonoBehaviour
{
    [SerializeField] private bool Win;
    // Start is called before the first frame update
    void Start()
    {
        if (Win)
        {
            Music.instance.PlayWinMusic();
        }
        else
        {
            Music.instance.PlayLoseMusic();
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    
}
