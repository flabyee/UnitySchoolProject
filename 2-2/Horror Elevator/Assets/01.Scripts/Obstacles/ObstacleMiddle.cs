using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMiddle : Obstacle
{
    protected override void OnCol()
    {
        sr.sprite = sprites[1];
        base.OnCol();
    }
}
