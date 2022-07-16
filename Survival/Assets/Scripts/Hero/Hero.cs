using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools;
using BigasTools.InputSystem;
using System.Linq;

public class Hero : Entity
{
    public Transform dronePos;
    public Nanosuit currentNanosuit = Nanosuit.Farmer;
    [SerializeField] Transform pointingArrow;
    public List<EnvironmentHeroProfile> environments = new List<EnvironmentHeroProfile>();
    Nanosuit[] nanosuits = new Nanosuit[5]{
        Nanosuit.Farmer,
        Nanosuit.Air_Manager,
        Nanosuit.Wood_Cutter,
        Nanosuit.Slayer,
        Nanosuit.Fisherman
    };
    int suitIndex = 1;
    public bool moving = false;
    public EnviromentEntity interactingWith = null;
    Drone assignedDrone;
    protected override void OnSpawn()
    {
        base.OnSpawn();
        cooldown.AddLoop(10, ()=>{
            ChangeSuit();
        }, this);
        CameraManager.Instance.currentEntity = this;
        assignedDrone = TagQuery.FindObject("Drone").GetComponent<Drone>();
    }
    protected override void OnMove()
    {
        base.OnMove();
        if(BGameInput.Instance.GetKeyPress("Interaction")){
            OnInteract();
        }
        var xy = BGameInput.Instance.GetAxis();
        moving = xy != Vector2.zero;

        this.transform.position += (Vector3)xy *moveSpeed* Time.deltaTime;
    }
    void ChangeSuit(){
        var s = nanosuits[suitIndex];
        currentNanosuit = s;
        suitIndex++;
        if(suitIndex >= nanosuits.Length)suitIndex = 0;

        assignedDrone.Talk(new BigasTools.UI.TextData($"Changing suit to... {s}"), new BigasTools.UI.TextRenderSettings(Color.white, 32), new BigasTools.UI.TextSpeedSettings(.02f, 2f, 3f));
        assignedDrone.squashY = .7f;
        Blink(.25f, 3, Color.green);
    }
    void OnInteract(){
        if(interactingWith == null){
            assignedDrone.Blink(.15f, 3, Color.red);
            assignedDrone.squashX = .725f;
            return;
        }
        if(currentNanosuit != interactingWith.requiredSuit){
            Talk(new BigasTools.UI.TextData($"I don't have the right nanosuit to deal with this..."), new BigasTools.UI.TextRenderSettings(Color.white, 32), new BigasTools.UI.TextSpeedSettings(.02f, 2f, 3f));
            return;
        }
    }
    void HandleArrow(){
        var e = environments.Where(x => x.requiredSuit == currentNanosuit).FirstOrDefault();
    }
}
public enum Nanosuit{
    Farmer,
    Air_Manager,
    Wood_Cutter,
    Slayer,
    Fisherman
}
