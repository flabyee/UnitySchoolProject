using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private void Update()
    {
        if (this.transform.position.y < -3)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
    private void OnTriggerEnter2D(Collider2D Enemy)
    {
        if (Enemy.tag == "Enemy")
        {
            SceneManager.LoadScene("GameOverScene");
        }
        if (Enemy.tag == "End")
        {
            SceneManager.LoadScene("StageSelectScene");
        }
    }
}
