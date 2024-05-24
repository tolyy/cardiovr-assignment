using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType
{
    pickup,
    collectable
}   

public class InteractableBase : MonoBehaviour
{
    // interaction type
    public InteractableType interactableType;
    // related item to equip
    public GameObject itemToEquip;
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interact(GameObject interactingPlayer){        
        // Destroy the object
        Destroy(this.gameObject);
    }
}
