using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialFight2 : MonoBehaviour
{
    public Text tutorialFight;
    public Text tutorialFight2;
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
            tutorialFight.gameObject.SetActive(false);
            tutorialFight2.gameObject.SetActive(true);
        }
    }
}
