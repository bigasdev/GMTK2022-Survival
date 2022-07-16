using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;
    public static CameraManager Instance{
        get{
            if(instance == null)instance = FindObjectOfType<CameraManager>();
            return instance;
        }
    }
    [SerializeField] float cameraSpeed = 3f;
    public Vector4 limit;
    public Entity currentEntity;
    Vector2 velocity;
    private void LateUpdate() {
        
    }
    private void Update() {
        if(currentEntity == null)return;
        this.transform.position = Vector2.SmoothDamp(this.transform.position, currentEntity.currentPosition, ref velocity, cameraSpeed, 15f );
        this.transform.position = new Vector2(Mathf.Clamp(this.transform.position.x,limit.x, limit.y), Mathf.Clamp(this.transform.position.y,limit.z, limit.w));
    }
}
