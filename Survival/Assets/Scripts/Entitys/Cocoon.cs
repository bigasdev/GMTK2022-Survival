using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools;
public class Cocoon : EnviromentEntity
{
    protected override void OnSpawn()
    {
        base.OnSpawn();
        cooldown.AddLoop(.25f, ()=>{
            if(assignedHero.currentNanosuit!=Nanosuit.Slayer)return;
            var d = Vector2.Distance(assignedHero.currentPosition, currentPosition);
            if(d>=5)return;
            assignedHero.assignedDrone.Shoot(currentPosition, this);
        }, this);
        Initialize();
    }
    protected override void OnClose()
    {
        if(assignedHero.currentNanosuit!=Nanosuit.Slayer)return;
        base.OnClose();
    }
    public override void Hit(float damage, Entity attacker = null)
    {
        squashY = .925f;
        Blink(.05f, 1, Color.red);
        life -= damage;
    }
    public override void Kill()
    {
        base.Kill();
        PoolsManager.Instance.GetPool("HitAnim").GetFromPool((Vector3)currentPosition);
    }
}
