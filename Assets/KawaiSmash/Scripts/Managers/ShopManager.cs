using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopManager : MonoBehaviour
{
    //Elements
    [SerializeField] private SkinButton skinButtonPrefab;
    [SerializeField] private Transform skinButtonParent;
    [SerializeField] private GameObject PurchaseButton;
    //Data
    [SerializeField] private SkinDataSO[] skinDataSos;
    //Variables
    private int lastSelectedSkin;
    private bool[] unlockedStates;

    private const String SKIN_BUTTON_KEY = "SkinButton_";

    private const String LAST_SELECTED_SKIN_KEY = "LastSelectedSkinKey";
    //Actions
    public static Action<SkinDataSO> onSkinSelected;
    

    void Awake()
    {
        unlockedStates = new bool [skinDataSos.Length];
      
    
    }


    void Start()
    { 
       Init();
       LoadData();
    }

    public void PurchaseButtonCallaback()
    {
        unlockedStates[lastSelectedSkin] = true;
        SaveData();
        SkinButtonClickedCallback(lastSelectedSkin);
    }

    private void Init()
    {
        for (int i = 0; i < skinDataSos.Length; i++)
        {
            
            PurchaseButton.SetActive(false);
            SkinButton skinButtonInstance = Instantiate(skinButtonPrefab, skinButtonParent);
            skinButtonInstance.Configure(skinDataSos[i].GetObjectsPrefab()[0].GetSprite());

            int j = i; // capture index
            skinButtonInstance.GetButton().onClick.AddListener(() => SkinButtonClickedCallback(j));

           
        }
    }
    private void SkinButtonClickedCallback(int skinButtonIndex, bool shouldSaveLastSkin = true )
    {
        lastSelectedSkin = skinButtonIndex;
        Debug.Log($"Skin button {skinButtonIndex} clicked!");

        for (int i = 0; i < skinButtonParent.childCount; i++)
        {
            SkinButton currentSkinButton = skinButtonParent.GetChild(i).GetComponent<SkinButton>();

            if (i == skinButtonIndex)
            {
                currentSkinButton.Select();
            }
            else
            {
                currentSkinButton.DeSelect();
            }

            if (IsSkinUnlocked(skinButtonIndex))
            {
                onSkinSelected?.Invoke((skinDataSos[skinButtonIndex]));
                if (shouldSaveLastSkin)
                {
                    SaveLastSelectedSkin();
                }

            }
        }

        ManagePurchaseButtonVisiBility(skinButtonIndex);
    }

    private void ManagePurchaseButtonVisiBility(int skinButtonIndex)
    {
        PurchaseButton.SetActive(!IsSkinUnlocked(skinButtonIndex));
    }

    private bool IsSkinUnlocked(int skinButtonIndex)
    {
        return unlockedStates[skinButtonIndex];
    }

    private void LoadData()
    {
        for (int i = 0; i < unlockedStates.Length; i++)
        {
            int unlockedValue =  PlayerPrefs.GetInt(SKIN_BUTTON_KEY+ i);
            if (i == 0)
            {
                unlockedValue = 1;
            }

            if (unlockedValue == 1)
                unlockedStates[i] = true;
        }
        LoadLastSlectedSkin();
       
    }

    private void SaveData()
    {
        for (int i = 0; i < unlockedStates.Length; i++)
        {
            int unlockedValue = unlockedStates[i] ? 1 : 0;
            PlayerPrefs.SetInt(SKIN_BUTTON_KEY + i, unlockedValue);
        }
    }

    private void LoadLastSlectedSkin()
    {
        int lastSelectedSkinIndex = PlayerPrefs.GetInt(LAST_SELECTED_SKIN_KEY);
        SkinButtonClickedCallback(lastSelectedSkinIndex, false);
    }

    private void SaveLastSelectedSkin()
    {
        PlayerPrefs.SetInt(LAST_SELECTED_SKIN_KEY, lastSelectedSkin);
    }
}
