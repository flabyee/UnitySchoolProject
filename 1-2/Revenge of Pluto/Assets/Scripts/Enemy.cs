using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    private Buttons player = null;
    SpriteRenderer sr = null;
    [SerializeField]
    private float speed = 0;
    [SerializeField]
    private short hp = 2;
    private bool isDead = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        player = FindObjectOfType<Buttons>();
        sr = FindObjectOfType<SpriteRenderer>();
        isDead = false;
        hp = 4;
    }



    // Update is called once per frame
    void Update()
    {
        if (15 > Mathf.Abs(player.currentPosition.x - transform.position.x))
        {
            if (0 < (player.currentPosition.x - transform.position.x))
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                sr.flipX = true;
            }
            else
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                sr.flipX = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D floor)
    {
        if (floor.tag == "bullet")
        {
            hp--;

            if (hp <= 0)
            {
                isDead = true;
                Destroy(this.gameObject);
            }
        }
    }
}
