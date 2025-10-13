using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorial : MonoBehaviour
{
    public float PlayerSideSpeed;
    public float PlayerForwardSpeed;
    public float PlayerNormalSpeed;
    public Rigidbody rb;
    public float JumpPower = 0f;
    public bool JumpFlag = false;
    public float DecelerationFactor = 2f;
    private bool isInBox = false;
    private bool startBool = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartMoving", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (startBool)
        {
            Move();
        }
    }

    void StartMoving()
    {
        startBool = true;
    }

    void Move()
    {
        var speed = Vector3.zero;
        speed.z = PlayerForwardSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            speed.x = -PlayerSideSpeed;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            speed.x = PlayerSideSpeed;
        }
        transform.Translate(speed);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (JumpFlag == true)
            {
                rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            JumpFlag = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            isInBox = true;
            PlayerForwardSpeed -= DecelerationFactor;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            isInBox = false;
            PlayerForwardSpeed = PlayerNormalSpeed;
        }
    }
}
