using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (!DataManager.instance.isShoting)
        {
            transform.Translate(Vector2.right * (DataManager.instance.obstacleSpeed * Time.deltaTime));
        }

        if (transform.position.x > 10)
        {
            Destroy(gameObject);
        }
    }
}
