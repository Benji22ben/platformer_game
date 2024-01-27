using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    [SerializeField] private float slowMotionTimescale;

    private float startTimescale;
    private float startFixedDeltaTime;
    private float slowMotionPower = 100f;
    private float regenerationInterval = 1f;
    private float regenerationRate = 10f;

    private float slowMotionPowerUseRate = 10f;
    private float slowMotionPowerUseInterval = 0.1f;
    
    private bool isCtrlPressed = false;

    void Start()
    {
        startTimescale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;

        StartCoroutine(ConsumeSlowMotionPower());
        StartCoroutine(RegeneratePower());
    }

    void Update()
    {
        isCtrlPressed = Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.LeftControl);

        if(slowMotionPower >= 1)
        {
            if(isCtrlPressed)
            {
                StartSlowMotion();
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

    private IEnumerator ConsumeSlowMotionPower()
    {
        while (true)
        {
            if (isCtrlPressed)
            {
                slowMotionPower -= slowMotionPowerUseRate;
                slowMotionPower = Mathf.Max(slowMotionPower, 0);

                yield return new WaitForSeconds(slowMotionPowerUseInterval);
            }
            else
            {
                yield return null;
            }
        }
    }

    private IEnumerator RegeneratePower()
    {
        while (true)
        {
            if (!isCtrlPressed)
            {
            slowMotionPower += regenerationRate;
            
            slowMotionPower = Mathf.Min(slowMotionPower, 100);

            yield return new WaitForSeconds(regenerationInterval);
            }
        }
    }
}
