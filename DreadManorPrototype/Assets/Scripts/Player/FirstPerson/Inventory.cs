using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<ItemId, ItemData> _items = new ();

    public void AddItem(ItemData item)
    {
        if (_items.TryGetValue(item.Id, out ItemData data))
        {
            data.Count += item.Count;
            _items[item.Id] = item;
        }
        else
        {
            _items.Add(item.Id, item);
        }
    }

    public bool UseItem(ItemId id)
    {
        if (_items.TryGetValue(id, out ItemData data))
        {
            if (data.RemoveOnUse)
                data.Count--;

            if (data.Count <= 0)
                _items.Remove(id);

            return true;
        }
        return false;
    }
}
