using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public delegate int Add(int a, int b);

    void Start()
    {
        Add a = MyAdd;
    }

    public int MyAdd(int a, int b)
    {
        return a + b;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
