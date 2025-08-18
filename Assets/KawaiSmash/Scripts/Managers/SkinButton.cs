using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    //Elements
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject selectionOutline;



    public void Configure(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }

    public void Select() => selectionOutline.SetActive(true);
    public void DeSelect() => selectionOutline.SetActive(false);

}
