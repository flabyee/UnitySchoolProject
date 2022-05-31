using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Sprite[] sprites;

    public SpriteRenderer sr;
    public Collider2D collider;

    public float changeDelay = 1f;
    public float onTime = 0.2f;

    protected virtual void OnEnable()
    {
        StartCoroutine(Change());
    }

    protected virtual IEnumerator Change()
    {
        yield return new WaitForSeconds(changeDelay);

        OnCol();

        yield return new WaitForSeconds(onTime);

        OffCol();

        gameObject.SetActive(false);
    }

    protected virtual void OnCol()
    {
        collider.enabled = true;
    }

    protected virtual void OffCol()
    {
        sr.sprite = sprites[0];
        collider.enabled = false;
    }
}
