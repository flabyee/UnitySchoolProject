using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    GameManager gameManager = null;
    Animator animator = null;
    SpriteRenderer sr = null;
    
    [SerializeField]
    private float jumpSpeed = 10f;

    private float shotCooltime = 0.6f;
    private float lastShotTime = 0f;

    public GameObject bulletPrefab;

    Vector2 startPos = Vector2.zero;
    Vector2 endPos = Vector2.zero;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();


        //shotCooltime = 1f - ((DataManager.instance.playerShotSpeedLevel - 1) * 0.05f);

    }

    void Update()
    {
        if(Time.time > lastShotTime)
        {
            DataManager.instance.isShoting = false;
        }
        else
        {
            DataManager.instance.isShoting = true;
        }

        if(DataManager.instance.isKnife)
        {
            sr.flipX = false;
            animator.Play("Knife");
            StartCoroutine(KnifeAnimation());
        }
        else if(Time.time > lastShotTime)
        {
            sr.flipX = true;
            animator.Play("Move");
        }
        else
        {
            sr.flipX = false;
            animator.Play("Attack");
        }

        if(Input.GetMouseButtonDown(0))
        {
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Mathf.Abs(startPos.y - endPos.y) < 1f && rigid.velocity.y == 0)   //드래그 범위가 좁으면
            {
                Attack();
            }
            else
            {
                if (startPos.y - endPos.y < 0)   //아래에서 위로 드래그
                {
                    Jump();
                }
                else if (startPos.y - endPos.y > 0) //위에서 아래로 드래그
                {
                    Slide();
                }
            }
        }
    }

    public void Jump()
    {
        if(rigid.velocity.y == 0)
        {
            rigid.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);    //time.deltatime 필요없음
            
        }
       
    }
    public void Slide()
    {
        if(rigid.velocity.y != 0)
        {
            rigid.AddForce(Vector2.down * jumpSpeed * 3f, ForceMode2D.Impulse);
        }
    }
    public void Attack()
    {
        if(Time.time - 0.1f > lastShotTime)
        {
            lastShotTime = Time.time + shotCooltime - DataManager.instance.playerShotSpeedLevel * 0.01f;
            GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 180)));
            Destroy(bullet, 3f);
        }
    }

    //IEnumerator Slide()
    //{
    //    isSlide = true;

    //    transform.rotation = Quaternion.Euler(0, 0, -90);
    //    yield return new WaitForSeconds(1f);
    //    transform.rotation = Quaternion.Euler(0, 0, 0);

    //    isSlide = false;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("OBSTACLE") || collision.CompareTag("MONSTER") || collision.CompareTag("ENEMYBULLET"))
        {
            DataManager.instance.isKnife = false;
            DataManager.instance.isDie = true;
            gameManager.EndPopupOpen();

        }
        else if(collision.CompareTag("EXITDOOR"))
        {
            DataManager.instance.isExit = true;
            gameManager.EndPopupOpen();
        }

    }

    IEnumerator KnifeAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        DataManager.instance.isKnife = false;
    }
}
