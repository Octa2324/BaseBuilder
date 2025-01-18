using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isSwordActive = false;

    public GameObject menuPanel;
    public GameObject shopPanel;
    public GameObject pausePanel;
    public GameObject finalPanel;


    private LeafsController leafsController;


    private RectTransform shopPanelRectTransform;

    private CanvasGroup shopPanelCanvasGroup;
    private CanvasGroup pausePanelCanvasGroup; 
    private CanvasGroup finalPanelCanvasGroup;
    public float panelSpeed = 1f;

    SoundEffectManager soundEffectManager;

    public GameObject firstHouse;
    public GameObject secondHouse;
    public GameObject thirdHouse;

    private Dictionary<int, GameObject> houses = new Dictionary<int, GameObject>();

    private int currentHouseIndex;



    private void Start()
    {
        menuPanel.SetActive(true);
        shopPanel.SetActive(false);
        pausePanel.SetActive(false);

        leafsController = FindObjectOfType<LeafsController>();
        shopPanelCanvasGroup = shopPanel.GetComponent<CanvasGroup>();
        pausePanelCanvasGroup = pausePanel.GetComponent<CanvasGroup>();
        finalPanelCanvasGroup = finalPanel.GetComponent<CanvasGroup>();

        shopPanelCanvasGroup.alpha = 0f;
        pausePanelCanvasGroup.alpha = 0f;
        finalPanelCanvasGroup.alpha = 0f;

        soundEffectManager = SoundEffectManager.Instance;

        currentHouseIndex = PlayerPrefs.GetInt("CurrentHouseIndex", 0);

        houses.Add(0, firstHouse);
        houses.Add(1, secondHouse);
        houses.Add(2, thirdHouse);

        SetActiveHouse(currentHouseIndex);


    }



    public void SetActiveHouse(int houseIndex)
    {
        foreach (var house in houses.Values)
        {
            house.SetActive(false);
        }

        if (houses.ContainsKey(houseIndex))
        {
            houses[houseIndex].SetActive(true);
        }
    }


    public void StartGame()
    {
        soundEffectManager.Select();
        isSwordActive = true;
        menuPanel.SetActive(false);
        shopPanel.SetActive(false);
        finalPanel.SetActive(false);
    }

    public void OpenShop()
    {
        soundEffectManager.Select();
        menuPanel.SetActive(false);
        shopPanel.SetActive(true);
        StartCoroutine(FadeInPanel(shopPanelCanvasGroup));
    }

    public void CloseShop()
    {
        soundEffectManager.Select();
        StartCoroutine(FadeOutPanel(shopPanelCanvasGroup));
        menuPanel.SetActive(true);
    }

    public void OpenPause()
    {
        soundEffectManager.Select();
        menuPanel.SetActive(false);
        pausePanel.SetActive(true);
        StartCoroutine(FadeInPanel(pausePanelCanvasGroup));
    }

    public void ClosePause()
    {
        soundEffectManager.Select();
        StartCoroutine(FadeOutPanel(pausePanelCanvasGroup));
        menuPanel.SetActive(true);
    }

    public void ColoseFinal()
    {
        soundEffectManager.Select();
        StartCoroutine(FadeOutPanel(finalPanelCanvasGroup));
        menuPanel.SetActive(true);
    }

    public void OpenFinal()
    {
        menuPanel.SetActive(false);
        shopPanel.SetActive(false);
        finalPanel.SetActive(true);
        StartCoroutine(FadeInPanel(finalPanelCanvasGroup));
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
        shopPanel.SetActive(false);
        pausePanel.SetActive(false);
        finalPanel.SetActive(false);
    }

    public void MainMenu()
    {
        soundEffectManager.Select();
        SceneManager.LoadScene("MainMenu");
    }


}
