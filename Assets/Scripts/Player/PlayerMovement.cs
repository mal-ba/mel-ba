using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 캐릭터 이동 속도

    private Rigidbody2D rb;
    private Vector2 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;      // 탑다운 2D: 중력 없음
        rb.freezeRotation = true;  // 충돌 시 회전 방지
    }

    void Update()
    {
        // 입력 처리 (WASD 또는 방향키)
        movement.x = Input.GetAxisRaw("Horizontal"); // 좌우 입력
        movement.y = Input.GetAxisRaw("Vertical");   // 상하 입력

        // 대각선 이동 시 속도가 빨라지지 않도록 정규화
        movement = Vector2.ClampMagnitude(movement, 1f);
    }

    void FixedUpdate()
    {
        // 물리 이동 처리
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
