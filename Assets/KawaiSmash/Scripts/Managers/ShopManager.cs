using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    //Elements

    [SerializeField] private SkinButton skinButtonPrefab;
    [SerializeField] private Transform skinButtonParent;
    
    //Data

    [SerializeField] private SkinDataSO[] skinDataSos;
   
    void Start()
    {
       Init();
    }
    
    private void Init()
    {
        for (int i = 0; i < skinDataSos.Length; i++)
        {
           SkinButton skinButtonInstance = Instantiate(skinButtonPrefab, skinButtonParent);
           skinButtonInstance.Configure(skinDataSos[i].GetObjectsPrefab()[0].GetSprite());
           if (i == 0)
           {
               skinButtonInstance.Select();
           }
        }
    }
}
