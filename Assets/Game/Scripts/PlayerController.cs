using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
public class PlayerController : MonoBehaviour
{

    private bool wrongMove = false;
    public EffectCountdown effectCountdown;

    void Update()
    {
         if (wrongMove) 
        {
            GameManager.Instance.PlayerLost();
            wrongMove = false; // Set the flag to true to prevent further calls
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            SoundManager.Instance.RightMove();
            ScoreManager.Instance.IncrementScore();
        }

        if (collision.gameObject.CompareTag("Wrong"))
        {
            SoundManager.Instance.WrongMove();
            wrongMove = true;
        }
    }

    public void ResetPlayer()
    {
        wrongMove = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // if (other.CompareTag("SlowMotion"))
        // {
        //     PowerUps.Instance.ActivateSlowMotion();
        //     effectCountdown.StartSlowMotionCountdown();
        //     Destroy(other.gameObject);
        // }

        if (other.CompareTag("ExtraPoints"))
        {
            SoundManager.Instance.Bite();
            PowerUps.Instance.ActivateExtraPoints();
            effectCountdown.StartExtraPointsCountdown();
            Destroy(other.gameObject);
        }
    }
}