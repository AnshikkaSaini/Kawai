using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource mergeSource;

    private void Awake()
    {
        MergeManager.OnMergeProcessed += MergeProcessCallback;
        SettingsManager.onSFX_ValueChanged += SFX_ValueChangedCallback;
    }
    private void OnDestroy()
    {
        MergeManager.OnMergeProcessed -= MergeProcessCallback;
        SettingsManager.onSFX_ValueChanged -= SFX_ValueChangedCallback;
    }
    private void Start()
    {
        bool sfxActive = PlayerPrefs.GetInt("sfxActiveKey", 1) == 1;
        mergeSource.mute = !sfxActive;
    }


    public void PlayMergeSounds()
    {
     
        mergeSource.pitch = Random.Range(0.9f, 1.1f);
        mergeSource.Play();
    }

    private void MergeProcessCallback(FruitType fruitType, Vector2 mergePos)
    {
        PlayMergeSounds();
    }

    private void SFX_ValueChangedCallback(bool SFX_Active)
    {
        //mergeSource.volume = SFX_Active ? 1 : 0;
        mergeSource.mute = !SFX_Active;
    }

}
