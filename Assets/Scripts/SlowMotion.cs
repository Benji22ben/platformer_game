using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlowMotion : MonoBehaviour
{
    [SerializeField] private float slowMotionTimescale;

    private float startTimescale;
    private float startFixedDeltaTime;
    [SerializeField] [Range(0, 1000)] private float slowMotionPowerBaseValue = 100f;
    private float slowMotionPower;

    [SerializeField] [Range(0, 500)] private float slowMotionPowerUseRatePerSecond = 10f;
    [SerializeField] [Range(0, 500)] private float regenerationRatePerSecond = 10f;

    private bool isCtrlPressed = false;

    void Start()
    {
        slowMotionPower = slowMotionPowerBaseValue;

        startTimescale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        if (!Convert.ToBoolean(PlayerPrefs.GetInt("isGameStarted")))
        {
            return;
        }
        isCtrlPressed = Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.LeftControl);
    }

    void FixedUpdate()
    {
        if(slowMotionPower <= 0 && isCtrlPressed)
        {
            StopSlowMotion();
            return;
        }
        if(isCtrlPressed)
        {
            StartSlowMotion();
            ConsumeSlowMotionPower();
        } else 
        {
            StopSlowMotion();
            RegeneratePower();
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
        slowMotionPower -= slowMotionPowerUseRatePerSecond * startFixedDeltaTime;
        slowMotionPower = Mathf.Max(slowMotionPower, 0);
    }

    private void RegeneratePower()
    {
        slowMotionPower += regenerationRatePerSecond * startFixedDeltaTime;
        slowMotionPower = Mathf.Min(slowMotionPower, 100);
    }

    private void ResetPower()
    {
        slowMotionPower = slowMotionPowerBaseValue;
    }

    public void StopSlowMotionAndResetPower()
    {
        isCtrlPressed = false;
        StopSlowMotion();
        ResetPower();
    }
}
