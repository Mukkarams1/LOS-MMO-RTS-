using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceForBg;
    [SerializeField] private AudioSource audioSourceForBtn;
    [SerializeField] private AudioClip[] BtnClickClips;
    [SerializeField] private AudioClip MainMenuClip;
    [SerializeField] private AudioClip GamePLayClip;
    [SerializeField] public List<LOSSoundClip> soundClips;
    public static SoundManager instance;

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
    }
    void Update()
    {
        if (audioSourceForBg == null)
        {
            audioSourceForBg = Camera.main.gameObject.GetComponent<AudioSource>();
        }
        SetBGSound();
    }
    public void SetBGSound()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            audioSourceForBg.clip = GamePLayClip;
            if (audioSourceForBg.isPlaying == false)
            {
                audioSourceForBg.Play();
            }
        }
        else
        {
            audioSourceForBg.clip = MainMenuClip;
            if (audioSourceForBg.isPlaying == false)
            {
                audioSourceForBg.Play();
            }
        }
    }
    public void PlaySound(string id)
    {
        if (GetAudioClipFromID(id) != null)
            audioSourceForBtn.PlayOneShot(GetAudioClipFromID(id));
        else
        {
            Debug.Log("Sound Clip with ID " + id + " not found");
        }
    }

    private AudioClip GetAudioClipFromID(string id)
    {
        return soundClips.Find(x => x.id == id).clip;
    }

    int GetSoundIDInt(string id)
    {
        if (id == "ButtonClick")
        {
            return 0;
        }
        return 0;
    }
    

   
    
}

[System.Serializable]
public class LOSSoundClip
{
    [SerializeField] public string id;
    [SerializeField] public AudioClip clip;
}