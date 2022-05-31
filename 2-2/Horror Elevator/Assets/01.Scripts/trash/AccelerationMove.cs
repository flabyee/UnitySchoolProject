using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationMove : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigid;

    private Vector2 dir = Vector2.zero;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        dir.x = Input.acceleration.x;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        rigid.AddForce(dir * speed);
    }
}
