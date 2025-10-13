using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialFight : MonoBehaviour
{
    public Text tutorialJumpText2;
    public Text tutorialFight;
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
            tutorialJumpText2.gameObject.SetActive(false);
            tutorialFight.gameObject.SetActive(true);
        }
    }
}
