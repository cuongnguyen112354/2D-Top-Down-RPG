using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;

    // List các trạng thái của Enemy
    private enum State
    {
        Roaming
    }

    // Khai báo biến state để lưu trạng thái hiện tại của Enemy
    private State state;
    private EnemyPathFinding pathFinding;

    // Set up trạng thái ban đầu cho Enemy
    void Awake()
    {
        pathFinding = GetComponent<EnemyPathFinding>();
        state = State.Roaming;
    }

    // Hàm này sẽ được gọi khi Enemy bắt đầu chạy
    void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    // Hàm này sẽ chạy vô hạn để di chuyển Enemy
    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            pathFinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(roamChangeDirFloat);
        }
    }

    // Lấy vị trí ngẫu nhiên để di chuyển
    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
