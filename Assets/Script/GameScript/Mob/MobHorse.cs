using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MobHorse : MonoBehaviour
{
    public float MobForwardSpeed;
    public float StartSpeed;
    public float SecondSpeed;
    public float EndSpeed;
    public float MobSpeedUp;
    public float MobSideSpeed;
    public float RotationSpeed;
    public float SpeedUpLapseTime;
    public bool SpeedUpAble;
    public float rnd;
    public float DecelerationFactor = 2f;//Wall内にいると減速する値
    private bool isInBox = false; // Wall内にいるかどうか
    private bool startMobBool = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartMobMoving", 5f);
        MobForwardSpeed = StartSpeed + Random.Range(0.0f, 1.0f);
        EndSpeed = EndSpeed + Random.Range(0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (startMobBool)
        {
            Move();
            Rotation();
            SpeedUp();
            SideMove();
        }
        /*Move();
        Rotation();
        SpeedUp();
        SideMove();*/
    }

    void StartMobMoving()
    {
        startMobBool = true;
    }

    void Move()
    {
        var speed = Vector3.zero;
        speed.z = MobForwardSpeed * Time.deltaTime;
        transform.Translate(speed);
    }

    void SideMove()
    {
        var speed = Vector3.zero;
        if (transform.position.x > 52||transform.position.x<-52)
        {
            speed.x = -MobSideSpeed * Time.deltaTime;
        }
        transform.Translate(speed);
    }

    void Rotation()
    {
        var speed = Vector3.zero;
        if (transform.position.z > 40 && (transform.localEulerAngles.y >= 180|| transform.localEulerAngles.y ==0))
        {
            speed.y = RotationSpeed * Time.deltaTime;
        }else if (transform.position.z < -40)
        {
            speed.y = RotationSpeed * Time.deltaTime;
        }

        /*if (180<transform.localEulerAngles.y && transform.localEulerAngles.y < 360)
        {
            speed.y = RotationSpeed;
        }*/

        /*if (transform.position.z > 40 && transform.localEulerAngles.y < 360)
        {
            speed.y = RotationSpeed;
        }*/

        if (-40 < transform.position.z && transform.position.z < 40 && transform.localEulerAngles.y != 0)
        {
            //xが+の時、最初の直線
            if (transform.position.x > 0)
            {
                if (180 < transform.localEulerAngles.y && transform.localEulerAngles.y < 360)
                {
                    speed.y = -RotationSpeed * Time.deltaTime;
                }
                if (0 < transform.localEulerAngles.y && transform.localEulerAngles.y < 180)
                {
                    speed.y = RotationSpeed * Time.deltaTime;
                }
            }

            //xが-の時、最後の直線
            if (transform.position.x < 0)
            {
                if (180 < transform.localEulerAngles.y && transform.localEulerAngles.y < 360)
                {
                    speed.y = RotationSpeed * Time.deltaTime;
                }
                if (0 < transform.localEulerAngles.y && transform.localEulerAngles.y < 180)
                {
                    speed.y = -RotationSpeed * Time.deltaTime;
                }
            }
        }
        transform.eulerAngles -= speed;
    }

    void SpeedUp()
    {
        //ランダムでスピードアップ
        if (SpeedUpLapseTime == 0)
        {
            rnd = Random.Range(1, 10);
        }

        if (SpeedUpAble == true)
        {
            if (rnd == 1)
            {
                if(-40<transform.position.z&& transform.position.z < 40)
                {
                    MobForwardSpeed += MobSpeedUp;
                }
                else
                {
                    MobForwardSpeed += MobSpeedUp;
                    RotationSpeed += 3f;
                }
                SpeedUpAble = false;
            }
        }

        if (SpeedUpAble == false)
        {
            SpeedUpLapseTime += Time.deltaTime;

            if (SpeedUpLapseTime >= 2)//2秒スピードアップ
            {
                if (CountdownTimer.elapsedTime>5&& CountdownTimer.elapsedTime < 70)
                {
                    MobForwardSpeed = SecondSpeed;
                }
                else if(CountdownTimer.elapsedTime > 70)
                {
                    MobForwardSpeed = EndSpeed;
                }
                else
                {
                    MobForwardSpeed = 5f;
                }
                //MobForwardSpeed = 5f;
                RotationSpeed = 5f;
            }

            if (SpeedUpLapseTime >= 5)//5秒のクールタイム
            {
                SpeedUpAble = true;
                SpeedUpLapseTime = 0;
            }
        }
    }

    // Triggerに入ったとき
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            isInBox = true;  // Wallエリアに入ったとき
            MobForwardSpeed -= DecelerationFactor;
        }
    }

    // Triggerから出たとき
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            isInBox = false;  // Wallエリアから出たとき
            MobForwardSpeed += DecelerationFactor;
        }
    }
}
