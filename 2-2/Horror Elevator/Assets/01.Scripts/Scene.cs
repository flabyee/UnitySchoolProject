using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void OnLoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void OnLoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
