using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    // Item에는 모두 Use를 구현해서 Mover와 충돌시 어떤 아이템이던간에 Use를 실행함
    public abstract void Use(GameObject target);
}
