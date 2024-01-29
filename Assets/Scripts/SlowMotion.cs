using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    [SerializeField] private float slowMotionTimescale;

    private float startTimescale;
    private float startFixedDeltaTime;
    private float slowMotionPower = 100f;

    private float slowMotionTimer; 
    
    private bool isCtrlPressed = false;

    void Start()
    {
        startTimescale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        isCtrlPressed = Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.LeftControl);

        if(slowMotionPower >= 1)
        {
            if(isCtrlPressed)
            {
                StartSlowMotion();
                slowMotionTimer += Time.deltaTime;
            } else 
            {
                StopSlowMotion();
            }
        } 
        else {
            StopSlowMotion();
        }

        Debug.Log(slowMotionPower);
    }

    private void StartSlowMotion()
    {
        Time.timeScale = slowMotionTimescale;
        Time.fixedDeltaTime = startFixedDeltaTime * slowMotionTimescale;
    }

    private void StopSlowMotion()
    {
        Time.timeScale = startTimescale;
        Time.fixedDeltaTime = startFixedDeltaTime;
    }

    private void ConsumeSlowMotionPower()
    {
        if (isCtrlPressed)
        {
            slowMotionPower -= slowMotionPowerUseRate;
            slowMotionPower = Mathf.Max(slowMotionPower, 0);
        }
    }

    private void RegeneratePower()
    {
        if (!isCtrlPressed)
        {
        slowMotionPower += regenerationRate;
        
        slowMotionPower = Mathf.Min(slowMotionPower, 100);
        }
    }
}
