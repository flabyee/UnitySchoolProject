using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if(!DataManager.instance.isShoting)
        {
            transform.Translate(Vector2.down * (DataManager.instance.obstacleSpeed * Time.deltaTime));
        }

        if(transform.position.x > 10)
        {
            Destroy(gameObject);
        }
    }

    public void DestroyAnimation()
    {
        // Todo : ºÎ¼ÅÁü È¿°ú
    }
}
