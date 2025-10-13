using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoalTape : MonoBehaviour
{
    private List<string> goalOrderNames = new List<string>();// ゴール順を記録するリスト
    private List<string> goalOrderIDs = new List<string>();//ゴール順のIDリスト

    public TextMesh goalRankName;// ゴール順を表示するためのUI（インスペクタで設定）
    public TextMesh goalRankName2;// ゴール順を表示するためのUI（インスペクタで設定）
    public TextMesh goalRank; // ゴールしたキャラの番号（ID）表示用
    public Text tutorialFight2;//fightのテキスト消す
    public Text tutorialGoalText;//ゴールした時のナレーション
    public Image announcer;//アナウンサーの画像
    public SpeedUpSlider speedUpSlider;// SpeedUpSlider の参照

    public Camera mainCamera; // メインカメラ（インスペクタで設定）
    public GameObject miniCamera;//ミニカメラのUI
    public Camera goalCamera;// 切り替えるゴールカメラ（インスペクタで設定）

    public float delaySeconds = 3f;// 何秒後に表示するか
    private Renderer objRenderer;
    private Collider objCollider;
    public GameObject titleScene;
    private bool isTimerTitled = false;
    private bool isReadyToChangeScene = false;
    public bool GoalPlayer = false;
    public float horseNum;

    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name; ;

        objRenderer = GetComponent<Renderer>();
        objCollider = GetComponent<Collider>();
        objRenderer.enabled = false;// 最初は見えないようにする
        objCollider.enabled = false;
        titleScene.SetActive(false);
        // 指定時間後に表示する
        StartCoroutine(ShowAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (goalOrderNames.Count >= horseNum&&!isTimerTitled)
        {
            isTimerTitled = true;
            StartCoroutine(WaitForTransition());
        }

        if (isReadyToChangeScene&&(Input.GetKeyDown(KeyCode.Space)|| Input.GetMouseButtonDown(0)))
        {
            SceneManager.LoadScene("Title");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterInfo CharacterInfo = other.GetComponent<CharacterInfo>();

        if (!goalOrderNames.Contains(other.name))
        {
            /// 順番を記録
            goalOrderNames.Add(other.name);
            goalOrderIDs.Add(CharacterInfo.CharacterID.ToString());
            // ゴール順を更新
            UpdateGoalText();
            if (other.gameObject.CompareTag("Player"))
            {
                speedUpSlider.HideSlider();
                GoalPlayer = true;
                Text();
                //if (goalOrderNames.Count == 1)
                //{
                //cameraMoved = true;
                Invoke("SwitchCamera", 5f);
                //}
            }
        }
    }

    void UpdateGoalText()
    {
        if (goalRankName != null)
        {
            goalRankName.text = "\n";
            goalRankName2.text = "\n";
            goalRank.text = "\n";
            for(int i = 0; i < goalOrderNames.Count-6; i++)
            {
                goalRankName.text += goalOrderNames[i] + "\n";
                //goalRank.text += goalOrderIDs[i] + "\n";
            }

            for (int j = 6; j < goalOrderNames.Count; j++)
            {
                goalRankName2.text += goalOrderNames[j] + "\n";
                //goalRank.text += goalOrderIDs[i] + "\n";
            }

            for (int l = 0; l < goalOrderNames.Count-7; l++)
            {
                goalRank.text += goalOrderIDs[l] + "\n";
            }
        }
    }

    void SwitchCamera()
    {
        if (mainCamera!=null){
            Debug.Log("123");
            mainCamera.gameObject.SetActive(false);
            //miniCamera.gameObject.SetActive(false);
            goalCamera.gameObject.SetActive(true);
            DeleteText();
        }
    }

    IEnumerator ShowAfterDelay()
    {
        yield return new WaitForSeconds(delaySeconds);
        objRenderer.enabled = true;
        objCollider.enabled = true;
    }

    //タイトルへの表示開始
    IEnumerator WaitForTransition()
    {
        yield return new WaitForSeconds(7f);
        titleScene.SetActive(true);
        isReadyToChangeScene = true;
    }

    void Text()
    {
        if (sceneName == "Tutorial")
        {
            tutorialFight2.gameObject.SetActive(false);
            tutorialGoalText.gameObject.SetActive(true);
        }
    }

    void DeleteText()
    {
        if (sceneName == "Tutorial")
        {
            announcer.gameObject.SetActive(false);
            tutorialGoalText.gameObject.SetActive(false);
        }
    }
}
