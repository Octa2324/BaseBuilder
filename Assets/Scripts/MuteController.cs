using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteController : MonoBehaviour
{
    public Button muteButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    private void Start()
    {
        if (muteButton != null)
        {
            muteButton.onClick.AddListener(ToggleMute);
            UpdateButtonSprite();
        }
    }

    private void ToggleMute()
    {
        if (SoundEffectManager.Instance != null)
        {
            SoundEffectManager.Instance.ToggleMute();
            UpdateButtonSprite();
        }
    }

    private void UpdateButtonSprite()
    {
        if (muteButton != null && muteButton.image != null && SoundEffectManager.Instance != null)
        {
            muteButton.image.sprite = SoundEffectManager.Instance.IsMuted() ? musicOffSprite : musicOnSprite;
        }
    }
}