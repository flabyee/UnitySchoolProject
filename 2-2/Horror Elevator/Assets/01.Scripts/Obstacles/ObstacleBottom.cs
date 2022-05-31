using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBottom : Obstacle
{
    protected override void OnCol()
    {
        sr.sprite = sprites[Random.Range(1, 4)];
        base.OnCol();
    }
}
