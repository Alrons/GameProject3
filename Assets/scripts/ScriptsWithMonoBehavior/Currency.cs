using Assets.scripts.Constatns;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    Dictionary<int, KeyValuePair<string, double>> currencyDictionary = new Dictionary<int, KeyValuePair<string, double>>();

    Dictionary<int, GameObject> createdCurrencies = new Dictionary<int, GameObject>();

    public GameObject mainCamera;
    public Transform canvas;
    public GameObject pref;

    public Image Image;
    public Text textCurrencyValues;

    private BalanceService balanceService;

    private void Start()
    {
        //currencyDictionary[0] = new KeyValuePair<string, double>("path to image for currency1", 100.0);
        //currencyDictionary[1] = new KeyValuePair<string, double>("path to image for currency2", 100.0);
        //currencyDictionary[2] = new KeyValuePair<string, double>("path to image for currency3", 100.0);
        balanceService = new BalanceService();
        GetBalance();
    }

    private async void GetBalance()
    {
        try
        {
            var balanceResponse = await balanceService.GetBalance();

            if (balanceResponse?.Data?.BalanceList == null)
            {
                Debug.LogWarning("Balance response or data is null.");
                return;
            }

            foreach (var balanceEntry in balanceResponse.Data.BalanceList)
            {
                if (int.TryParse(balanceEntry.Key, out int currencyKey))
                {
                    currencyDictionary[currencyKey] = new KeyValuePair<string, double>(
                        currencyDictionary.TryGetValue(currencyKey, out var existingPair)
                            ? existingPair.Key
                            : "default_path",
                        balanceEntry.Value
                    );
                }
                else
                {
                    Debug.LogWarning($"Invalid currency key format: {balanceEntry.Key}");
                }
            }

            InitializedCurrency();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error getting balance: {ex.Message}");
        }
    }

    private void InitializedCurrency()
    {
        createdCurrencies.Clear();

        float positionOffsetX = 0.3f;

        foreach (var currencyPair in currencyDictionary)
        {
            GameObject currencyObject = CopyPref(pref, canvas);

            currencyObject.transform.localScale = new Vector3(0.26f, 0.975f, 1f);

            RectTransform rectTransform = currencyObject.GetComponent<RectTransform>();
            float newPosX = positionOffsetX * createdCurrencies.Count + 0.132f;
            rectTransform.anchoredPosition = new Vector2(newPosX, -0.5f);

            Image currencyImage = currencyObject.GetComponentInChildren<Image>();
            Text currencyText = currencyObject.GetComponentInChildren<Text>();

            currencyImage.sprite = Resources.Load<Sprite>(currencyPair.Value.Key);
            currencyText.text = currencyPair.Value.Value.ToString("F2");

            createdCurrencies[currencyPair.Key] = currencyObject;
        }
    }

    private void ReloadCurrency(int currency)
    {
        if (createdCurrencies.TryGetValue(currency, out var currencyObject))
        {
            Image currencyImage = currencyObject.GetComponentInChildren<Image>();
            Text currencyText = currencyObject.GetComponentInChildren<Text>();

            currencyText.text = currencyDictionary[currency].Value.ToString("F2");
            currencyImage.sprite = Resources.Load<Sprite>(currencyDictionary[currency].Key);
        }
    }

    public bool Purchase(int currency, double price)
    {
        if (currencyDictionary.TryGetValue(currency, out var currencyPair))
        {
            if (currencyPair.Value >= price)
            {
                double newAmount = currencyPair.Value - price;
                currencyDictionary[currency] = new KeyValuePair<string, double>(currencyPair.Key, newAmount);
                ReloadCurrency(currency);
                return true;
            }
            else
            {
                Debug.Log("Not enough funds for the purchase.");
                return false;
            }
        }
        else
        {
            Debug.Log("Currency not found.");
            return false;
        }
    }


    public bool Sale(int currency, double price)
    {
        if (currencyDictionary.TryGetValue(currency, out var currencyPair))
        {
            double newAmount = currencyPair.Value + (price * GameConst.SellReturn);
            currencyDictionary[currency] = new KeyValuePair<string, double>(currencyPair.Key, newAmount);
            ReloadCurrency(currency);
            return true;
        }
        else
        {
            Debug.Log("Currency not found.");
            return false;
        }
    }

    private GameObject CopyPref(GameObject Prefub, Transform setparent)
    {
        var spawn = Instantiate(Prefub);
        spawn.transform.SetParent(setparent.transform);
        spawn.transform.localScale = new Vector3(1, 1, 1);
        return spawn;
    }
}