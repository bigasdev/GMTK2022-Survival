using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools.InputSystem;
public class WASDHud : Tiphud
{
    public override void OnKeyPress()
    {
        base.OnKeyPress();
        if(BGameInput.Instance.GetAxis().x != 0 || BGameInput.Instance.GetAxis().y != 0){
            OnEnd();
        }
    }
}
