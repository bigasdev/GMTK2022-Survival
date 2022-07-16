using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools;
public class EnviromentEntity : Entity
{
    [SerializeField] protected bool isMother;
    [SerializeField] protected bool isInfected = false;
    public Nanosuit requiredSuit;
    public List<EnviromentEntity> childs;
    protected Hero assignedHero;
    protected override void OnSpawn()
    {
        base.OnSpawn();
        assignedHero = FindObjectOfType<Hero>();
        if(isMother){
            assignedHero.environments.Add(new EnvironmentHeroProfile(requiredSuit, this, this.transform));
        }
    }
    protected override void OnMove()
    {
        base.OnMove();
        if(isInfected){
            var d = Vector2.Distance(currentPosition, assignedHero.currentPosition);
            if(d > 3)return;
            if(d > 1.5){
                assignedHero.interactingWith = null;
            }
            if(d < 1){
                OnClose();
                assignedHero.interactingWith = this;

            }
        }
    }
    protected virtual void OnClose(){

    }
}
[System.Serializable]
public class EnvironmentHeroProfile{
    public Nanosuit requiredSuit;
    public EnviromentEntity entity;
    public Transform pos;

    public EnvironmentHeroProfile(Nanosuit requiredSuit, EnviromentEntity entity, Transform pos)
    {
        this.requiredSuit = requiredSuit;
        this.entity = entity;
        this.pos = pos;
    }
}
