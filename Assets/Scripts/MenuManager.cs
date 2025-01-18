using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject infoPanel;
    public GameObject settingsPanel;


    private CanvasGroup settingsPanelCanvasGroup;
    private CanvasGroup infoPanelCanvasGroup;
    public float panelSpeed = 1f;

    SoundEffectManager soundEffectManager;

    void Start()
    {
        menuPanel.SetActive(true);
        infoPanel.SetActive(false);
        settingsPanel.SetActive(false);

        settingsPanelCanvasGroup = settingsPanel.GetComponent<CanvasGroup>();
        infoPanelCanvasGroup = infoPanel.GetComponent<CanvasGroup>();

        settingsPanelCanvasGroup.alpha = 0f;
        infoPanelCanvasGroup.alpha = 0f;

        soundEffectManager = FindObjectOfType<SoundEffectManager>();
    }

    void Update()
    {
        
    }

    public void OpenInfo()
    {
        soundEffectManager.Select();
        menuPanel.SetActive(false);
        infoPanel.SetActive(true);
        StartCoroutine(FadeInPanel(infoPanelCanvasGroup));
    }

    public void CloseInfo()
    {
        soundEffectManager.Select();
        StartCoroutine(FadeOutPanel(infoPanelCanvasGroup));
        menuPanel.SetActive(true);
    }

    public void OpenSettings()
    {
        soundEffectManager.Select();
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        StartCoroutine(FadeInPanel(settingsPanelCanvasGroup));
    }

    public void CloseSettings()
    {
        soundEffectManager.Select();
        StartCoroutine(FadeOutPanel(settingsPanelCanvasGroup));
        menuPanel.SetActive(true);
    }

    public void Play()
    {
        soundEffectManager.Select();
        SceneManager.LoadScene("Game");
    }

    private IEnumerator FadeInPanel(CanvasGroup panel)
    {
        float timeElapsed = 0f;
        float startAlpha = panel.alpha;

        while (timeElapsed < 1f)
        {
            panel.alpha = Mathf.Lerp(startAlpha, 1f, timeElapsed);
            timeElapsed += Time.deltaTime * panelSpeed;
            yield return null;
        }

        panel.alpha = 1f;
    }

    private IEnumerator FadeOutPanel(CanvasGroup panel)
    {
        float timeElapsed = 0f;
        float startAlpha = panel.alpha;

        while (timeElapsed < 1f)
        {
            panel.alpha = Mathf.Lerp(startAlpha, 0f, timeElapsed);
            timeElapsed += Time.deltaTime * panelSpeed;
            yield return null;
        }

        panel.alpha = 0f;
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        infoPanel.SetActive(false);
    }
}
