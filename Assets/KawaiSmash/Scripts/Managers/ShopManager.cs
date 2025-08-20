using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private SkinButton skinButtonPrefab;
    [SerializeField] private Transform skinButtonParent;
    [SerializeField] private GameObject PurchaseButton;
    [SerializeField] private TextMeshProUGUI skinLabelText;
    [SerializeField] private TextMeshProUGUI skinPriceText;

    [Header("Data")]
    [SerializeField] private SkinDataSO[] skinDataSos;

    private int lastSelectedSkin;
    private bool[] unlockedStates;
    private Button purchaseBtn;

    private const string SKIN_BUTTON_KEY = "SkinButton_";
    private const string LAST_SELECTED_SKIN_KEY = "LastSelectedSkinKey";

    public static Action<SkinDataSO> onSkinSelected;

    void Awake()
    {
        unlockedStates = new bool[skinDataSos.Length];
        purchaseBtn = PurchaseButton.GetComponent<Button>();
        purchaseBtn.onClick.AddListener(PurchaseButtonCallback);
    }

    void Start()
    {
        Init();
        LoadData();
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

    public void PurchaseButtonCallback()
    {
        int price = skinDataSos[lastSelectedSkin].GetPrice();

        if (CoinManager.instance.CanPurchase(price))
        {
            CoinManager.instance.SpendCoins(price);

            unlockedStates[lastSelectedSkin] = true;
            SaveData();

            SkinButtonClickedCallback(lastSelectedSkin);
        }
        else
        {
            Debug.LogWarning("Not enough coins to purchase this skin!");
        }
    }

    private void SkinButtonClickedCallback(int skinButtonIndex, bool shouldSaveLastSkin = true)
    {
        lastSelectedSkin = skinButtonIndex;

        for (int i = 0; i < skinButtonParent.childCount; i++)
        {
            SkinButton currentSkinButton = skinButtonParent.GetChild(i).GetComponent<SkinButton>();
            if (i == skinButtonIndex)
                currentSkinButton.Select();
            else
                currentSkinButton.DeSelect();
        }

        if (IsSkinUnlocked(skinButtonIndex))
            onSkinSelected?.Invoke(skinDataSos[skinButtonIndex]);

        if (shouldSaveLastSkin)
            SaveLastSelectedSkin();
        skinPriceText.text = skinDataSos[skinButtonIndex].GetPrice().ToString();

        UpdateSkinLabel(skinButtonIndex);
        UpdatePurchaseButton(skinButtonIndex);
    }

    private void UpdatePurchaseButton(int skinButtonIndex)
    {
        bool unlocked = IsSkinUnlocked(skinButtonIndex);

        purchaseBtn.interactable = !unlocked && CoinManager.instance.CanPurchase(skinDataSos[skinButtonIndex].GetPrice());
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
            if (i == 0) unlockedValue = 1; // First skin unlocked by default
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
