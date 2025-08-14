using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinData", menuName = "ScriptabelObjects/SkinData", order = 0)]
public class SkinDataSO : ScriptableObject
{
    [SerializeField] private Fruit[] objectprefabs;
    [SerializeField] private Fruit[] spawnablePrefabs;


    public Fruit[] GetObjectsPrefab()
    {
        return objectprefabs;
        
    }
    public Fruit[] GetSpawnablePrefabs()
    {
        return spawnablePrefabs;
        
    }

}
