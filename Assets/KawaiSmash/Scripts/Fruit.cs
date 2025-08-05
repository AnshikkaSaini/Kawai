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
    [SerializeField] private FruitType typeOfFruit;

    private bool canBeMerged;

    private bool hasCollided;

    [Header("Actions")] 
    public static Action<Fruit,Fruit> onCollisionWithFruit;
    

    [Header("Effects")] [SerializeField] private ParticleSystem mergeParticles;

    private void Start()
    {
        Invoke("AllowMerge", .25f);
    }

    private void AllowMerge()
    {
        canBeMerged = true;
    }

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
        ManageCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ManageCollision(collision);
    }

    private void ManageCollision( Collision2D collision)
    {
        hasCollided = true;
        
        if (!canBeMerged)
        {
            return;
        }
        if (collision.collider.TryGetComponent(out Fruit otherFruit))
        {
            if (otherFruit.GetFruitType() != typeOfFruit)
            {
                return;
            }
            else if (!otherFruit.canBeMerged)
            {
                return;
            }
            else
            {
                onCollisionWithFruit?.Invoke(this,otherFruit);   //subscribing to the event when same fruits collide
            }
        }
    }

    public void Merge()
    {
        if (mergeParticles != null)
        {
        mergeParticles.transform.SetParent(null);
        mergeParticles.Play();
        }

        Destroy(gameObject);
    }

    public FruitType GetFruitType()
    {
        return typeOfFruit;
    }

    public Sprite GetSprite()
    {
        return _spriteRenderer.sprite;
    }

    public bool FruitCollided()
    {
        return hasCollided;
    }

    public bool CanbeMerged()
    {
        return canBeMerged;
    }

}