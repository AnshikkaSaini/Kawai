using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoinText : MonoBehaviour
{
    public void UpdateText(String text)
    {
        GetComponent<TextMeshProUGUI>().text = text;
    }
}
