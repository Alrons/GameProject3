using Assets.scripts.Constatns;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class Currency : MonoBehaviour
{
    Dictionary<int, CurrencyData> currencyDictionary = new Dictionary<int, CurrencyData>();
    Dictionary<int, GameObject> createdCurrencies = new Dictionary<int, GameObject>();

    public GameObject mainCamera;
    public Transform canvas;
    public GameObject pref;

    public Image Image;
    public Text textCurrencyValues;

    private ICurrencyService currencyService;
    private IBalanceService balanceService;

    private async void Start()
    {
        // currencyService = new CurrencyService();
        // balanceService = new BalanceService();

        currencyService = new MockCurrencyService(); 
        balanceService = new MockBalanceService(); 

        await LoadCurrencies();
    }

    private async Task LoadCurrencies()
    {
        try
        {
            var currencyResponse = await currencyService.GetCurrency();
            if (currencyResponse?.Data == null)
            {
                Debug.LogWarning("Currency response is null or empty.");
                return;
            }

            foreach (var currency in currencyResponse.Data)
            {
                currencyDictionary[currency.Id] = currency;
            }

            await LoadBalances();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error loading currencies: {ex.Message}");
        }
    }

    private async Task LoadBalances()
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
                if (int.TryParse(balanceEntry.Key, out int currencyKey) && currencyDictionary.TryGetValue(currencyKey, out var currency))
                {
                    currency.BalanceCurrency = balanceEntry.Value;
                }
            }

            InitializeCurrencies();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error loading balances: {ex.Message}");
        }
    }

    private void InitializeCurrencies()
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

            currencyImage.sprite = Resources.Load<Sprite>(currencyPair.Value.CurrencyType);
            currencyText.text = currencyPair.Value.BalanceCurrency.ToString("F2");

            createdCurrencies[currencyPair.Key] = currencyObject;
        }
    }

    private void ReloadCurrency(int currency)
    {
        if (!createdCurrencies.TryGetValue(currency, out var currencyObject))
        {
            Debug.LogError($"ReloadCurrency: currencyObject с ключом {currency} не найден в createdCurrencies.");
            return;
        }

        if (!currencyDictionary.TryGetValue(currency, out var currencyData))
        {
            Debug.LogError($"ReloadCurrency: currencyData с ключом {currency} не найден в currencyDictionary.");
            return;
        }

        Text currencyText = currencyObject.GetComponentInChildren<Text>();
        if (currencyText == null)
        {
            Debug.LogError($"ReloadCurrency: Текстовый компонент не найден у currencyObject ({currencyObject.name}).");
            return;
        }

        currencyText.text = currencyData.BalanceCurrency.ToString("F2");
        Debug.Log($"ReloadCurrency: Баланс обновлён — {currencyText.text} для валюты {currency}");
    }


    public bool Purchase(int currency, double price)
    {
        if (!currencyDictionary.TryGetValue(currency, out var currencyData))
        {
            Debug.LogWarning($"[Purchase] Currency with ID {currency} not found.");
            return false;
        }

        if (currencyData.BalanceCurrency < price)
        {
            Debug.LogWarning($"[Purchase] Not enough funds: current = {currencyData.BalanceCurrency}, required = {price}");
            return false;
        }

        currencyData.BalanceCurrency -= price;
        ReloadCurrency(currency);
        Debug.Log($"[Purchase] Purchase successful. New balance: {currencyData.BalanceCurrency}");
        return true;
    }


    public bool Sale(int currency, double price)
    {
        if (currencyDictionary.TryGetValue(currency, out var currencyData))
        {
            currencyData.BalanceCurrency += price * GameConst.SellReturn;
            ReloadCurrency(currency);
            return true;
        }

        Debug.Log("Currency not found.");
        return false;
    }

    private GameObject CopyPref(GameObject Prefub, Transform setparent)
    {
        var spawn = Instantiate(Prefub);
        spawn.transform.SetParent(setparent.transform);
        spawn.transform.localScale = new Vector3(1, 1, 1);
        return spawn;
    }
}
