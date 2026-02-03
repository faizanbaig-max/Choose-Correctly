using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public List<SoundData> soundData;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundClipType type)
    {
        for (int i = 0; i < soundData.Count; i++)
        {
            if (soundData[i].type==type)
            {
                audioSource.clip = soundData[i].clip;
                audioSource.Play(); 
            }
        }
    }



}
[System.Serializable]
public class SoundData
{
    public AudioClip clip;
    public SoundClipType type;
}
public enum SoundClipType
{
    FlipTile,
    matching,
    mismatch,
    win,
    fail
}

