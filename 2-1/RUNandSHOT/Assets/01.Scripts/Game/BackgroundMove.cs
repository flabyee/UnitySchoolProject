using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private MeshRenderer mesh;
    Vector2 offset = Vector2.zero;

    public float speed = 0.1f;
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if(!DataManager.instance.isShoting)
        {
            offset.x -= (speed + DataManager.instance.playerSpeedLevel * 0.02f) * Time.deltaTime;
            mesh.material.SetTextureOffset("_MainTex", offset);
        }
    }
}
