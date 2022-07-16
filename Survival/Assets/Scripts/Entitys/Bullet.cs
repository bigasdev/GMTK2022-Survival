using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools;

public class Bullet : Entity
{
    public Cocoon assignedCocoon;
    protected override void OnMove()
    {
        base.OnMove();
        if(assignedCocoon!=null){
            var d = Vector2.Distance(currentPosition, assignedCocoon.currentPosition);
            if(d<=.01f){
                assignedCocoon.Hit(1, this);
                PoolsManager.Instance.GetPool("Bullet").AddToPool(this.gameObject);
            }
        }
    }
}
