using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Transform Player;
    void Update()
    {
        this.transform.position = new Vector3(Player.position.x, 1, 10);
    }
}
