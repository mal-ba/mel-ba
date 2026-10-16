using UnityEngine;

public class Hotbar : MonoBehaviour
{
    public const int MaxSlotLimit = 7; // 단축창 최대 슬롯 수

    public Inventory inventory; // 아이템 개수를 확인/소모할 인벤토리
    public string[] hotbarSlots = new string[MaxSlotLimit]; // 단축창 슬롯
    [SerializeField] private int maxSlots = 3; // 기본적으로 3개의 슬롯만 활성화됨

    public int ActiveSlots => maxSlots;

    void Awake()
    {
        // Inspector에서 배열 크기를 잘못 지정해도 오류가 나지 않도록 보정
        if (hotbarSlots == null || hotbarSlots.Length != MaxSlotLimit)
            System.Array.Resize(ref hotbarSlots, MaxSlotLimit);

        maxSlots = Mathf.Clamp(maxSlots, 1, MaxSlotLimit);
    }

    void Update()
    {
        // 숫자키 1~7로 슬롯 사용
        for (int i = 0; i < maxSlots; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                UseItemFromHotbar(i);
        }
    }

    public bool AddItemToHotbar(string itemName)
    {
        if (string.IsNullOrEmpty(itemName))
            return false;

        // 이미 등록된 아이템이면 중복 등록하지 않음
        for (int i = 0; i < maxSlots; i++)
        {
            if (hotbarSlots[i] == itemName)
                return true;
        }

        for (int i = 0; i < maxSlots; i++)
        {
            if (string.IsNullOrEmpty(hotbarSlots[i]))
            {
                hotbarSlots[i] = itemName;
                Debug.Log($"{itemName}이(가) 단축창 {i + 1}번 슬롯에 추가되었습니다.");
                return true;
            }
        }

        Debug.Log("단축창이 가득 찼습니다!");
        return false;
    }

    public void UseItemFromHotbar(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= maxSlots || string.IsNullOrEmpty(hotbarSlots[slotIndex]))
        {
            Debug.Log("해당 슬롯에 아이템이 없습니다.");
            return;
        }

        string itemName = hotbarSlots[slotIndex];

        // 인벤토리가 연결되어 있으면 실제 아이템을 소모
        if (inventory != null && !inventory.RemoveItem(itemName, 1))
        {
            hotbarSlots[slotIndex] = null;
            return;
        }

        Debug.Log($"{itemName} 아이템을 사용했습니다.");

        // 남은 아이템이 없으면 슬롯 비우기
        if (inventory == null || !inventory.HasItem(itemName, 1))
            hotbarSlots[slotIndex] = null;
    }

    public bool IncreaseHotbarSlots()
    {
        if (maxSlots < MaxSlotLimit)
        {
            maxSlots++;
            Debug.Log($"단축창 슬롯이 {maxSlots}개로 증가했습니다!");
            return true;
        }

        Debug.Log($"단축창 슬롯은 최대 {MaxSlotLimit}개까지 가능합니다.");
        return false;
    }
}
