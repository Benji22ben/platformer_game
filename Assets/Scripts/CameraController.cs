using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 0, -10);
    
    void Update()
    {
        if (Convert.ToBoolean(PlayerPrefs.GetInt("isGameStarted")))
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer() {
        transform.position = new Vector3(transform.position.x, playerTransform.position.y + cameraOffset.y, transform.position.z);
    }
}
