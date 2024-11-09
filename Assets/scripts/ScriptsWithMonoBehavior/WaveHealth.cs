using UnityEngine;
using UnityEngine.UI;

public class WaveHealth : MonoBehaviour
{
    public Image LineBar;

    public void ConditionalDamage(float healthPercent)
    {
        LineBar.fillAmount = healthPercent / 100;
    }

}