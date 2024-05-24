using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    bool isBeingUsed = false;
    protected GameObject player;

    public virtual void Equip(GameObject callingPlayer)
    {
        player = callingPlayer;
        Debug.Log("Item is equipped by " + player.name);   
    }

    public virtual void Unequip(GameObject callingPlayer)
    {
        player = null;
        Debug.Log("Item is unequipped");
    }

    public virtual void Use()
    {
        if (!isBeingUsed)
        {
            isBeingUsed = true;
            Debug.Log("Item is being used");
        }
    }

    public virtual void EndUse()
    {
        if (isBeingUsed)
        {
            isBeingUsed = false;
            Debug.Log("Item is not being used");
        }
    }
}