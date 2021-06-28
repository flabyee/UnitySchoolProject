using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnColor : MonoBehaviour
{
    private Image image;


    private float x;
    private float addValue = 0.1f;
    // Start is called before the first frame update
    void Start()
    {

        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.color = new Color(x, 1, 1, 1);
        x += addValue;
        if (x > 254)
        {
            addValue = -addValue;
        }
        if(x < 1)
        {
            addValue = -addValue;
        }
    }
}
