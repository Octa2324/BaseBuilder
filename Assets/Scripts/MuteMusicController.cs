using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteMusicController : MonoBehaviour
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
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.ToggleMute();
            UpdateButtonSprite();
        }
    }

    private void UpdateButtonSprite()
    {
        if (muteButton != null && muteButton.image != null && MusicManager.Instance != null)
        {
            muteButton.image.sprite = MusicManager.Instance.IsMuted() ? musicOffSprite : musicOnSprite;
        }
    }
}
