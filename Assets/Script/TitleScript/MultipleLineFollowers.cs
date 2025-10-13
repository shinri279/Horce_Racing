using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MultipleLineFollowers : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public List<Transform> objects; // 移動させるオブジェクトのリスト
    public float speed = 5f;
    public float spacing = 1.0f; // オブジェクト間の距離（ワールド空間での距離）

    private List<float> distances; // 各オブジェクトの進行距離
    private Vector3[] points;
    private float totalLength;

    void Start()
    {
        if (lineRenderer == null || objects.Count == 0)
        {
            Debug.LogError("LineRendererまたはオブジェクトが設定されていません！");
            return;
        }

        // LineRendererの頂点座標を取得
        int pointCount = lineRenderer.positionCount;
        points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            points[i] = lineRenderer.transform.TransformPoint(lineRenderer.GetPosition(i)); // ローカル→ワールド座標
        }

        // 線の全長を計算
        totalLength = CalculateTotalLength();

        // 各オブジェクトの初期位置を設定（一定間隔で開始）
        distances = new List<float>();
        for (int i = 0; i < objects.Count; i++)
        {
            distances.Add(i * spacing);
        }
    }

    void Update()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            distances[i] += speed * Time.deltaTime; // 各オブジェクトの進行距離を更新

            // ループ処理（最後まで行ったら最初に戻る）
            if (distances[i] >= totalLength)
            {
                distances[i] -= totalLength;
            }

            // 新しい位置を計算して設定
            Vector3 newPos = GetPositionOnLine(distances[i]);
            objects[i].position = newPos;

            // 進行方向を求める（少し先の位置を取得）
            Vector3 nextPos = GetPositionOnLine(distances[i] + 0.1f);
            Vector3 direction = (nextPos - newPos).normalized;

            // 向きを進行方向に合わせる
            if (direction != Vector3.zero) // ゼロ除算防止
            {
                Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
                objects[i].rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y-90f, 0); // Z軸を0に固定
            }
        }
    }

    // 線の全長を計算する関数
    private float CalculateTotalLength()
    {
        float length = 0f;
        for (int i = 1; i < points.Length; i++)
        {
            length += Vector3.Distance(points[i - 1], points[i]);
        }
        return length;
    }

    // 指定された距離の位置を線上で求める関数
    private Vector3 GetPositionOnLine(float distance)
    {
        float traveled = 0f;

        for (int i = 1; i < points.Length; i++)
        {
            float segmentLength = Vector3.Distance(points[i - 1], points[i]);

            if (traveled + segmentLength >= distance)
            {
                float ratio = (distance - traveled) / segmentLength;
                return Vector3.Lerp(points[i - 1], points[i], ratio);
            }

            traveled += segmentLength;
        }

        return points[points.Length - 1]; // 最後のポイント
    }
}
