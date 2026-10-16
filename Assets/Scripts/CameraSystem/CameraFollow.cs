using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 따라갈 대상 (캐릭터)
    public float smoothing = 5f; // 카메라 이동 부드러움

    private Vector3 offset;

    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("CameraFollow: target이 지정되지 않았습니다.");
            enabled = false;
            return;
        }

        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}
