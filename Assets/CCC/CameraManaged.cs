using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManaged : MonoBehaviour
{
    [SerializeField] Camera cameraView = null;


    private void Start()
    {
        if(CameraManager.Instance != null)
            CameraManager.Instance.Register(this);
    }

    public void Active()
    {
        cameraView.enabled = true;
    }

    public void Disable()
    {
        cameraView.enabled = false;
    }
}
