using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private RectTransform minimapRectTransform; 
    [SerializeField] private List<Transform> horses;
    [SerializeField] private List<RectTransform> horseIcons;

    private float minimapWidth;
    private float minimapHeight;
    private float rotationSmoothTime = 0.1f; // 回転のスムーズ度
    public float PlusY;
    public float PlusX;

    [SerializeField] private float minX = -100f;
    [SerializeField] private float maxX = 100f;
    [SerializeField] private float minY = -100f;
    [SerializeField] private float maxY = 100f;

    void Start()
    {
        minimapWidth = minimapRectTransform.rect.width;
        minimapHeight = minimapRectTransform.rect.height;
    }

    void Update()
    {
        if (horses.Count == 0) return;

        Transform leadHorse = horses[0]; // 先頭馬
        float leadYRotation = leadHorse.eulerAngles.y; // 先頭馬の向き

        // 進行方向の回転をスムーズにするためのQuaternion
        Quaternion rotation = Quaternion.Euler(0, 0, leadYRotation); // ミニマップ用に回転反転

        for (int i = 0; i < horses.Count; i++)
        {
            if (i >= horseIcons.Count) continue;

            Vector3 relativePos = horses[i].position - leadHorse.position; // 先頭馬との相対位置
            if (CountdownTimer.elapsedTime==0)
            {
                PlusY = 0;
            }
            else if(CountdownTimer.elapsedTime < 5)
            {
                PlusY += 1*Time.deltaTime;
            }
            else if(CountdownTimer.elapsedTime >20&& CountdownTimer.elapsedTime < 27)
            {
                PlusX -= 1 * Time.deltaTime;
                PlusY -= 1 * Time.deltaTime;
            }
            else if (CountdownTimer.elapsedTime > 30 && CountdownTimer.elapsedTime < 33)
            {
                PlusX += 1f * Time.deltaTime;
                PlusY -= 1.8f * Time.deltaTime;
            }
            /*else if (CountdownTimer.elapsedTime > 57 && CountdownTimer.elapsedTime < 70)
            {
                PlusX -= 1f * Time.deltaTime;
                PlusY -= 1.5f * Time.deltaTime;
            }*/
            Vector2 minimapPos = new Vector2(-relativePos.z / 100f * minimapHeight + PlusX, relativePos.x / 20f * (minimapWidth / 2f) + PlusY);

            // 相対位置を回転
            Vector2 rotatedPos = rotation * minimapPos; // Quaternion適用

            // ★ 上限・下限を制限（Clamp）
            rotatedPos.x = Mathf.Clamp(rotatedPos.x, minX, maxX);
            rotatedPos.y = Mathf.Clamp(rotatedPos.y, minY, maxY);

            // ミニマップアイコンの位置を更新
            horseIcons[i].anchoredPosition = Vector2.Lerp(horseIcons[i].anchoredPosition, rotatedPos, rotationSmoothTime);
        }
    }
}
