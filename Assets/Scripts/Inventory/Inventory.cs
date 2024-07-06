using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventorySize = 10;
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

    public void RemoveItem(int index)
    {
        tab[index].RemoveItem(1);
        Debug.Log(tab[index]);
    }

    // retourne l'exces d'item non ajoute
    public (ItemDefinition, int) AddItem(int index, ItemDefinition item, int quantity)
    {
        return (tab[index].GetItem, tab[index].AddItem(item, quantity).Item2);
    }

    // Ajoute un item à la première place possible
    public (ItemDefinition, int) AddItemFast(ItemDefinition item, int quantity)
    {
        foreach (Slot slot in tab)
        {
            if (slot.IsEmpty())
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

    public int GetItemIndex(int itemID)
    {
        int i = 0;
        foreach (Slot slot in tab)
        {
            if (slot.GetItem.GetID == itemID)
            {
                return i;
            }
            i++;
        }
        return -1;
    }

    public void SwitchItem(int index, int index2)
    {
        Slot tmpSlot = tab[index];
        tab[index] = tab[index2];
        tab[index2] = tmpSlot;
    }

    public int GetNumberOfItems()
    {
        int res = 0;
        foreach (Slot slot in tab)
        {
            if (slot.GetItem.GetID != 0)
            {
                res++;
            }
        }
        return res;
    }

    public void SortInventory()
    {
        for (int j = 0; j < inventorySize; j++)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                if (tab[i].GetItem.GetID == 0 && i + 1 < inventorySize)
                {
                    SwitchItem(i, i + 1);
                }
            }
        }
    }

    public ItemDefinition CheckItem(int index)
    {
        return tab[index].GetItem;
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