using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMove : MonoBehaviour
{
    public float speed;
    public float defaultSpeed;    //speed에서 빼는 값
    public float addSpeed;      //speed 가증치


    public float fireCoolTime;
    public float lastFireTime;


    public GameObject bulletPrefab;



    public Canvas canvas;

    public Slider hpBarPrefab;
    private Slider hpBar;

    public Slider attackBarPrefab;
    private Slider attackBar;

    public float hp = 3000;
    private float maxHp = 3000;

    GameManager gameManager;
    Animator animator;

    private bool shotting = false;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();

        shotting = false;

        if(DataManager.instance.isEasy)
        {
            hp = 3000;
            maxHp = 3000;
            addSpeed = 0.01f;
            fireCoolTime = 6f;
            DataManager.instance.obstacleSpeed = 7f;
        }
        else if(DataManager.instance.isNormal)
        {
            hp = 7500;
            maxHp = 7500;
            addSpeed = 0.012f;
            fireCoolTime = 5f;
            DataManager.instance.obstacleSpeed = 8f;
        }
        else if(DataManager.instance.isHard)
        {
            hp = 10000;
            maxHp = 10000;
            addSpeed = 0.014f;
            fireCoolTime = 4f;
            DataManager.instance.obstacleSpeed = 9f;
        }


        hpBar = Instantiate(hpBarPrefab, canvas.transform);
        attackBar = Instantiate(attackBarPrefab, canvas.transform);


        lastFireTime = fireCoolTime + Time.time;
    }

    void Update()
    {
        speed = (speed * (1 + addSpeed * Time.deltaTime));  // - (DataManager.instance.playerSpeedLevel * 0.0001f)

        if (!DataManager.instance.isShoting)
        {
            transform.Translate(Vector2.left * (speed - defaultSpeed) * Time.deltaTime);
        }
        else if(DataManager.instance.isShoting)
        {
            transform.Translate(Vector2.left * (speed * Time.deltaTime));
        }

        if (hpBar != null)
        {
            hpBar.transform.position = (transform.position + new Vector3(0.15f, 3.5f, 0));
        }
        if(attackBar != null)
        {
            attackBar.transform.position = transform.position + new Vector3(0.15f, 4f, 0);
            attackBar.value = 1 - (1 * ((lastFireTime - Time.time) / fireCoolTime));
        }

        if(shotting)
        {
            animator.Play("Shot");
        }
        else
        {
            animator.Play("Move");
        }

        if(lastFireTime < Time.time)
        {
            lastFireTime = Time.time + 1000000f;
            StartCoroutine(Fire());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("BULLET"))
        {
            
            DataManager.instance.AddScore(100 + (10 * (DataManager.instance.playerPowerLevel - 1)));    // 100 + 10 * powerlevel - 1


            if(hp > 100 + (10 * (DataManager.instance.playerPowerLevel - 1)))
            {
                hp -= 100 + (10 * (DataManager.instance.playerPowerLevel - 1));
                hpBar.value = 1f * ((float)hp / (float)maxHp);
            }
            else
            {
                DataManager.instance.isKill = true;
                gameManager.EndPopupOpen();
            }
        }
        else if(collision.CompareTag("KNIFE"))
        {
            DataManager.instance.isKnife = true;

            DataManager.instance.AddScore(50 + (5 * (DataManager.instance.playerPowerLevel - 1)));    // 300 + 10 * powerlevel - 1


            if (hp > 50 + (5 * (DataManager.instance.playerPowerLevel - 1)))
            {
                hp -= 50 + (5 * (DataManager.instance.playerPowerLevel - 1));
                hpBar.value = 1f * ((float)hp / (float)maxHp);
            }
            else
            {
                DataManager.instance.isKill = true;
                if (DataManager.instance.isEasy) DataManager.instance.isEasyClear = true;
                if (DataManager.instance.isNormal) DataManager.instance.isNormalClear = true;
                if (DataManager.instance.isHard) DataManager.instance.isHardClear = true;
                gameManager.EndPopupOpen();
            }
        }
        else if(collision.CompareTag("OBSTACLE"))
        {
            Destroy(collision.gameObject);
            // ToDo : 부셔짐 효과 만들기
        }
        else if(collision.CompareTag("OUTLINE"))
        {
            DataManager.instance.isOut = true;
            gameManager.EndPopupOpen();
        }
    }

    IEnumerator Fire()
    {
        shotting = true;
        yield return new WaitForSeconds(1.1f);
        shotting = false;
        GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(-1f, 1.7f, 0), Quaternion.identity);
        Destroy(bullet, 2f);

        lastFireTime = Time.time + fireCoolTime;

    }
}
