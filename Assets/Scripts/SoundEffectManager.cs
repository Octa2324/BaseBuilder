using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance;

    private AudioSource src;
    public AudioClip select, buy, upgrade, cant, hit, explode;

    private bool isMuted;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        src = GetComponent<AudioSource>();
        if (src == null)
        {
            src = gameObject.AddComponent<AudioSource>();
        }

        LoadMuteState();
        UpdateMuteState();
    }

    public void Select()
    {
        if (!isMuted && src != null && select != null)
        {
            src.PlayOneShot(select);
        }
    }

    public void Upgrade()
    {
        if (!isMuted && src != null && upgrade != null)
        {
            src.PlayOneShot(upgrade);
        }
    }

    public void Cant()
    {
        if (!isMuted && src != null && cant != null)
        {
            src.PlayOneShot(cant);
        }
    }

    public void Buy()
    {
        if (!isMuted && src != null && buy != null)
        {
            src.PlayOneShot(buy);
        }
    }

    public void Hit()
    {
        if (!isMuted && src != null && hit != null)
        {
            src.PlayOneShot(hit);
        }
    }

    public void Explode()
    {
        if (!isMuted && src != null && explode != null)
        {
            src.PlayOneShot(explode);
        }
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        UpdateMuteState();
        SaveMuteState();
    }

    public bool IsMuted()
    {
        return isMuted;
    }

    private void UpdateMuteState()
    {
        if (src != null)
        {
            src.mute = isMuted;
        }
    }

    private void SaveMuteState()
    {
        PlayerPrefs.SetInt("MuteState", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadMuteState()
    {
        isMuted = PlayerPrefs.GetInt("MuteState", 0) == 1;
    }
}
