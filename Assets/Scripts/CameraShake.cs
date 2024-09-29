using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static Transform cameraTransform;
    private static Vector3 _orignalPosOfCam;
    public float shakeFrequency;
    private static float timer;
    private static float dur;
    public static bool shaking;
    public static CameraShake sc;
    private void Start()
    {
        sc = this;
        cameraTransform = Camera.main.transform;
    }
    public void shake(float duration)
    {
        if (shaking)
        {
            dur += duration / 2;
        }
        else
        {
            dur = duration;
            setpos();
            shaking = true;

        }
    }
    private void Update()
    {
        if (shaking)
        {
            timer += Time.deltaTime;
            ShakeM();
            if (timer >= dur)
            {
                StopShake();
                timer = 0;
                
            }
        }


    }
    private void setpos()
    {
        cameraTransform = Camera.main.transform;
        _orignalPosOfCam = this.transform.position;
    }
    private void ShakeM()
    {
        Vector3 pos = _orignalPosOfCam + Random.insideUnitSphere * shakeFrequency;
        cameraTransform.position = new Vector3(pos.x,pos.y, -10);
        setpos();
    }

    private void StopShake()
    {
    
        cameraTransform.position = _orignalPosOfCam;
        shaking = false;
    }
}

