using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LDtkUnity;
using UnityEngine.UI;

public class Grass : EnviromentEntity
{
    [SerializeField] Sprite normalGrass, infectedGrass;
    [SerializeField] float rakingModifier = .125f;
    [SerializeField] GameObject UI;
    [SerializeField] Image progressAmt;
    public float rakingAmt = 0;
    bool wasRaked = false;
    protected override void OnSpawn()
    {
        var f = GetComponent<LDtkFields>();
        isMother = f.GetBool("isMother");
        base.OnSpawn();
        if(isMother){
            spriteRenderer.sprite = infectedGrass;
            var c = FindObjectsOfType<Grass>();
            childs = new List<EnviromentEntity>(c);
            cooldown.AddLoop(5f, ()=>{
                var rnd = Random.Range(0,childs.Count);
                var r = childs[rnd] as Grass;
                squashX = .52f;
                Blink(.25f, 5, Color.red);
                r.Infect();
                childs.Remove(r);
            });
            Initialize();
        }
    }
    protected override void OnMove()
    {
        base.OnMove();
        if(isInfected){
            if(assignedHero.currentNanosuit != Nanosuit.Farmer)return;
            if(wasRaked && rakingAmt >=1){
                Desinfect();
                return;
            }
            rakingAmt -= rakingModifier/4 * Time.deltaTime;
            if(rakingAmt <= 0f){
                UI.SetActive(false);
            }else{
                UI.SetActive(true);
            }
            progressAmt.fillAmount = rakingAmt;
            rakingAmt = Mathf.Clamp(rakingAmt, 0,1);
        }
    }
    protected override void OnClose()
    {
        if(assignedHero.currentNanosuit != Nanosuit.Farmer)return;
        base.OnClose();
        wasRaked = true;
        rakingAmt += rakingModifier * Time.deltaTime;
    }
    protected override void Infect(){
        base.Infect();
        spriteRenderer.sprite = infectedGrass;
        isInfected = true;
    }
    protected override void Desinfect(){
        base.Desinfect();
        spriteRenderer.sprite = normalGrass;
        Blink(.25f, 4, Color.green);
        isInfected = false;
        UI.SetActive(false);
        return;
    }
}
