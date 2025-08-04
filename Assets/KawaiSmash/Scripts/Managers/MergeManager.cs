using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MergeManager : MonoBehaviour
{
    [Header("Action")] 
    public static Action<FruitType, Vector2> OnMergeProcessed;
    
    [Header("Settings")]
    private Fruit lastSender;
    void Start()
    {
        Fruit.onCollisionWithFruit += CollisionBetweenFruitsCallback;
    }
    
    private void CollisionBetweenFruitsCallback(Fruit sender, Fruit otherFruit)
    {
        if (lastSender != null)
            return;
        lastSender = sender;
        ProcessMerge(sender, otherFruit);
        Debug.Log("Collision Dectected" + sender.name);
    }
    
    private void ProcessMerge( Fruit sender, Fruit otherFruit)
    {
        FruitType MergeFruitType = sender.GetFruitType();
        MergeFruitType += 1;
        
        Vector2 fruitSpawnPos = 
            (sender.transform.position + otherFruit.transform.position) / 2;
        
        Destroy(sender?.gameObject);
        Destroy(otherFruit?.gameObject);

        StartCoroutine(ResetLastSenderCoroutine());
        
        OnMergeProcessed?.Invoke(MergeFruitType, fruitSpawnPos);
    }

    IEnumerator ResetLastSenderCoroutine()
    {
        yield return new WaitForEndOfFrame();
        lastSender = null;
    }
}
