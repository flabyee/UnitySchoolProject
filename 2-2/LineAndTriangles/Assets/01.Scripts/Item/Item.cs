using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    // Item���� ��� Use�� �����ؼ� Mover�� �浹�� � �������̴����� Use�� ������
    public abstract void Use(GameObject target);
}
