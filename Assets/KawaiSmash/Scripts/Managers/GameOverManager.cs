using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameOverManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject deadline;

    [SerializeField] private Transform fruitsParent;

    [Header("Timer")] 
    [SerializeField] private float durationThreshold;
    private float timer;
    private bool timerOn;
    private bool isGameOver;
    
    
    void Update()
    {

        if (!isGameOver)
        {
            ManageGameOver();
        }
    }

    private void ManageGameOver()
    {
        if (timerOn)
        {
            ManageTimerOn();
        }
        else
        {
            if (IsFruitAboveLine())
            {
                StartTimer();
            }
        }
    }

    private void ManageTimerOn()
    {
        timer += Time.deltaTime;
 

        if (!IsFruitAboveLine())
        {
            StopTimer();
        }

        if (timer >= durationThreshold)
        {
            GameOver();
        }
    }

    private bool IsFruitAboveLine()
    {
        for (int i = 0; i < fruitsParent.childCount; i++)
        {
            Fruit fruit = fruitsParent.GetChild(i).GetComponent<Fruit>();

            if (!fruit.HasCollided())
            {
                continue;
            }
            Rigidbody2D rb = fruitsParent.GetChild(i).GetComponent<Rigidbody2D>();
            if (rb != null && rb.velocity.magnitude > 0.05f)
                continue; // still moving, ignore

            if (IsFruitAboveLine(fruitsParent.GetChild(i)))
            {
                return true;
            }
        }
        return false;
    }
    
    private bool IsFruitAboveLine(Transform fruit)
    {
        if (fruit.position.y > deadline.transform.position.y)
        {
            return true;
        }

        return false;
    }

    private void StartTimer()
    {
        timer = 0;
        timerOn = true;
    }
    private void StopTimer()
    {
        timerOn = false;
        timer = 0;
    }

    private void GameOver()
    {
        Debug.LogError("Game Over");
        isGameOver = true;
        GameManager.GameManagerInstance.SetGameOverState();
    }


   
}
