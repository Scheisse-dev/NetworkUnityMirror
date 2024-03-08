using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] List<CameraManaged> cameras = new();


    public bool Register(CameraManaged _cam)
    {
        if (cameras.Contains(_cam))
            return false;
        cameras.Add(_cam);
        return true;
    }
    public bool UnRegister(CameraManaged _cam)
    {
        if (!cameras.Contains(_cam))
            return false;
        cameras.Remove(_cam);
        return true;
    }

    public void ActiveAll()
    {
        foreach(CameraManaged _cam in cameras)
        {
            if (_cam)
                _cam.Active();
        }
    }
    public void DisableAll()
    {
        foreach(CameraManaged _cam in cameras)
        {
            if (_cam)
                _cam.Disable();
        }
    }
}
