using System;

public enum ItemId
{
    FireWood = 1,
    SkullGem = 2
}

[Serializable]
public struct ItemData
{
    public ItemId Id;
    public int Count;
    public bool RemoveOnUse;
}