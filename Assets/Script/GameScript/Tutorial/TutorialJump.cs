using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialJump : MonoBehaviour
{
    public TutorialCountdownTimer tutorialCountdownTimer;
    public Text tutorialJumpText;
    public Text TutorialSpeedupText;
    public bool jumpTutorial = false;
    public GameObject tutorialPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpTutorial&&Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            tutorialPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            tutorialPanel.SetActive(true);
            tutorialJumpText.gameObject.SetActive(true);
            TutorialSpeedupText.gameObject.SetActive(false);
            tutorialCountdownTimer.tutorialSpeedupKey = false;
            jumpTutorial = true;
        }
    }


}
