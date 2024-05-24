using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGun : InteractableBase
{
    void Awake()
    {
        interactableType = InteractableType.pickup;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact(GameObject interactingPlayer)
    {
        PlayerInventory playerInventory =  interactingPlayer.GetComponent<PlayerInventory>();

        if (playerInventory && itemToEquip != null)
        {
            playerInventory.AddItem(itemToEquip);
            Debug.Log("Gun picked up!");
        }
        
        base.Interact(interactingPlayer);  
    }
}
