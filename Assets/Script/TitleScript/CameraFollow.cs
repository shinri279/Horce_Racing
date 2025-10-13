using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    public Transform[] targets;                 // 追従するターゲット（オブジェクト）
    public float distance = 10f;                // ターゲットからカメラまでの距離
    public float height = 5f;                   // カメラの高さ
    public float switchInterval = 5f;           // ターゲットを切り替える間隔（秒）
    public float transitionSpeed = 0.5f;        // ターゲット間のカメラ移動スピード（スムージング）
    public Text targetNameText;                 // UIのTextコンポーネント

    private int currentTargetIndex = 0;         // 現在のターゲットのインデックス
    private Transform currentTarget;            // 現在のターゲット
    private Vector3 velocity = Vector3.zero;    // カメラの移動速度を管理するための変数

    private void Start()
    {
        // 初期ターゲットを設定
        currentTarget = targets[currentTargetIndex];
        UpdateTargetName();
        // ターゲット切り替えのコルーチンを開始
        StartCoroutine(SwitchTarget());
    }

    private void LateUpdate()
    {
        // ターゲットの位置を元にカメラの位置を決定
        Vector3 desiredPosition = currentTarget.position - currentTarget.forward * distance + Vector3.up * height;

        // 以前のターゲットから新しいターゲットへの移動を滑らかに行う（カメラ移動のスムージング）
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, transitionSpeed);


        // カメラがターゲットを向くようにする
        transform.LookAt(currentTarget);
    }

    private IEnumerator SwitchTarget()
    {
        // 無限ループでターゲットを切り替える
        while (true)
        {
            // 次のターゲットに切り替え
            currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
            currentTarget = targets[currentTargetIndex];
            UpdateTargetName();

            // 次のターゲットに切り替えるまで待機
            yield return new WaitForSeconds(switchInterval);
        }
    }

    private void UpdateTargetName()
    {
        if (targetNameText != null)
        {
            targetNameText.text = currentTarget.name;
        }
    }
}
