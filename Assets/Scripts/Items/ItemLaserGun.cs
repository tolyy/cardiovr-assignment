using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class ItemLaserGun : ItemBase
{
    public GameObject laserFirePoint;
    public float MaxDistance = 100f;
    public LayerMask layerMask;
    public float force = 10f;

    private LineRenderer lr;
    private bool bFiring = false;
    private bool isCooldown = false;

    private float laserDuration = 5f;
    private float laserCooldown = 3f;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
    }

    public override void Equip(GameObject callingPlayer)
    {
        base.Equip(callingPlayer);
    
        GetHUDBase().UpdateAmmoText((int)laserDuration);
    }
    
    public override void Unequip(GameObject callingPlayer)
    {
        base.Unequip(callingPlayer);
    }

    public override void Use()
    {
        // Call the base function
        base.Use();

        StartLaserFire();
    }

    void StartLaserFire()
    {
        if (!bFiring && !isCooldown)
        {
            bFiring = true;
            lr.enabled = true;
            StartCoroutine(ShootLaser());
        }
    }

    IEnumerator ShootLaser()
    {
        float elapsedTime = 0f;

        while (bFiring)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= laserDuration)
            {
                bFiring = false;
                lr.enabled = false;
                StartCoroutine(LaserFireCooldown());
            }
            else
            {
                LaserFire();
            }

            if (!isCooldown)
            {
                GetHUDBase().UpdateAmmoText((int)(laserDuration - elapsedTime) + 1); 
            }
            
            yield return null;
        }
    }

    void LaserFire()
    {
        Debug.Log("Laser Fired");
        RaycastHit hit;

        Vector3 startPoint = laserFirePoint.transform.position;
        Vector3 endPoint;

        if (Physics.Raycast(startPoint, laserFirePoint.transform.forward, out hit, MaxDistance, layerMask))
        {
            endPoint = hit.point;

            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 forceDirection = hit.point - laserFirePoint.transform.position;
                rb.AddForce(forceDirection.normalized * force, ForceMode.Impulse);
            }
            else
            {
                endPoint = laserFirePoint.transform.position + laserFirePoint.transform.forward * MaxDistance;
            }
        }
        else
        {
            endPoint = laserFirePoint.transform.position + laserFirePoint.transform.forward * MaxDistance;
        }

        lr.SetPosition(0, startPoint);
        lr.SetPosition(1, endPoint);

        StartCoroutine(UpdateBeamPoint());
    }

    IEnumerator UpdateBeamPoint()
    {
        while (bFiring)
        {
            yield return null;
            Vector3 startPoint = laserFirePoint.transform.position;
            Vector3 endPoint = startPoint + laserFirePoint.transform.forward * MaxDistance;
            lr.SetPosition(0, startPoint);
            lr.SetPosition(1, endPoint);
        }
    }

    HUDbase GetHUDBase()
    {
        return player.GetComponent<HUDbase>();
    }

    IEnumerator LaserFireCooldown()
    {
        GetHUDBase().UpdateAmmoTextString("OVERHEAT!");
        isCooldown = true;
        Debug.Log("Laser Cooldown Started");
        yield return new WaitForSeconds(laserCooldown);
        isCooldown = false;
        GetHUDBase().UpdateAmmoTextString("READY!");
    }

    public override void EndUse()
    {
        base.EndUse();
        StopLaserFire();
    }

    void StopLaserFire()
    {
        Debug.Log("Laser Stopped");
        StopCoroutine(ShootLaser());
        bFiring = false;
        lr.enabled = false;
    }
}