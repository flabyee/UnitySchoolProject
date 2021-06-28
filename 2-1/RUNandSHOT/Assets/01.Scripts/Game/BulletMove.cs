using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private float speed = 10f;

    bool isMove = true;

    Animator animator = null;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(isMove)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MONSTER"))
        {
            isMove = false;
            transform.localScale = new Vector3(4, 4, 0);
            animator.Play("Hit");
            Destroy(gameObject, 0.35f);
        }
    }
}
