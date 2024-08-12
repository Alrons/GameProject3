using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    // in order to change the price, you must first change the value in the list, then run ChangeValues()
    public List<string> currencyNames = new List<string>();
    public List<int> currencyValues = new List<int>();

    public GameObject mainCamera;
    public Transform canvas;
    public GameObject pref;
    public Text textCurrencyNames;
    public Text textcurrencyValues;


    private void Start()
    {
        
        currencyNames.Add("currency1");
        currencyNames.Add("currency2");
        currencyNames.Add("currency3");

        currencyValues.Add(100);
        currencyValues.Add(1000);
        currencyValues.Add(300);
        ChangeValues();
    }

    public void ChangeValues()
    {
        foreach (Transform prefInCanvas  in canvas)
        {
            Destroy(prefInCanvas.gameObject);
        }

        int count = 0;
        foreach (string currencyName in currencyNames)
        {
            textCurrencyNames.text = currencyName;
            textcurrencyValues.text = $"{currencyValues[count]}";
            CopyPref(pref, canvas);
            count++;
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
