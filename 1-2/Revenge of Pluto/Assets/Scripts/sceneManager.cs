using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public void StageSelect()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
    public void OneStage()
    {
        SceneManager.LoadScene("OneStageScene");
    }
    public void TwoStage()
    {
        SceneManager.LoadScene("TwoStageScene");
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
