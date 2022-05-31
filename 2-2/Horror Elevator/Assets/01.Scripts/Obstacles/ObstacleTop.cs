using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTop : Obstacle
{
    bool canMove = false;
    public float speed = 5f;

    private void Update()
    {
        if(canMove)
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }
    }

    protected override IEnumerator Change()
    {
        yield return new WaitForSeconds(changeDelay);

        canMove = true;

        yield return new WaitForSeconds(onTime);

        canMove = false;

        gameObject.SetActive(false);
    }
}
