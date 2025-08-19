using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // Elements
    [SerializeField] private SkinButton skinButtonPrefab;
    [SerializeField] private Transform skinButtonParent;
    [SerializeField] private GameObject PurchaseButton;
    [SerializeField] private TextMeshProUGUI skinLabelText;
    [SerializeField] private GameObject notEnoughCoins;

    // Data
    [SerializeField] private SkinDataSO[] skinDataSos;

    // Variables
    private int lastSelectedSkin;
    private bool[] unlockedStates;
    private Button purchaseBtn;

    // Constants
    private const string SKIN_BUTTON_KEY = "SkinButton_";
    private const string LAST_SELECTED_SKIN_KEY = "LastSelectedSkinKey";

    // Actions
    public static Action<SkinDataSO> onSkinSelected;

    void Awake()
    {
        unlockedStates = new bool[skinDataSos.Length];
        purchaseBtn = PurchaseButton.GetComponent<Button>();
    }

    void Start()
    {
        Init();
        LoadData();
    }

    public void PurchaseButtonCallback()
    {
        int price = skinDataSos[lastSelectedSkin].GetPrice();
        bool canPurchase = CoinManager.instance.CanPurchase(price);

        if (canPurchase)
        {
            // Deduct coins if your CoinManager has a Spend method
            // CoinManager.instance.SpendCoins(price);

            // Unlock the selected skin
            unlockedStates[lastSelectedSkin] = true;
            SaveData();

            // Hide "Not Enough Coins" message
            notEnoughCoins.SetActive(false);

            // Refresh UI for the unlocked skin
            SkinButtonClickedCallback(lastSelectedSkin);
        }
        else
        {
            // Show "Not Enough Coins" message only when trying to buy without enough
            notEnoughCoins.SetActive(true);
        }
    }


    private void Init()
    {
        for (int i = 0; i < skinDataSos.Length; i++)
        {
            SkinButton skinButtonInstance = Instantiate(skinButtonPrefab, skinButtonParent);
            skinButtonInstance.Configure(skinDataSos[i].GetObjectsPrefab()[0].GetSprite());

            int index = i; // Capture local index
            skinButtonInstance.GetButton().onClick.AddListener(() => SkinButtonClickedCallback(index));
        }
    }

    private void SkinButtonClickedCallback(int skinButtonIndex, bool shouldSaveLastSkin = true)
    {
        lastSelectedSkin = skinButtonIndex;

        // Update button selection visuals
        for (int i = 0; i < skinButtonParent.childCount; i++)
        {
            SkinButton currentSkinButton = skinButtonParent.GetChild(i).GetComponent<SkinButton>();
            if (i == skinButtonIndex)
                currentSkinButton.Select();
            else
                currentSkinButton.DeSelect();
        }

        // Invoke action if skin is unlocked
        if (IsSkinUnlocked(skinButtonIndex))
            onSkinSelected?.Invoke(skinDataSos[skinButtonIndex]);

        // Save last selected skin
        if (shouldSaveLastSkin)
            SaveLastSelectedSkin();

        // Update purchase button and label
        ManagePurchaseButtonVisibility(skinButtonIndex);
        UpdateSkinLabel(skinButtonIndex);
    }

    private void ManagePurchaseButtonVisibility(int skinButtonIndex)
    {
        bool isUnlocked = IsSkinUnlocked(skinButtonIndex);

        if (isUnlocked)
        {
            // Keep the button visible but disabled
            PurchaseButton.SetActive(true);
            purchaseBtn.interactable = false;

            // Hide the "Not Enough Coins" message
            notEnoughCoins.SetActive(false);
        }
        else
        {
            PurchaseButton.SetActive(true);

            bool canPurchase = CoinManager.instance.CanPurchase(skinDataSos[skinButtonIndex].GetPrice());
            purchaseBtn.interactable = canPurchase;

            // Don’t show "Not Enough Coins" here — only on button click
            notEnoughCoins.SetActive(false);
        }
    }



    private void UpdateSkinLabel(int skinButtonIndex)
    {
        skinLabelText.text = skinDataSos[skinButtonIndex].GetName();
    }

    private bool IsSkinUnlocked(int skinButtonIndex)
    {
        return unlockedStates[skinButtonIndex];
    }

    private void LoadData()
    {
        for (int i = 0; i < unlockedStates.Length; i++)
        {
            int unlockedValue = PlayerPrefs.GetInt(SKIN_BUTTON_KEY + i, 0);
            if (i == 0) unlockedValue = 1; // Default first skin unlocked
            unlockedStates[i] = unlockedValue == 1;
        }

        LoadLastSelectedSkin();
    }

    private void SaveData()
    {
        for (int i = 0; i < unlockedStates.Length; i++)
        {
            PlayerPrefs.SetInt(SKIN_BUTTON_KEY + i, unlockedStates[i] ? 1 : 0);
        }
    }

    private void LoadLastSelectedSkin()
    {
        int lastSkin = PlayerPrefs.GetInt(LAST_SELECTED_SKIN_KEY, 0);
        SkinButtonClickedCallback(lastSkin, false);
    }

    private void SaveLastSelectedSkin()
    {
        PlayerPrefs.SetInt(LAST_SELECTED_SKIN_KEY, lastSelectedSkin);
    }
}
