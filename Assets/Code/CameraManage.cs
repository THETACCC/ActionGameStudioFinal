using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManage : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public GameObject Target;
    // Function to switch the follow target of the virtual camera
    public void SwitchFollowTarget()
    {
        if (virtualCamera != null)
        {
            virtualCamera.Follow = Target.transform;
        }
        else
        {
            Debug.LogError("Virtual Camera is not assigned!");
        }
    }
}
