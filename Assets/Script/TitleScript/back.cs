using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class back : MonoBehaviour
{
    private bool isTimerStarted = false;
    private bool ChangeScene = false;
    public Button stage1, stage2, stage3, backButton,tutorialButton;
    public Image title;
    public Text startText;
    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(OnButtonClicked);
    }

    // Update is called once per frame
    void OnButtonClicked()
    {
        stage1.gameObject.SetActive(false);
        stage2.gameObject.SetActive(false);
        stage3.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        tutorialButton.gameObject.SetActive(false);
        title.gameObject.SetActive(true);
        startText.gameObject.SetActive(true);
    }
}
