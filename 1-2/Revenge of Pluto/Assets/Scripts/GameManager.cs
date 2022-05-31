using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject OptionMenuObject = null;
    // Start is called before the first frame update
    void Start()
    {
        OptionMenuObject.SetActive(false);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void touchOptionOpenButton()
    {

        OptionMenuObject.SetActive(true);
        Time.timeScale = 0;

    }
    public void touchOptionCloseButton()
    {
        OptionMenuObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void touchOneStageRestartButton()
    {
        SceneManager.LoadScene("OneStageScene");
        Time.timeScale = 1;
    }
    public void touchTwoStageRestartButton()
    {
        SceneManager.LoadScene("TwoStageScene");
        Time.timeScale = 1;
    }
    public void touchStageSelectButton()
    {
        SceneManager.LoadScene("StageSelectScene");
        Time.timeScale = 1;
    }
}
