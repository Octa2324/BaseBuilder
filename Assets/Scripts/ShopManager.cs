using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI leafsNumber;
    public TextMeshProUGUI moneyNumber;
    public TextMeshProUGUI logNumber;
    public TextMeshProUGUI logsNumber;

    private int leafs;
    private int money;
    private int log;
    private int logs;

    public GameObject firstHouse;
    public GameObject secondHouse;
    public GameObject thirdHouse;

    private Dictionary<int, GameObject> houses = new Dictionary<int, GameObject>();

    private int currentHouseIndex;

    private GameManager gameManager;

    public TextMeshProUGUI warningMessage;

    SoundEffectManager soundEffectManager;

    public Button buyHouseButton;

    public Button shopButton;


    void Start()
    {
        leafs = PlayerPrefs.GetInt("GoodLeafCount", 0);

        money = PlayerPrefs.GetInt("Money", 0);

        log = PlayerPrefs.GetInt("Log", 0);

        logs = PlayerPrefs.GetInt("Logs", 0) +20;

        currentHouseIndex = PlayerPrefs.GetInt("CurrentHouseIndex", 0);

        houses.Add(0, firstHouse);
        houses.Add(1, secondHouse);
        houses.Add(2, thirdHouse);

        gameManager = FindObjectOfType<GameManager>();

        UpdateUI();

        warningMessage.alpha = 0;

        soundEffectManager = FindObjectOfType<SoundEffectManager>();

        SetActiveHouse(currentHouseIndex);
    }

    void Update()
    {
        SetActiveHouse(currentHouseIndex);
    }

    public void BuyMoney()
    {
        if(leafs >= 5)
        {
            leafs -= 5;
            money += 1;
            PlayerPrefs.SetInt("GoodLeafCount", leafs);
            PlayerPrefs.SetInt("Money", money);
            PlayerPrefs.Save();
            soundEffectManager.Buy();
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough leafs!");
            ShowWarningMessage();
        }
    }

    public void BuyLog()
    {
        if(money >= 5)
        {
            money -= 5;
            log += 1;
            PlayerPrefs.SetInt("Money", money);
            PlayerPrefs.SetInt("Log", log);
            PlayerPrefs.Save();
            soundEffectManager.Buy();
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough money!");
            ShowWarningMessage();
        }
    }

    public void BuyLogs()
    {
        if (log >= 5)
        {
            log -= 5;
            logs += 1;
            PlayerPrefs.SetInt("Log", log);
            PlayerPrefs.SetInt("Logs", logs);
            PlayerPrefs.Save();
            soundEffectManager.Buy();
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough log!");
            ShowWarningMessage();
        }
    }

    public void BuyHouse()
    {
        if (logs >= 5)
        {
            logs -= 5;
            PlayerPrefs.SetInt("Logs", logs);

            currentHouseIndex++;
            if (currentHouseIndex >= houses.Count)
            {
                currentHouseIndex = houses.Count - 1; // Cap at the last house
                PlayerPrefs.SetInt("CurrentHouseIndex", currentHouseIndex);
                PlayerPrefs.Save();

                soundEffectManager.Upgrade();
                SetActiveHouse(currentHouseIndex);
                UpdateUI();
                StartCoroutine(ShowFinalPanelAfterDelay());
                buyHouseButton.interactable = false;
            }
            else
            {
                PlayerPrefs.SetInt("CurrentHouseIndex", currentHouseIndex);
                PlayerPrefs.Save();

                soundEffectManager.Upgrade();
                SetActiveHouse(currentHouseIndex);
                UpdateUI();

                gameManager.CloseShop();
            }
        }
        else
        {
            Debug.Log("Not enough logs!");
            ShowWarningMessage();
        }
    }

    private IEnumerator ShowFinalPanelAfterDelay()
    {
        yield return new WaitForSeconds(1f); 
        gameManager.OpenFinal(); 
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


    private void UpdateUI()
    {
        leafsNumber.text = leafs.ToString();
        moneyNumber.text = money.ToString();
        logNumber.text = log.ToString(); 
        logsNumber.text = logs.ToString(); 
    }

    private void ShowWarningMessage()
    {
        soundEffectManager.Cant();
        StartCoroutine(FadeText());
    }

    private IEnumerator FadeText()
    {
        float duration = 0.5f; 
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            warningMessage.alpha = t / duration;
            yield return null;
        }
        warningMessage.alpha = 1;

        yield return new WaitForSeconds(1f);

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            warningMessage.alpha = 1 - (t / duration);
            yield return null;
        }
        warningMessage.alpha = 0;
    }
}
