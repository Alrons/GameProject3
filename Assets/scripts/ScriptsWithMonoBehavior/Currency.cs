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

    private CurrencyService currencyService;
    private BalanceService balanceService;

    private async void Start()
    {
        currencyService = new CurrencyService();
        balanceService = new BalanceService();
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
        if (createdCurrencies.TryGetValue(currency, out var currencyObject) && currencyDictionary.TryGetValue(currency, out var currencyData))
        {
            Text currencyText = currencyObject.GetComponentInChildren<Text>();
            currencyText.text = currencyData.BalanceCurrency.ToString("F2");
        }
    }

    public bool Purchase(int currency, double price)
    {
        if (currencyDictionary.TryGetValue(currency, out var currencyData) && currencyData.BalanceCurrency >= price)
        {
            currencyData.BalanceCurrency -= price;
            ReloadCurrency(currency);
            return true;
        }

        Debug.Log("Not enough funds for the purchase.");
        return false;
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
