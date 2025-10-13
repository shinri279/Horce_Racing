using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    public List<Transform> horses;// 競走馬リスト
    public List<Transform> wayPoints;// レーストラックの経路（Waypoint）
    //public Transform goal; // ゴールのTransform
    public Transform playerHorce;
    public Text rankText;// 順位表示用のText（UI）

    private Dictionary<Transform, float> horseProgress = new Dictionary<Transform, float>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 各馬のゴールまでの距離を更新
        horseProgress.Clear();
        foreach(var horse in horses)
        {
            float progress = CalculateHorseProgress(horse);
            horseProgress[horse] = progress;
        }

        // 順位計算
        var sortedHorses = horseProgress.OrderByDescending(h => h.Value).ToList();

        // プレイヤーの順位を取得
        int playerRank = sortedHorses.FindIndex(h => h.Key == playerHorce) + 1;

        // UIのTextに表示
        if (rankText != null)
        {
            rankText.text = ""+playerRank;
        }

        // 順位を設定
        for(int i = 0; i < sortedHorses.Count; i++)
        {
            Debug.Log($"{sortedHorses[i].Key.name} の順位: {i + 1}");
        }
    }

    private float CalculateHorseProgress(Transform horce)
    {
        float totalProgress = 0f;
        float totalDistance = 0f;

        for(int i = 0; i < wayPoints.Count - 1; i++)
        {
            totalDistance += Vector3.Distance(wayPoints[i].position, wayPoints[i + 1].position);
        }

        // 現在のWaypointを探す
        for(int i = 0; i < wayPoints.Count - 1; i++)
        {
            float segmentLength = Vector3.Distance(wayPoints[i].position, wayPoints[i + 1].position);
            float distToNext = Vector3.Distance(horce.position, wayPoints[i + 1].position);
            float distToPrev = Vector3.Distance(horce.position, wayPoints[i].position);

            if (distToPrev < segmentLength && distToNext < segmentLength)
            {
                float segmentProgress = 1f - (distToNext / segmentLength);
                totalProgress += (i + segmentProgress) / wayPoints.Count;
                break;
            }
        }
        return totalProgress;
    }
}
