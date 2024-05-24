using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCoin : InteractableBase
{
    void Awake()
    {
        interactableType = InteractableType.collectable;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public override void Interact(GameObject interactingPlayer)
    {
        // Bunu inventory scriptiyle degistir
        PlayerInventory playerInventory = interactingPlayer.GetComponent<PlayerInventory>();
        if (playerInventory){
            playerInventory.coin += 1;
            interactingPlayer.GetComponent<HUDbase>().UpdateCoinText(playerInventory.coin);
        }

        base.Interact(interactingPlayer);
    }
}