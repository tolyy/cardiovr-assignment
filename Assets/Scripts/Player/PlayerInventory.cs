using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int coin = 0;
    public int itemIndex = 0;
    public List<GameObject> Inventory;
    public GameObject hand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void AddItem(GameObject item)
    {
        GameObject spawnedItem = Instantiate(item, hand.transform.position, hand.transform.rotation);
        spawnedItem.transform.SetParent(hand.transform);
        
        if (Inventory.Count > 0)
        {
            Inventory[itemIndex].SetActive(false);
        }
        
        Inventory.Add(spawnedItem);
        itemIndex = Inventory.Count - 1;
        Debug.Log("Item Picked Up " + item.name);

        UpdateItem();
    }

    public ItemBase GetItem()
    {
        if (Inventory.Count == 0)
        {
            return null;
        }

        ItemBase itemBase = Inventory[itemIndex].GetComponent<ItemBase>();
        return itemBase;
    }

    public void UpdateItem() 
    {    
        for (int i = 0; i < Inventory.Count; i++)
        {
            Debug.Log(i);
            Debug.Log("Item Index: " + itemIndex);
            
            if (i == itemIndex)
            {
                Inventory[i].GetComponent<ItemBase>().Equip(this.gameObject);
            }
            else
            {
                Inventory[i].GetComponent<ItemBase>().Unequip(this.gameObject);
            }

            Inventory[i].SetActive(i == itemIndex);
        }
    }

    public void nextItem()
    {
        if(Inventory.Count == 0)
        {
            return;
        }
        itemIndex = (itemIndex + 1) % Inventory.Count;
        Debug.Log(itemIndex);
        UpdateItem();
    }

    public void previousItem()
    {
        if(Inventory.Count == 0)
        {
            return;
        }
        itemIndex = (itemIndex - 1 + Inventory.Count) % Inventory.Count;
        Debug.Log(itemIndex);
        UpdateItem();
    }
}