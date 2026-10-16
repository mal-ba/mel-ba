using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public string itemName;
    public int quantity;

    public InventoryItem(string name, int qty)
    {
        itemName = name;
        quantity = qty;
    }
}

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public int maxItems = 150;

    // 인벤토리 내용이 바뀔 때마다 호출 (UI 갱신 등에 사용)
    public event Action OnInventoryChanged;

    public int TotalCount
    {
        get
        {
            int total = 0;
            foreach (var item in items)
                total += item.quantity;
            return total;
        }
    }

    public bool AddItem(string itemName, int quantity)
    {
        if (string.IsNullOrEmpty(itemName) || quantity <= 0)
            return false;

        if (TotalCount + quantity > maxItems)
        {
            Debug.Log("인벤토리가 가득 찼습니다!");
            return false;
        }

        // 기존 아이템이 있는지 확인
        InventoryItem existingItem = FindItem(itemName);
        if (existingItem != null)
            existingItem.quantity += quantity;
        else
            items.Add(new InventoryItem(itemName, quantity));

        Debug.Log($"{itemName} {quantity}개 추가됨");
        OnInventoryChanged?.Invoke();
        return true;
    }

    public bool RemoveItem(string itemName, int quantity)
    {
        if (quantity <= 0)
            return false;

        InventoryItem existingItem = FindItem(itemName);

        if (existingItem != null && existingItem.quantity >= quantity)
        {
            existingItem.quantity -= quantity;

            if (existingItem.quantity == 0)
                items.Remove(existingItem);

            Debug.Log($"{itemName} {quantity}개 제거됨");
            OnInventoryChanged?.Invoke();
            return true;
        }

        Debug.Log($"{itemName}이(가) 부족합니다.");
        return false;
    }

    public bool HasItem(string itemName, int quantity)
    {
        return GetCount(itemName) >= quantity;
    }

    public int GetCount(string itemName)
    {
        InventoryItem existingItem = FindItem(itemName);
        return existingItem != null ? existingItem.quantity : 0;
    }

    private InventoryItem FindItem(string itemName)
    {
        return items.Find(item => item.itemName == itemName);
    }
}
