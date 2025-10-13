using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    public string nextSceneName = "Name"; // ëJà⁄êÊÇÃÉVÅ[ÉìñºÇéwíË
    private bool isTimerStarted = false;
    private bool ChangeScene = false;
    public Button stage1, stage2, stage3, backButton,tutorialButton;
    public Image title;
    public Text startText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimerStarted)
        {
            isTimerStarted = true;
            StartCoroutine(WaitForTransition());
        }

        if (ChangeScene&&Input.GetKeyDown(KeyCode.Space)|| Input.GetMouseButtonDown(0))
        {
            //SceneManager.LoadScene(nextSceneName);
            stage1.gameObject.SetActive(true);
            stage2.gameObject.SetActive(true);
            stage3.gameObject.SetActive(true);
            backButton.gameObject.SetActive(true);
            tutorialButton.gameObject.SetActive(true);
            title.gameObject.SetActive(false);
            startText.gameObject.SetActive(false);
        }
    }

    IEnumerator WaitForTransition()
    {
        yield return new WaitForSeconds(2f);
        ChangeScene = true;
    }
}
