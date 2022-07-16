using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools;

public class Drone : Entity
{
    [SerializeField] float followingSpeed = 7f;
    Hero assignedHero;
    protected override void OnSpawn()
    {
        base.OnSpawn();
        cooldown.AddLoop(1.5f, Wander, this);
        assignedHero = TagQuery.FindObject("Hero").GetComponent<Hero>();
    }
    protected override void OnMove()
    {
        base.OnMove();
        var d = Vector2.Distance(currentPosition, assignedHero.dronePos.position);
        if(d > .35f){
            currentPosition = Vector2.MoveTowards(currentPosition, assignedHero.dronePos.position, followingSpeed*Time.deltaTime);
        }
    }
    void Wander(){
        if(assignedHero.moving)return;
        var p = new Vector2(Random.Range(currentPosition.x + 1.5f, currentPosition.x - 1.5f), Random.Range(currentPosition.y + .5f, currentPosition.y -.2f));
        MoveTo(p);
    }
}
