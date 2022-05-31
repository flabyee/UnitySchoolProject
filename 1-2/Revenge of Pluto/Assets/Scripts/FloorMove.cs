using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FloorMove : MonoBehaviour
{
    int x = 1;
    [SerializeField]
    public float speed=0.5f;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(x * speed * Time.deltaTime, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D floor)
    {
        x = x * -1;
    }
}
