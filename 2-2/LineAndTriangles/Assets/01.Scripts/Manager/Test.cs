using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    private static Test _instance;
    public static Test Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.Log("SceneManager Singleton");
        }
        else
        {
            _instance = this;
        }
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
