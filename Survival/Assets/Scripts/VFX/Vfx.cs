using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools;
public class Vfx : MonoBehaviour
{
    [SerializeField] string poolName;

    public void OnStop(){
        PoolsManager.Instance.GetPool(poolName).AddToPool(this.gameObject);
    }
}
