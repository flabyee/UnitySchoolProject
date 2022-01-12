using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CONArcher : CONCharacter
{
    public void ShootArrow(GameObject targetObj)
    {
        Debug.Log("shoot");
        GameSceneClass.gMGPool.CreateObj(ePrefabs.Arrow, transform.position).GetComponent<ConArrow>().SetVelocity(targetObj.transform.position.x);
    }
}
