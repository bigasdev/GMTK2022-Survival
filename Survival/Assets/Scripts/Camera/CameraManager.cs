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
    public Entity currentEntity;
    Vector2 velocity;
    private void LateUpdate() {
        if(currentEntity == null)return;
        this.transform.position = Vector2.SmoothDamp(this.transform.position, currentEntity.currentPosition, ref velocity, cameraSpeed, 15f );
    }
}
