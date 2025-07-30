using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

public class Fruit : MonoBehaviour
{

    [Header("Elements")] [SerializeField] 
    private SpriteRenderer _spriteRenderer;
    
    
    [Header("Data")] 
    [FormerlySerializedAs("TypeOfFruit")]
    [Header("Data")]
    [SerializeField] private FruitType typeOfFruit;

    [Header("Actions")] 
    public static Action<Fruit,Fruit> onCollisionWithFruit;

    public void EnablePhysics()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;// Objects that should move and be affected by physics (gravity, forces, collisions).
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
            if (otherFruit.GetFruitType() != typeOfFruit)
            {
                return;
            }
            else
            {
                onCollisionWithFruit?.Invoke(this,otherFruit);   //subscribing to the event when same fruits collide
            }
        }
    }
    public FruitType GetFruitType()
    {
        return typeOfFruit;
    }

    public Sprite GetSprite()
    {
        return _spriteRenderer.sprite;
    }

}