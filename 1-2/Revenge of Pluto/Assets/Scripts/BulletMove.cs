using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.UI;

public class BulletMove : MonoBehaviour
{
    private float x=1;
    private float y;
    private float addX;
    private float addY;
    public Buttons buttons;
    public GameObject parentPosition;
    private void OnEnable()
    {
        addX = x;
        addY = y;
        this.transform.localPosition = Vector3.zero;
        StartCoroutine(DelBullet());
        this.transform.parent = null;
    }
    private void Update()
    {
        this.transform.position += new Vector3(addX*0.25f, addY*0.25f , 0);
    }
    public void Rightattack()
    {
        x = 1;
        y = 0;
    }
    public void Leftattack()
    {
        x = -1;
        y = 0;
    }
    public void Upattack()
    {
        x = 0;
        y = 1;
    }
    public void Downattack()
    {
        x = 0;
        y = -1;
    }
    IEnumerator DelBullet()
    {
        yield return new WaitForSeconds(50f * Time.deltaTime);
        this.transform.SetParent(parentPosition.transform);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D floor)
    {
        if(floor.tag == "Enemy")
        {
            this.transform.SetParent(parentPosition.transform);
            this.gameObject.SetActive(false);
        }
        
    }
}
