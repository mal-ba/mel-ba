using UnityEngine;

// 플레이어가 닿으면 아이템을 떨어뜨리는 자원 오브젝트 (나무 → 나뭇가지, 바위 → 돌 등)
public class ResourceNode : MonoBehaviour
{
    public GameObject dropPrefab; // 떨어질 아이템 프리팹 (ItemPickup 포함)
    public int minDrops = 1;
    public int maxDrops = 3; // 최대 떨어질 개수
    public float respawnTime = 30f; // 다시 채집 가능해지기까지의 시간 (0 이하면 한 번만)

    private float nextAvailableTime = 0f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && CanHarvest())
            Harvest();
    }

    bool CanHarvest()
    {
        return Time.time >= nextAvailableTime;
    }

    void Harvest()
    {
        if (dropPrefab == null)
        {
            Debug.LogWarning($"{name}: dropPrefab이 지정되지 않았습니다.");
            return;
        }

        nextAvailableTime = respawnTime > 0f ? Time.time + respawnTime : float.PositiveInfinity;

        int dropCount = Random.Range(minDrops, maxDrops + 1);
        for (int i = 0; i < dropCount; i++)
        {
            Vector3 dropPosition = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), -1f, 0);
            Instantiate(dropPrefab, dropPosition, Quaternion.identity);
        }
    }
}
