using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 0, -10);
    
    private void Update()
    {
        transform.position = new Vector3(playerTransform.position.x + cameraOffset.x, playerTransform.position.y + cameraOffset.y, transform.position.z);
    }
}
