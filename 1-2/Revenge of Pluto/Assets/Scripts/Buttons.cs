using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private int headRotation;
    [SerializeField]
    private GameObject head;
    [SerializeField]
    private GameObject headPosition;
    private bool attackTure = false;
    private int rotatonZ;
    private Rigidbody2D myRigid;
    bool jumpTrue=false;
    bool joystickPosition = false;
    bool joystickRotation = false;
    private float y = 0;
    private float x=2;
    public float jump=5;
    [SerializeField]
    Material material = null;
    [SerializeField]
    public Vector3 currentPosition = Vector3.zero;

    [SerializeField]
    private float speed = 5;
    // Start is called before the first frame update
    private void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        x = 2;

    }
    void True()
    {
        joystickPosition = true;
    }
    void TurnTrue()
    {
        joystickRotation = true;
    }
    private void Move()
    {
        this.transform.position += new Vector3(speed*x * Time.deltaTime, 0);
    }
    public void JoystickFalse()
    {
        joystickPosition = false;
        joystickRotation = false;
    }
    public void JoystickRightdown() 
    {
        True();
        x = 2f;
        head.transform.rotation = Quaternion.Euler(0, 0, 0);
        headPosition.transform.rotation = Quaternion.Euler(0, 0, 90 * 0);
    }
    public void JoystickLeftdown()
    {
        True();
        x = -2f;
        head.transform.rotation = Quaternion.Euler(0, 180, 0);
        headPosition.transform.rotation = Quaternion.Euler(0, 0, 90 * 0);
    }
    public void Joystickupdown()
    {
        y = 1;
        TurnTrue();
        rotatonZ = 1;
        headRotation = 45;
    }
    public void JoystickDowndown()
    {
        y = -1;
        TurnTrue();
        rotatonZ = -1;
        headRotation = -140;
    }
    public void Jump()
    {
        if (jumpTrue)
        {
            myRigid.velocity = new Vector2(0, jump);
        }
    }
    private void OnTriggerEnter2D(Collider2D floor)
    {
        if (floor.tag == "Floor")
        {
            jumpTrue = true;
        }
        if(floor.tag == "Enemy")
        {
            Debug.Log("죽음");
        }
    }
    private void OnTriggerExit2D(Collider2D floor)
    {
        if (floor.tag == "Floor")
        {
            jumpTrue = false;
        }
    }
    private void Update()
    {
        if (Time.timeScale == 1)
        {
            if (joystickPosition)
            {
                material.mainTextureOffset += new Vector2(-x * 0.0005f, 0);
                Move();
            }
            if (joystickRotation)
            {
                head.transform.rotation = Quaternion.Euler(0, 0, headRotation);
                headPosition.transform.rotation = Quaternion.Euler(0, 0, 90 * rotatonZ);
            }
            currentPosition = transform.position;
        }
    }
    public void attackDown()
    {
        attackTure = true;
        StartCoroutine(attack());
    }
    public void attackUp()
    {
        attackTure = false;
    }
    IEnumerator attack()
    {
        while (attackTure)
        {
            yield return new WaitForSeconds(0.5f);
            if (!attackTure)
            {
                yield break;
            }
            headPosition.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
    // Update is called once per frame
   
}
