using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public List<Sprite> swordSprites;
    public Image swordImage;

    private int selectedSwordIndex = 0;

    SoundEffectManager soundEffectManager;

    void Start()
    {
        selectedSwordIndex = PlayerPrefs.GetInt("SelectedSword", 0);
        SetSword(selectedSwordIndex);

        soundEffectManager = FindObjectOfType<SoundEffectManager>();
    }


    private void SetSword(int index)
    {
        swordImage.sprite = swordSprites[index];
        PlayerPrefs.SetInt("SelectedSword", selectedSwordIndex);
        PlayerPrefs.Save();
    }

    public void NextSword()
    {
        soundEffectManager.Select();
        selectedSwordIndex = (selectedSwordIndex + 1) % swordSprites.Count;
        SetSword(selectedSwordIndex);
    }

    public void PreviousSword()
    {
        soundEffectManager.Select();
        selectedSwordIndex = (selectedSwordIndex - 1 + swordSprites.Count) % swordSprites.Count;
        SetSword(selectedSwordIndex);
    }

    public void MainMenu()
    {
        soundEffectManager.Select();
        SceneManager.LoadScene("MainMenu");
    }
}
