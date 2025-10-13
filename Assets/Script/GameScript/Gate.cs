using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private float DelaySeconds = 8f;// âΩïbå„Ç…è¡ñ≈Ç∑ÇÈÇ©
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowAfterDelay()
    {
        yield return new WaitForSeconds(DelaySeconds);
        gameObject.SetActive(false);
    }
}
