using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDbase : MonoBehaviour
{    
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI ammoText;

    public void UpdateCoinText(int coin)
    {
        coinText.text = "Coins: " + coin;
    }

    public void UpdateAmmoText(int ammo)
    {
        ammoText.text = "Ammo: " + ammo;
    }

    public void UpdateAmmoTextString(string ammo)
    {
        ammoText.text = ammo;
    }
}
