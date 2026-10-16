using UnityEngine;

public class Crafting : MonoBehaviour
{
    public Inventory inventory; // 인벤토리 참조
    public Hotbar hotbar; // 제작한 도구를 등록할 단축창 (선택)
    public KeyCode craftAxeKey = KeyCode.C; // 도끼 제작 키

    void Update()
    {
        if (Input.GetKeyDown(craftAxeKey))
            CraftAxe();
    }

    public bool CraftAxe()
    {
        if (inventory == null)
        {
            Debug.LogWarning("Crafting: inventory가 지정되지 않았습니다.");
            return false;
        }

        if (!inventory.HasItem(ItemNames.Branch, 1) || !inventory.HasItem(ItemNames.Stone, 1))
        {
            Debug.Log("재료가 부족합니다. (나뭇가지 1, 돌 1 필요)");
            return false;
        }

        inventory.RemoveItem(ItemNames.Branch, 1);
        inventory.RemoveItem(ItemNames.Stone, 1);

        // 재료를 빼서 생긴 공간에 도끼를 넣으므로 항상 성공
        inventory.AddItem(ItemNames.Axe, 1);
        Debug.Log("도끼 제작 완료!");

        if (hotbar != null)
            hotbar.AddItemToHotbar(ItemNames.Axe);

        return true;
    }
}
