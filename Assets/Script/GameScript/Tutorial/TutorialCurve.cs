using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCurve : MonoBehaviour
{
    public Text tutorialJumpText;
    public Text tutorialCurveText;
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
            tutorialJumpText.gameObject.SetActive(false);
            tutorialCurveText.gameObject.SetActive(true);
        }
    }
}
