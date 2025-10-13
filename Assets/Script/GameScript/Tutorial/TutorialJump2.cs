using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialJump2 : MonoBehaviour
{
    public Text tutorialCurveText;
    public Text tutorialJumpText2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tutorialCurveText.gameObject.SetActive(false);
            tutorialJumpText2.gameObject.SetActive(true);
        }
    }
}
