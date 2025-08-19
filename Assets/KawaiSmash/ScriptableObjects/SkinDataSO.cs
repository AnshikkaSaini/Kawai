using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinData", menuName = "ScriptabelObjects/SkinData", order = 0)]
public class SkinDataSO : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private Fruit[] objectprefabs;
    [SerializeField] private Fruit[] spawnablePrefabs;
    [SerializeField] private int price;

    public string GetName()
    {
        return name;
    }

    public Fruit[] GetObjectsPrefab()
    {
        return objectprefabs;
        
    }
    public Fruit[] GetSpawnablePrefabs()
    {
        return spawnablePrefabs;
        
    }

    public int GetPrice()
    {
        return price;
    }
}
