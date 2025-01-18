using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public List<Sprite> swordSprites;
    public Image swordImage;


    SoundEffectManager soundEffectManager;

    void Start()
    {
        int selectedSwordIndex = RuntimeDataManager.Instance.SelectedSwordIndex;
        SetSword(selectedSwordIndex);

        soundEffectManager = FindObjectOfType<SoundEffectManager>();
    }


    private void SetSword(int index)
    {
        swordImage.sprite = swordSprites[index];
        RuntimeDataManager.Instance.SelectedSwordIndex = index;
    }

    public void NextSword()
    {
        soundEffectManager.Select();
        int selectedSwordIndex = (RuntimeDataManager.Instance.SelectedSwordIndex + 1) % swordSprites.Count;
        SetSword(selectedSwordIndex);
    }

    public void PreviousSword()
    {
        soundEffectManager.Select();
        int selectedSwordIndex = (RuntimeDataManager.Instance.SelectedSwordIndex - 1 + swordSprites.Count) % swordSprites.Count;
        SetSword(selectedSwordIndex);
    }

    public void MainMenu()
    {
        soundEffectManager.Select();
        SceneManager.LoadScene("MainMenu");
    }
}
