using UnityEngine;

// 바닥에 떨어진 아이템. 플레이어가 닿으면 인벤토리에 들어갑니다.
[RequireComponent(typeof(Collider2D))]
public class ItemPickup : MonoBehaviour
{
    public string itemName = ItemNames.Branch;
    public int quantity = 1;
    public float pickupDelay = 0.5f; // 생성 직후 바로 줍지 않도록 하는 대기 시간

    private float spawnTime;

    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
        spawnTime = Time.time;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time - spawnTime < pickupDelay || !collision.CompareTag("Player"))
            return;

        Inventory inventory = collision.GetComponent<Inventory>();
        if (inventory != null && inventory.AddItem(itemName, quantity))
            Destroy(gameObject);
    }
}
