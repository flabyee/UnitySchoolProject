using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameManager gameManager;

    public Rigidbody2D rigid;
    public BoxCollider2D collider;
    public SpriteRenderer sr;

    public int hp;
    private int maxHp = 3;

    private float dir = 0f;
    public float moveSpeed = 10f;
    public float jumpPower = 10f;
    private float dirX;
    private bool canJump = true;
    private float isJump;
    private bool isBar = false;

    private void Awake()
    {

    }

    void Start()
    {
        hp = maxHp;
        gameManager = FindObjectOfType<GameManager>();

        Color tmp = sr.color;
        tmp.a = 127f;
        sr.color = tmp;
    }

    void Update()
    {
        //rigid.AddForce(new Vector2(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0));
        isJump = transform.position.y > -2.46f ? 0 : 1;
        rigid.velocity = new Vector2(dir * moveSpeed * Time.deltaTime * isJump, rigid.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("FLOOR"))
        {
            canJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        switch(collision.tag)
        {
            case "OBSTACLE":
                if (hp > 0)
                {
                    StartCoroutine(Hit());
                    Debug.Log('a');
                    hp--;
                    gameManager.SetHpUI(hp);
                    // To Do : ÀÏ½Ã ¹«Àû(±ôºý±ôºý)
                    // To Do : UI°»½Å
                }
                else
                {
                    gameManager.EndGame();
                }
                break;
        }
    }

    private IEnumerator Hit()
    {
        collider.enabled = false;

        yield return new WaitForSeconds(2f);

        collider.enabled = true;
    }

    public void OnLeftDown() {  dir = -1f; }
    public void OnLeftUp() { dir = 0f; }
    public void OnRightDown() {  dir = 1f; }
    public void OnRightUp() { dir = 0f; }
    public void OnJump()
    {
        if(canJump)
        {
            canJump = false;
            rigid.velocity = new Vector2(0, jumpPower);
        }
    }
}
