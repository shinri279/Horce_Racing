using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float PlayerSideSpeed;
    public float PlayerForwardSpeed;
    public float PlayerNormalSpeed;
    public float PlayerSpeedUp;
    public float RotationSpeed;
    public Rigidbody rb;
    public float JumpPower = 0f;
    public bool JumpFlag = false;
    public float SpeedUpLapseTime;
    public bool SpeedUpAble;
    public  float Stamina = 600;
    public float DecelerationFactor = 2f;//Wall内にいると減速する値
    private bool isInBox = false; // Wall内にいるかどうか
    private bool startBool = false;
    private float guts = 0f;
    public float GutsTime = 0f;
    //private float staminaGrass = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Stamina = 600;
        GutsTime = 0f;
        Invoke("StartMoving", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        GutsTime += Time.deltaTime;
        if (startBool)
        {
            Move();
            //Rotation();
            Jump();
            SpeedUp();

            CurveRotation();
        }
        /*Move();
        Rotation();
        Jump();
        SpeedUp();*/
    }

    void StartMoving()
    {
        startBool = true;
    }

    void Move()
    {
        var speed = Vector3.zero;
        speed.z = PlayerForwardSpeed * Time.deltaTime;
        /*if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            speed.x = -PlayerSideSpeed;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            speed.x = PlayerSideSpeed;
        }*/
        transform.Translate(speed);
    }

    void Rotation()
    {
        var speed = Vector3.zero;

        float normalizedY = NormalizeAngle(transform.localEulerAngles.y);
       
        transform.eulerAngles -= speed;
    }

    float NormalizeAngle(float angle)
    {
        while (angle > 180) angle -= 360;
        while (angle < -180) angle += 360;
        return angle;
    }

    public float tiltAngle = 0f;
    public float tiltSpeed = 5;

    private Quaternion targetRotation;

    void CurveRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //targetRotation = Quaternion.Euler(0, -tiltAngle,0);
            tiltAngle--;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //targetRotation = Quaternion.Euler(0, tiltAngle, 0);
            tiltAngle++;
        }

        targetRotation = Quaternion.Euler(0, tiltAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
    }

    void SpeedUp()
    {
        if (SpeedUpAble == true)
        {
            //staminaGrass = 1f;
            if (Stamina > 15)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    /*if (isInBox)//馬が草に入ってスピードアップしたらスタミナを多く消費する
                    {
                        staminaGrass = 5f;
                    }*/
                    PlayerForwardSpeed += PlayerSpeedUp;
                    //RotationSpeed += 4f;
                    SpeedUpAble = false;
                }
            }

            if (Stamina < 600)
            {
                Stamina += 5f * Time.deltaTime;
            }
        }

        if (SpeedUpAble == false)
        {
            SpeedUpLapseTime += Time.deltaTime;

            if (SpeedUpLapseTime >= 0.5)
            {
                PlayerForwardSpeed = 5f;
                RotationSpeed = 4.9f;
                SpeedUpAble = true;
                SpeedUpLapseTime = 0;
            }

            if (Stamina > 0)
            {
                Stamina -= 30f * Time.deltaTime;
            }
        }

        guts += Time.deltaTime;
        if (Stamina < 15)
        {
            if (GutsTime > 90)
            {
                if (guts > 0.1f)
                {
                    // またはif(Input.GetKeyUp(KeyCode.KeypadEnter))
                    if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.UpArrow))
                    {
                        PlayerForwardSpeed += 5f;
                        if (guts > 0.2f)
                        {
                            guts = 0;
                            PlayerForwardSpeed = 5f;
                        }
                    }
                }
            }
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (JumpFlag == true)
            {

                rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
                JumpFlag = false;//これがないとチュートリアルで止まったときにjump力を貯められる
            }
        }
    }

    //地面についたとき
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            JumpFlag = true;
        }
    }

    //ジャンプしているとき
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            JumpFlag = false;
        }
    }

    // Triggerに入ったとき
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            isInBox = true;  // Wallエリアに入ったとき
            PlayerForwardSpeed -= DecelerationFactor;
        }
    }

    // Triggerから出たとき
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            isInBox = false;  // Wallエリアから出たとき
            PlayerForwardSpeed = PlayerNormalSpeed;
        }
    }
}
