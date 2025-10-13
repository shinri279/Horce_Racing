using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public Text countdownText;// UIのTextオブジェクト
    public float countdownTime = 10f;// カウントダウン時間
    public Text countUpText;// カウントアップ用のText
    public GoalTape goalTape;
    public static float elapsedTime = 5;//カウントダウン用のTime
    public bool countbool;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (countbool) 
        {
            elapsedTime -= Time.deltaTime;

            if (countdownText.text == "1")
            {
                countdownText.color = Color.red;
            }
            else if (countdownText.text == "2" || countdownText.text == "3")
            {
                countdownText.color = Color.yellow;
            }

        }
        else if(!countbool&&goalTape.GoalPlayer)
        {
            //elapsedTime += 0;
        }
        else
        {
            elapsedTime += Time.deltaTime;
        }
    }

    private IEnumerator CountdownCoroutine()
    {
        while (elapsedTime > 0)
        {
            countdownText.text = elapsedTime.ToString("0");
            yield return new WaitForSeconds(1f);
        }
        elapsedTime = 0f;
        countbool = false;
        yield return new WaitForSeconds(0f);
        countdownText.gameObject.SetActive(false);
        StartCoroutine(CountUpCoroutine());
    }

    private IEnumerator CountUpCoroutine()
    {
        countUpText.gameObject.SetActive(true);// カウントアップ用のTextを表示
        while (true)
        {
            countUpText.text = elapsedTime.ToString("F1");
            yield return new WaitForSeconds(0.1f);
        }
    }
}
