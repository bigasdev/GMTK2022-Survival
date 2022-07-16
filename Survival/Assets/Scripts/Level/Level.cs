using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using BigasMath;
using BigasTools;
using System.Globalization;
public class Level : MonoBehaviour
{
    private static Level instance;
    public static Level Instance{
        get{
            if(instance == null)instance = FindObjectOfType<Level>();
            return instance;
        }
    }
    public List<UIData> uIDatas = new List<UIData>();
    public List<LevelData> datas = new List<LevelData>();
    [SerializeField] int npcsAmount = 5;
    private void Start() {
        for (int i = 0; i < npcsAmount; i++)
        {
            PoolsManager.Instance.GetPool("Npc").GetFromPool(new Vector3(Random.Range(CameraManager.Instance.limit.x + 2, CameraManager.Instance.limit.y - 2), Random.Range(CameraManager.Instance.limit.z + 2, CameraManager.Instance.limit.w - 2)));
        }
    }
    public void AddData(LevelData levelData){
        datas.Add(levelData);
    }
    public void Infect(Enviroments enviroment){
        var d = datas.Where(x=>x.enviroments==enviroment).FirstOrDefault();
        d.infectedChilds++;
    }
    public void Desinfect(Enviroments enviroment){
        var d = datas.Where(x=>x.enviroments==enviroment).FirstOrDefault();
        d.infectedChilds--;
    }
    private void Update() {
        foreach(var u in uIDatas){
            var d = datas.Where(x=>x.enviroments==u.enviroments).FirstOrDefault();
            if(d==null)return;
            u.quantityText.text = d.GetPercentage().ToString("00.0");
            u.fillBar.fillAmount = d.GetPercentage()*.01f;
        }
    }
}
[System.Serializable]
public class LevelData{
    public Enviroments enviroments;
    public EnviromentEntity motherEntity;
    public int infectedChilds;
    public int originalChilds = 1000;
    public LevelData(Enviroments enviroments, EnviromentEntity motherEntity)
    {
        this.enviroments = enviroments;
        this.motherEntity = motherEntity;
        originalChilds = this.motherEntity.childs.Count;
    }
    public float GetPercentage(){
        return BMathPercentage.GetPercentageFromFloat(infectedChilds, originalChilds);
    }
}
[System.Serializable]
public class UIData{
    public Enviroments enviroments;
    public Text quantityText;
    public Image fillBar;

    public UIData(Enviroments enviroments, Text quantityText, Image fillBar)
    {
        this.enviroments = enviroments;
        this.quantityText = quantityText;
        this.fillBar = fillBar;
    }
}
