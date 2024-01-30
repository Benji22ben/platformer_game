using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 0, -10);
    private GameStart gameStart;
    
    void Start()
    {
        gameStart = GetComponent<GameStart>();
    }

    void Update()
    {
        if (gameStart.isGameStarted)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer() {
        transform.position = new Vector3(transform.position.x, playerTransform.position.y + cameraOffset.y, transform.position.z);
    }
}
