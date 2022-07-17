using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BigasTools;
public class Tiphud : MonoBehaviour
{
    [SerializeField] Text tipText;
    [TextArea][SerializeField] string text;
    private void Start() {
        Initialize(text);
        StateController.Instance.ChangeState(States.GAME_IDLE);
    }
    public void Initialize(string name){
        tipText.text = name;
    }
    private void Update() {
        OnKeyPress();
    }
    public virtual void OnKeyPress(){

    }
    public virtual void OnEnd(){
        StateController.Instance.ChangeState(States.GAME_UPDATE);
        Destroy(this.gameObject);
    }
}
