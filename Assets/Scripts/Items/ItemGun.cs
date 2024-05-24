using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGun : ItemBase
{
    public Rigidbody bullet;
    float bulletSpeed = 20f;
    public GameObject firePoint;
    public float bulletLifeTime = 2f;
    int ammoAmount = 6;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public override void Equip(GameObject callingPlayer)
    {
        base.Equip(callingPlayer);
    
        UpdateAmmoText();
    }
    
    public override void Unequip(GameObject callingPlayer)
    {
        base.Unequip(callingPlayer);
    }

    void UpdateAmmoText()
    {
        HUDbase hud = player.GetComponent<HUDbase>();
        if (hud != null)
        {
            hud.UpdateAmmoText(ammoAmount);
        }
    }

    public override void Use()
    {
        base.Use();

        if (ammoAmount > 0)
        {
            Fire();
        }
        else
        {
            Debug.Log("outta ammo");
        }

        UpdateAmmoText();
    }

    void Fire()
    {
        Rigidbody b = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation).GetComponent<Rigidbody>();
        b.velocity = firePoint.transform.forward * bulletSpeed;

        Destroy(b.gameObject, bulletLifeTime);
        ammoAmount--;
        
        Debug.Log("Ammo: " + ammoAmount);
    }

    void Reload()
    {
        ammoAmount = 6;

        UpdateAmmoText();
        Debug.Log("Reloaded");
    }

    public override void EndUse()
    {
        base.EndUse();
    }
}