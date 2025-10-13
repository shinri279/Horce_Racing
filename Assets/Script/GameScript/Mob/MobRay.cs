using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MobRay : MonoBehaviour
{
    [SerializeField] GameObject Object;
    [SerializeField] float distance = 0.8f;//Rayの長さ
    [SerializeField] float leftDistance = 1f;//Rayの長さ
    [SerializeField] Vector3 rayOffset = Vector3.zero;// Rayの開始位置を調整
    public Rigidbody rb;
    public float JumpPower = 10;
    public bool JumpFlag = false;

    MobHorse script;
    private Animator animator;// Animatorを追加
    /*GameObject Mob;
    MobHorse script;*/
    // Start is called before the first frame update
    void Start()
    {
        script = GetComponent<MobHorse>();
        animator = GetComponent<Animator>();
        /*
        Mob = GameObject.FindGameObjectWithTag("Mob");
        script = Mob.GetComponent<MobHorse>();
        float NoMobSideSpeed = script.MobSideSpeed;*/
    }

    // Update is called once per frame
    void Update()
    {
        var rayStartPosition = transform.position+rayOffset;// 開始位置を調整可能に

        //前方のRay
        var rayDirection = transform.forward.normalized;

        RaycastHit raycastHit;

        bool isForwardHit = Physics.Raycast(rayStartPosition, rayDirection, out raycastHit, distance);

        Debug.DrawRay(rayStartPosition, rayDirection * distance, Color.red);

        if (isForwardHit)
        {
            //Debug.Log("HitObject:" + raycastHit.collider.gameObject.name);
            if (raycastHit.collider.CompareTag("Wall"))
            {
                if (JumpFlag == true)
                {
                    rb.AddForce(Vector3.up * JumpPower*Time.deltaTime, ForceMode.Impulse);
                    animator.SetBool("Jump", true);
                    animator.SetBool("Run", false);
                    Debug.Log("Jump");
                    //script.MobSideSpeed = 0f;
                }
            }
        }
        else
        {
            animator.SetBool("Run", true);
            animator.SetBool("Jump", false);
        }


        //横方向のRay
        var leftDirection = -transform.right.normalized;  // 左方向
        RaycastHit leftHit;

        bool isLeftHit = Physics.Raycast(rayStartPosition, leftDirection, out leftHit, leftDistance);

        Debug.DrawRay(rayStartPosition, leftDirection * leftDistance, Color.green);

        if (isLeftHit)
        {
            //Debug.Log("Left Hit: " + leftHit.collider.gameObject.name);
            if (leftHit.collider.CompareTag("Horce")|| leftHit.collider.CompareTag("Player"))
            {
                script.MobSideSpeed = 0f;
            }
        }
        else
        {
            script.MobSideSpeed = 3f;
        }

    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            JumpFlag = true;
            //script.MobSideSpeed = 0.05f;
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
}
