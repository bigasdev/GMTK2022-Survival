using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools;
using BigasTools.InputSystem;

public class Hero : Entity
{
    public Nanosuit currentNanosuit = Nanosuit.Farmer;
    Nanosuit[] nanosuits = new Nanosuit[5]{
        Nanosuit.Farmer,
        Nanosuit.Air_Manager,
        Nanosuit.Wood_Cutter,
        Nanosuit.Slayer,
        Nanosuit.Fisherman
    };
    int suitIndex = 0;
    protected override void OnSpawn()
    {
        base.OnSpawn();
        cooldown.AddLoop(10, ()=>{
            ChangeSuit();
        }, this);
        CameraManager.Instance.currentEntity = this;
    }
    protected override void OnMove()
    {
        base.OnMove();
        var xy = BGameInput.Instance.GetAxis();

        this.transform.position += (Vector3)xy *moveSpeed* Time.deltaTime;
    }
    void ChangeSuit(){
        var s = nanosuits[suitIndex];
        suitIndex++;
        if(suitIndex >= nanosuits.Length)suitIndex = 0;

        Talk(new BigasTools.UI.TextData($"Changing suit to... {s}"), new BigasTools.UI.TextRenderSettings(Color.white, 32), new BigasTools.UI.TextSpeedSettings(.02f, 2f, 3f));
        squashY = .7f;
        Blink(.25f, 3, Color.green);
    }
}
public enum Nanosuit{
    Farmer,
    Air_Manager,
    Wood_Cutter,
    Slayer,
    Fisherman
}
