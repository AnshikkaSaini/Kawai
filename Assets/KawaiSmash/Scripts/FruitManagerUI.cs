using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(FruitManager))]

public class FruitManagerUI : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI nextFruitText;

    [SerializeField] private Image _nextFruitImage;

    private FruitManager _fruitManager;
    void Start()
    {
        _fruitManager = GetComponent<FruitManager>();
    }

  
    void Update()
    {
        nextFruitText.text = _fruitManager.GetNextFruitName();
        _nextFruitImage.sprite = _fruitManager.GetNextFruitSprite();
    }
}
