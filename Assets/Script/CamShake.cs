using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachinevirtualCamera;
    
    private float shakeTimer;
    private float shakeTimerTotal;
    private float shakeIntensity;
    public static CamShake Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        cinemachinevirtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
        

    }

    void Update()
    {

        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer < 0)
            {
                CinemachineBasicMultiChannelPerlin cinBasicMulCHperlin = cinemachinevirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinBasicMulCHperlin.m_AmplitudeGain = Mathf.Lerp(shakeIntensity, 0f, (1-(shakeTimer / shakeTimerTotal)));
            }
        }
    }

    public void ShakeCamer(float intensity,float time) {

        CinemachineBasicMultiChannelPerlin cinBasicMulCHperlin = cinemachinevirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinBasicMulCHperlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
        shakeTimerTotal = time;
        shakeIntensity = intensity;
    }

}
