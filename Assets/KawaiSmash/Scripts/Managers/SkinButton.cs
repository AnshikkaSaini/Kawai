using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    //Elements
    [SerializeField] private Button button;
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject selectionOutline;



    public void Configure(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }

    public void Select()
    {
        if (selectionOutline == null)
        {
            Debug.LogError($"{name} has no selectionOutline assigned!");
            return;
        }
        selectionOutline.SetActive(true);
    }

    public Button GetButton()
    {
        return button;
    }

    public void DeSelect() => selectionOutline.SetActive(false);

}
