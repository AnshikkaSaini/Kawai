using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(FruitManager))]
public class FruitManagerUI : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI nextFruitText;

    private FruitManager fruitManager;
    void Start()
    {
        fruitManager = GetComponent<FruitManager>();
    }

  
    void Update()
    {
        nextFruitText.text = fruitManager.GetNextFruitName();
    }
}
