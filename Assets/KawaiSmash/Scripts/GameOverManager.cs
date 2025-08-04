using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject deadline;

    [SerializeField] private Transform fruitsParent;
    void Start()
    {
        
    }


    void Update()
    {
        CheckForGameOver(); 
        
    }

    private void CheckForGameOver()
    {
        for (int i = 0; i < fruitsParent.childCount; i++)
        {
            Fruit fruit = fruitsParent.GetChild(i).GetComponent<Fruit>();
            
            if (!fruit.FruitCollided())
            {
                continue;
            }

            CheckIfFruitAboveDeadLine(fruitsParent.GetChild(i));
        }
    }

    private void CheckIfFruitAboveDeadLine(Transform fruit)
    {
        if (fruit.position.y > deadline.transform.position.y)
        {
            Debug.Log("Game Over");
        }
    }

}
