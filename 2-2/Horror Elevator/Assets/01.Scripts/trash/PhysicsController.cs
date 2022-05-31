using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour
{
    public float xSpeed;
    public float ShakeForceMultiplier;
    public Rigidbody2D rigid;

    private float dirX;

    public void ShakeRigidbodies(Vector3 deviceAcceleration)
    {
        dirX = Input.acceleration.x;

        if (dirX > 1)
            dirX = 1;

        rigid.AddForce(new Vector2(dirX * xSpeed, deviceAcceleration.y * ShakeForceMultiplier), ForceMode2D.Impulse);
    }
}
