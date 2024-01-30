using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDeath : MonoBehaviour
{
    private Rigidbody2D rb;
    private SlowMotion slowMotion;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        slowMotion = GetComponent<SlowMotion>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GameOver"))
        {
            Die();
        }
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        slowMotion.StopSlowMotionAndResetPower();
        Invoke("RestartLevel", 0.01f);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
