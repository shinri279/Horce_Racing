using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
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
    public static float Stamina = 600;
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
            Rotation();
            Jump();
            SpeedUp();
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
        if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
        {
            speed.x = -PlayerSideSpeed;
        }

        if (Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
        {
            speed.x = PlayerSideSpeed;
        }
        transform.Translate(speed);
    }

    public float rot;

    void Rotation()
    {
        float y = transform.localEulerAngles.y;
        float targetY = 0f;

        //曲がるエリアに応じて角度を決める
        if (transform.position.z > 50f && transform.position.x > 0)
        {
            targetY = (-(9f / 6f) * transform.position.z) + 75f;//zの位置で計算している
        }
        else if (transform.position.z > 50f && transform.position.x <= 0)
        {
            targetY = ((9f / 6f) * transform.position.x) - 90f;//xの位置で計算している
        }
        else if (transform.position.z>100f)
        {
            targetY = -90f;
        }
        else
        {
            // 直線部分
            targetY = (transform.position.x > 0) ? 0f : 180f;
        }

        // 現在の角度yと目標角度targetYの間を滑らかに補間する
        float smoothY = Mathf.LerpAngle(y, targetY, 1f * Time.deltaTime);

        // 直接角度を設定
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, smoothY, transform.localEulerAngles.z);



        /*修正版2
        float y = transform.localEulerAngles.y;// NormalizeAngleは不要
        float targetY = 0f; // 目標角度

        // z > 40 または z < -40 のときだけ回転
        if (transform.position.z > 40)
        {
            targetY = -179f;
        }
        else if (transform.position.z< -40)
        {
            targetY = 1f;
        }
        else
        {
            // 直線部分では角度固定
            if (transform.position.x > 0)
                targetY = 0f;
            else if (transform.position.x < 0)
                targetY = 180f;
        }

        RotationSpeed = (180f/(transform.position.x));

        // 現在角度から目標角度へスムーズに回転
        float newY = Mathf.MoveTowardsAngle(y, targetY, RotationSpeed * Time.deltaTime);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, newY, transform.localEulerAngles.z);
        */

        /*float y = NormalizeAngle(transform.localEulerAngles.y);修正版１

        // 曲がるエリア（例：z > 40 または z < -40）のとき、180度だけ回転
        if (transform.position.z > 40 || transform.position.z < -40)
        {
            // 進行方向に応じてターン方向を決定
            //float targetY = (transform.position.x > 0) ? 180f : 0f;
            float targetY = 180f; // 目標角度
            // 現在角度から目標角度まで徐々に回転
            float newY = y - RotationSpeed * Time.deltaTime;
            if (newY > targetY) newY = targetY;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, newY, transform.localEulerAngles.z);

        }
        else
        {
            // 直線部分では角度を固定（0° or 180°）
            if (transform.position.x > 0)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0f, transform.localEulerAngles.z);
            }else if (transform.position.x < 0)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180f, transform.localEulerAngles.z);
            }
        }*/



        /*var speed = Vector3.zero;

        float normalizedY = NormalizeAngle(transform.localEulerAngles.y);
        //曲がる時に自動で曲がる
        if (transform.position.z > 40||transform.position.z<-40)
        {
            speed.y = RotationSpeed * Time.deltaTime;
            //rot = -((-9f / 5f) * transform.position.x + 90f);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, normalizedY, transform.localEulerAngles.z);
            //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rot, transform.localEulerAngles.z);
        }*/
        /*10/6修正します
        //曲がり切れなかったとき、回転の修正 前のレーンと後ろのレーンの時
        if(-40< transform.position.z && transform.position.z < 40&&transform.localEulerAngles.y!=0)
        {

            //xが+の時、最初の直線
            if (transform.position.x > 0) // Xが正のとき
            {
                //右を向いたとき
                /*if (0<normalizedY &&normalizedY<45)
                {
                    speed.y = RotationSpeed * Time.deltaTime; // 時計回りに修正
                }
                
                else if(45<normalizedY)
                {
                    //speed.y = 0;
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,0f,transform.localEulerAngles.z);
                }

                //左を向いたとき
                else if (-45<normalizedY&&normalizedY < 0)
                {
                    speed.y = -RotationSpeed * Time.deltaTime; // 反時計回りに修正
                }

                else if (-45 > normalizedY)
                {
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0f, transform.localEulerAngles.z);
                }////

                if (0 < normalizedY)
                {
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0f, transform.localEulerAngles.z);
                }
                else if ( normalizedY < 0)
                {
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0f, transform.localEulerAngles.z);
                }
            }
            //xが-の時、最後の直線
            else if (transform.position.x < 0) // Xが負のとき
            {
                //右向いたとき
                /*if (-135 > normalizedY && normalizedY > -180)
                {
                    speed.y = RotationSpeed * Time.deltaTime; // 時計回りに修正
                }

                else if (-135 < normalizedY&&normalizedY<0)
                {
                    //speed.y = 0;
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180f, transform.localEulerAngles.z);
                }

                //左を向いたとき
                else if (135 < normalizedY && normalizedY < 180)
                {
                    speed.y = -RotationSpeed * Time.deltaTime; // 反時計回りに修正
                }

                else if (135 > normalizedY)
                {
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180f, transform.localEulerAngles.z);
                }////

                if (0 > normalizedY && normalizedY > -180)
                {
                    //speed.y = RotationSpeed * Time.deltaTime; // 時計回りに修正
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180f, transform.localEulerAngles.z);
                }
                else if (0 < normalizedY && normalizedY < 180)
                {
                    //speed.y = -RotationSpeed * Time.deltaTime; // 反時計回りに修正
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180f, transform.localEulerAngles.z);
                }


            }

        }*/
        //transform.eulerAngles -= speed;
    }

    float NormalizeAngle(float angle)
    {
        while (angle > 180) angle -= 360;
        while (angle < -180) angle += 360;
        return angle;
    }

    void SpeedUp()
    {
        if (SpeedUpAble == true)
        {
            //staminaGrass = 1f;
            if (Stamina > 15)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.W))
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
                //RotationSpeed = 4.9f;
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
                    if (Input.GetKeyUp(KeyCode.Return)|| Input.GetKeyUp(KeyCode.UpArrow))
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
