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
    [SerializeField] Animator animator;
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
    public Drone assignedDrone;
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
        HandleArrow();
        var xy = BGameInput.Instance.GetAxis();
        Debug.Log(xy);
        dir = -(int)xy.x;
        if(dir==0)dir=1;
        HandleAnim(xy);
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
        if(e==null){
            pointingArrow.gameObject.SetActive(false);
            return;
        }

        var dir = (Vector2)e.pos.position - currentPosition;
        if(dir.magnitude > 5){
            pointingArrow.gameObject.SetActive(true);
        }else{
            pointingArrow.gameObject.SetActive(false);
        }
        var angle = Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg;
        pointingArrow.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void HandleAnim(Vector2 xy){
        animator.SetInteger("Nanosuit", (int)currentNanosuit);
        animator.SetBool("WalkingUp", xy.y == 1 && xy.x == 0);
        animator.SetBool("WalkingDown", xy.y == -1 && xy.x == 0);
        animator.SetBool("WalkingSide", xy.x != 0);
    }
}
public enum Nanosuit{
    Farmer = 0,
    Air_Manager = 1,
    Wood_Cutter = 2,
    Slayer = 3,
    Fisherman = 4
}
