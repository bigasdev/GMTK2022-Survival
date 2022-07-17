using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools;

public class Npc : Entity
{
    protected override void OnSpawn()
    {
        base.OnSpawn();
        cooldown.AddLoop(Random.Range(2.65f, 4.25f), OnHelp, this);
        cooldown.AddLoop(Random.Range(12, 17), OnScream, this);
    }
    void OnHelp(){
        var p = new Vector2(Random.Range(currentPosition.x + 2, currentPosition.x - 2), Random.Range(currentPosition.y + 5, currentPosition.y -5));
        MoveTo(p);
    }
    void OnScream(){
        squashX = .525f;
    }
}
