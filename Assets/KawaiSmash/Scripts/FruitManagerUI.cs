using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(FruitManager))]

public class FruitManagerUI : MonoBehaviour
{
    [SerializeField] private Image _nextFruitImage;

    private FruitManager _fruitManager;
    
    private void Awake()
    {
        FruitManager.onNextFruitIndexSet += UpdateNextFruitImage;
    } 
    private void OnDestroy()
    {
        FruitManager.onNextFruitIndexSet -= UpdateNextFruitImage;
    }


    private void UpdateNextFruitImage()
    {
        if (_fruitManager == null)
            _fruitManager = GetComponent<FruitManager>();
        _nextFruitImage.sprite = _fruitManager.GetNextFruitSprite();
    }

 
}
