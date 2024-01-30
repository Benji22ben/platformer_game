using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    // This script make the camera move down without the player at the start of the game
    [SerializeField] private float minY = -83f; // Minimum Y position
    [SerializeField] private float maxY = -6f;  // Maximum Y position
    [SerializeField] private float cameraMoveSpeed = 6f; // Speed at which the camera moves
    private float differenceBetweenXAndY;
    
    [SerializeField] private bool goDown = true; // True = Down, False = Up
    
    void Start() {
        differenceBetweenXAndY = maxY - minY; // Difference between the X and Y position of the camera
        differenceBetweenXAndY = differenceBetweenXAndY < 0 ? -differenceBetweenXAndY : differenceBetweenXAndY; // Make sure the difference is positive
        StartCoroutine(MoveCamera());
    }

    private IEnumerator MoveCamera()
    {
        float i = 0f;

        while (i < differenceBetweenXAndY)
        {
            float deltaMovement = cameraMoveSpeed * Time.deltaTime;
            i += deltaMovement;

            transform.position = new Vector3(transform.position.x, goDown ? -i : i, transform.position.z);

            // Add a small delay to control the speed of the movement
            yield return null;
        }

        yield return new WaitForSeconds(3f);
        GlobalVariables.isGameStarted = true;
    }
}

