using UnityEngine;

[CreateAssetMenu(fileName = "ItemDefinition", menuName = "Custom/ItemDefinition", order = 0)]

public class ItemDefinition : ScriptableObject
{
    [Header("Infos")]
    [SerializeField] private int ID;

    [Header("Icons")]
    [SerializeField] private Sprite ICON;

    public ItemDefinition(int id, int maxStack, Sprite icon, Sprite nearIcon, bool isPlacable)
    {
        ID = id;
        ICON = icon;
    }

    public int GetID
    {
        get { return ID; }
    }

    public Sprite GetIcon
    {
        get { return ICON; }
    }
}