using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource mergeSource;

    private void Awake()
    {
        MergeManager.OnMergeProcessed += MergeProcessCallback;
    }
    private void OnDestroy()
    {
        MergeManager.OnMergeProcessed -= MergeProcessCallback;
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
  
}
