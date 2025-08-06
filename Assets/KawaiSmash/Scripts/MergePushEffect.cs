using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergePushEffect : MonoBehaviour
{
    [SerializeField] private bool enableGizmos;
    [SerializeField] private float pushRadius;
    [SerializeField] private float pushMagnitude;
    private Vector2 pushPosition; 
    private void Awake()
    {
        MergeManager.OnMergeProcessed += MergeProcessCallback;
    }
    private void OnDestroy()
    {
        MergeManager.OnMergeProcessed -= MergeProcessCallback;
    }

    private void MergeProcessCallback(FruitType fruitType, Vector2 mergePos)
    {
        pushPosition = mergePos;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mergePos, pushRadius);

        foreach (Collider2D collider in colliders)
        {
            if(collider.TryGetComponent(out Fruit fruits))
            {
                Vector2 force = ((Vector2)fruits.transform.position - mergePos).normalized;
                force *= pushMagnitude;
                Rigidbody rig = new Rigidbody();

                fruits.GetComponent<Rigidbody2D>().AddForce(force);

            }
        }
    }

   
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!enableGizmos)
        {
            return;
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pushPosition,pushRadius);
    }
#endif    
}
