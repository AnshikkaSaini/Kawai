using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fruit : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private FruitType TypeOfFruit;

    [Header("Actions")] 
    public static Action<Fruit,Fruit> onCollisionWithFruit;

    public void EnablePhysics()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = true;
    }

    public void MoveTo (Vector2 targetPosition)
    {
        transform.position = targetPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Fruit otherFruit))
        {
            if (otherFruit.GetFruitType() != TypeOfFruit)
            {
                return;
            }
            else
            {
                onCollisionWithFruit?.Invoke(this,otherFruit);   
            }
        }
    }
    public FruitType GetFruitType()
    {
        return TypeOfFruit;
    }
}