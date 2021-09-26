using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

//Changes the prior Camera on call.
public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera upCam, behindCam;

    public void UpCam()
    {
        upCam.Priority = 1;
        behindCam.Priority = 0;
    }

    public void BehindCam()
    {
        upCam.Priority = 0;
        behindCam.Priority = 1;
    }
}
