using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField]
    private Material mt = null;
    // Update is called once per frame
    void Update()
    {
        mt.mainTextureOffset += new Vector2(-1 * 0.0005f, 0);
    }
}
