using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventorySize = 5;
    [SerializeField] private List<Slot> tab = new();
    private GameManager manager;

    void Start()
    {
        manager = GameManager.Instance;
        if (inventorySize == 0)
        {
            inventorySize = 1;
        }
        InitSlots();
    }

    public void InitSlots()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            tab.Add(new Slot(0, manager.ConvertIdToItem(0)));
        }
    }

    public (ItemDefinition, int) RemoveItem(int index, int quantity)
    {
        if (index < 0 || index >= inventorySize)
            throw new Exception("Index out of range");
        if (tab[index].GetItemQuantity < quantity)
            throw new Exception("Not enough item in the slot");

        return (tab[index].GetItem, tab[index].RemoveItem(quantity).Item2);
    }

    // retourne l'exces d'item non ajoute
    public (ItemDefinition, int) AddItem(int index, ItemDefinition item, int quantity)
    {
        if (index < 0 || index >= inventorySize)
            throw new Exception("Index out of range");
        if (tab[index].GetItemQuantity + quantity > item.GetMaxStack)
            throw new Exception("Too much item in the slot");

        return (tab[index].GetItem, tab[index].AddItem(item, quantity).Item2);
    }

    // Ajoute un item à la première place possible
    public (ItemDefinition, int) AddItemFast(ItemDefinition item, int quantity)
    {
        if (IsFullFor(item, quantity))
        {
            return (item, quantity);
        }
        foreach (Slot slot in tab)
        {
            if (slot.IsEmpty() || (slot.GetItem == item && slot.GetItemQuantity < item.GetMaxStack))
            {
                (ItemDefinition, int) reste = slot.AddItem(item, quantity);
                if (reste.Item2 <= 0)
                {
                    return reste;
                }
                AddItemFast(reste.Item1, reste.Item2);
            }
        }
        return (null, 0);
    }

    public bool IsFullFor(ItemDefinition item, int quantity)
    {
        int quantityRestant = quantity;
        foreach (Slot slot in tab)
        {
            if (slot.IsEmpty())
            {
                return false;
            }
            if (slot.GetItem == item)
            {
                if (item.GetMaxStack - slot.GetItemQuantity < quantityRestant)
                {
                    return false;
                }
                else
                {
                    quantityRestant -= item.GetMaxStack - slot.GetItemQuantity;
                }
            }
        }
        return true;
    }

    public void SwitchItem(int index, Slot slot)
    {
        if (tab[index].GetItem == slot.GetItem)
        {
            (ItemDefinition, int) restant = tab[index].AddItem(slot.GetItem, slot.GetItemQuantity);
            slot.RemoveItem(slot.GetItemQuantity);
            slot.AddItem(restant.Item1, restant.Item2);
            return;
        }
        tab[index].SwitchItems(slot);
    }


    public ItemDefinition CheckItem(int index)
    {
        if (index < 0 || index >= inventorySize)
        {
            throw new Exception("Index out of range" + index + ">" + inventorySize);
        }
        return tab[index].GetItem;
    }

    public int CheckItemQuantity(int index)
    {
        if (index < 0 || index >= inventorySize)
        {
            throw new Exception("Index out of range" + index + ">" + inventorySize);
        }
        return tab[index].GetItemQuantity;
    }

    public bool IsInInventory(int itemID)
    {
        foreach (Slot slot in tab)
        {
            if (slot.GetItem.GetID == itemID)
            {
                return true;
            }
        }
        return false;
    }

    public int GetInventorySize
    {
        get { return inventorySize; }
    }

    public Slot GetSlot(int index)
    {
        return tab[index];
    }

    public List<Slot> GetTab
    {
        get { return tab; }
    }
}