using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoBar : MonoBehaviour
{

    public Slider slider;
    public TextMeshProUGUI ammoText;

    public void SetMaxAmmo(int ammo)
    {
        slider.maxValue = ammo;
        slider.value = ammo;
        ammoText.text = ammo.ToString();
    }

    public void SetAmmo(int ammo)
    {
        slider.value = ammo;
        ammoText.text = ammo.ToString();
    }
}
