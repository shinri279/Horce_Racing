using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCountdownTimer : MonoBehaviour
{
    public Text countdownText;// UIのTextオブジェクト
    public float countdownTime = 10f;// カウントダウン時間
    public Text countUpText;// カウントアップ用のText
    public Text TutorialText1;
    public Text TutorialText2;
    public Text TutorialSideMoveText;
    public Text TutorialSpeedupText;
    public bool tutorialSideKey = true;
    public bool tutorialSpeedupKey = true;
    public bool pressed = false;
    public GoalTape goalTape;
    public static float elapsedTime = 5;//カウントダウン用のTime
    public bool countbool;
    public GameObject tutorialPanel;

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

            /*if (elapsedTime < 0.9f)
            {
                countdownText.color = Color.red;
            }*/

            if (countdownText.text =="1")
            {
                countdownText.color = Color.red;
            }
            else if(countdownText.text=="2"||countdownText.text=="3")
            {
                countdownText.color = Color.yellow;
            }
        }
        else if (!countbool&goalTape.GoalPlayer)
        {

        }
        else
        {
            elapsedTime += Time.deltaTime;
        }

        if (elapsedTime > 2&&elapsedTime<3&&!tutorialSideKey)
        {
            Time.timeScale = 0f; // ゲーム一時停止
            TutorialSideMoveText.gameObject.SetActive(true);
            tutorialPanel.SetActive(true);
            TutorialText1.gameObject.SetActive(true);
            tutorialSideKey = true;
        }
        else if(elapsedTime>5&&elapsedTime<6&&!tutorialSpeedupKey)
        {
            Time.timeScale = 0f; // ゲーム一時停止
            TutorialSideMoveText.gameObject.SetActive(false);
            tutorialPanel.SetActive(true);
            TutorialSpeedupText.gameObject.SetActive(true);
            tutorialSideKey = false;
            tutorialSpeedupKey = true;
        }

        if (tutorialSideKey)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.D))
                pressed = true;
            if (pressed)
            {
                Time.timeScale = 1f;
                tutorialPanel.SetActive(false);
            }
            pressed = false;
        }

        if (tutorialSpeedupKey)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Return)||Input.GetKeyDown(KeyCode.W))
                pressed = true;
            if (pressed)
            {
                Time.timeScale = 1f;
                tutorialPanel.SetActive(false);
            }
            pressed = false;
        }
    }

    private IEnumerator CountdownCoroutine()
    {
        while (elapsedTime > 0f)
        {
            countdownText.text = elapsedTime.ToString("0");
            yield return new WaitForSeconds(1f);
        }
        elapsedTime = 0f;
        countbool = false;
        yield return new WaitForSeconds(0f);
        countdownText.gameObject.SetActive(false);
        TutorialText1.gameObject.SetActive(false);
        TutorialText2.gameObject.SetActive(false);

        StartCoroutine(CountUpCoroutine());
        tutorialSideKey = false;
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
