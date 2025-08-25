using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.Serialization;

public class SettingsManager : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private GameObject resetProgressprompt;
    [FormerlySerializedAs("pushForceSlider")]
    [SerializeField] private Slider pushForceMagnitudeSlider;
    [SerializeField] private Toggle SFX_Toggle;

    [Header("Data")]
    private const string lastPushMagnitudeKey = "lastPushMagnitudeKey";
    private const string sfxActiveKey = "sfxActiveKey";
    private bool canSave;


    public static Action<float> onPushmagnitudeSliderChange;
    public static Action<bool> onSFX_ValueChanged;

    private void Awake()
    {
        LoadData();
    }
  

    private IEnumerator Start()
    {
        Initialize();
        yield return new WaitForSeconds(1);
        canSave = true;
    }

    private void Initialize()
    {
        // Update listeners with current values
        onPushmagnitudeSliderChange?.Invoke(pushForceMagnitudeSlider.value);
        onSFX_ValueChanged?.Invoke(SFX_Toggle.isOn);

        // Subscribe to UI events programmatically
        pushForceMagnitudeSlider.onValueChanged.AddListener(delegate { SliderValueChangeCallback(); });
        SFX_Toggle.onValueChanged.AddListener(delegate (bool value) { ToggleCallback(value); });
    }

    public void ResetprogresssbuttonCallback()
    {
        resetProgressprompt.SetActive(true);
    }

    public void ResetProgressYes()
    {
        PlayerPrefs.DeleteAll();
        GameManager.Instance.ResetGame();
    }

    public void ResetProgressNo()
    {
        resetProgressprompt.SetActive(false);
    }

    public void SliderValueChangeCallback()
    {
        onPushmagnitudeSliderChange?.Invoke(pushForceMagnitudeSlider.value);
        SaveData();
    }

    public void ToggleCallback(bool is_SFX_Active)
    {
        onSFX_ValueChanged?.Invoke(is_SFX_Active);
        SaveData();
    }

    private void LoadData()
    {
        pushForceMagnitudeSlider.value = PlayerPrefs.GetFloat(lastPushMagnitudeKey, 5f);
        SFX_Toggle.isOn = PlayerPrefs.GetInt(sfxActiveKey, 1) == 1;
    }

    private void SaveData()
    {
        if (!canSave)
            return;

        PlayerPrefs.SetFloat(lastPushMagnitudeKey, pushForceMagnitudeSlider.value);
        int sfxValue = SFX_Toggle.isOn ? 1 : 0;
        PlayerPrefs.SetInt(sfxActiveKey, sfxValue);
        PlayerPrefs.Save();
    }
}
