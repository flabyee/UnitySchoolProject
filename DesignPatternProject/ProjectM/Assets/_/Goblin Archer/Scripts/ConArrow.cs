using UnityEngine;
using System.Collections;

public class ConArrow : CONEntity {
	public Vector3 v = new Vector3(0, 0, 0);
    public Vector3 basicV = new Vector3(20, 5, 0);
	public Vector3 a = new Vector3(0, -9.8f, 0);
    public bool right = true;

	public float targetX;

	public override void Start () 
    {
        if (!right)
            v.x = -v.x;
    }

	public override void Update () 
    {

        transform.position += v * Time.deltaTime;
        v += a * Time.deltaTime;

        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        //transform.Translate(Vector3.right);
    }

	public void SetVelocity(float x)
    {
        x = Mathf.Clamp(x, 5, 30);
        v = new Vector3(x, basicV.y, 0);
    }
}
