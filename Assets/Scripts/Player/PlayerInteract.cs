using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // Bunu burdan cikar inventoryscriptine koy
    public int coin = 0;
    private PlayerInventory playerInventory;

    void Awake()
    {
        playerInventory = this.GetComponent<PlayerInventory>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // On Left Mouse Button Dowm
        if (Input.GetMouseButtonDown(0))
        {
            ItemBase itemBase = playerInventory.GetItem();
            if (itemBase != null)
            {
                itemBase.Use();
            }
        }

        // On Left Mouse Button Release
        if (Input.GetMouseButtonUp(0))
        {
            ItemBase itemBase = playerInventory.GetItem();
            if (itemBase != null)
            {
                itemBase.EndUse();
            }
        }

        // Scroll Wheel Up
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            playerInventory.nextItem();
        }

        // Scroll Wheel Down
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            playerInventory.previousItem();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Interactable"))
        {
            InteractableBase interactableBase = other.gameObject.GetComponent<InteractableBase>();
            if (interactableBase != null)
            {
                interactableBase.Interact(this.gameObject);
            }
        }
    }
}
