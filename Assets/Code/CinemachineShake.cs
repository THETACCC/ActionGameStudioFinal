using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{

    public static CinemachineShake Instance { get; private set; }
    private CinemachineVirtualCamera cinemachinevirtualcamera;
    private float shakeTimer;

    private void Awake()
    {

        Instance = this;
        cinemachinevirtualcamera = GetComponent<CinemachineVirtualCamera>();
    }


    public void ShakeCamera(float intensity, float time)
    {

        CinemachineBasicMultiChannelPerlin perlin = cinemachinevirtualcamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        perlin.m_AmplitudeGain = intensity;
        shakeTimer = time;

    }


    private void Update()
    {
          if(shakeTimer> 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer < 0f ) 
            {
                //Time up
                CinemachineBasicMultiChannelPerlin perlin = cinemachinevirtualcamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();


                perlin.m_AmplitudeGain = 0f;

            }
        }
    }


}
