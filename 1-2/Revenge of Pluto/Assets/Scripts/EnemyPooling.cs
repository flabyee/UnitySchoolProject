using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{
    [SerializeField]
    private float wait;
    [SerializeField]
    private int childCount=0;
    private bool stop=false;
    private int childNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyPool());
    }
    private void Update()
    {
        if (stop)
        {
            StopCoroutine(EnemyPool());
        }
    }

    // Update is called once per frame
    IEnumerator EnemyPool()
    {
        while(!stop)
        {
            this.transform.GetChild(childNum).gameObject.SetActive(true);
            childNum++;
            if (childNum > childCount - 1)
            {
                stop = true;
            }
            yield return new WaitForSeconds(wait);
        }
    }
}
