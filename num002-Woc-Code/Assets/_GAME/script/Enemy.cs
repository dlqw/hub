using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Enemy enemy;

    public float EnemyHP=100;
    public bool isDead = false;
    public bool isFight = false;
    public Collider2D VigilanceRange;
    public Collider2D AttackRange;
    public GameObject EnemyAttackRange;
    Rigidbody2D rg;
    public float MoveSpeed = 0;
    float changeTime = 0;
    float time = 0;
    //���ʱ��
    public float minTime = 0;
    public float maxTime = 0;

    int direction = 1;
    float distance_ = 0;
    bool isChange = false;


    Transform PlayerTransform;
    public float Distance = 0;


    float Dwell_gap = 0; //ͣ��ʱ��
    float Dwell_Cumulative = 0; //ͣ��ʱ���ۻ�
    public float DwellMinTime = 0;
    public float DwellMaxTime = 0;

    float moveTime = 0; //����ʱ��
    //���ߵ����ʱ������ʱ���趨
    public float moveMinTime = 0;
    public float moveMaxTime = 0;

    float moveTimeCumulative = 0;

    //bool isDwell = false;
    bool isDwellRange = false;

    bool CanMove = false;
    bool CanCount = false;
    bool CanTimeCumulative = false;
    //bool isAttacking = false;
    //bool isAttacked = false;

    public bool TrackPlayer = false;
    public float TrackSpeed = 0;

    public float CurrentPlayer = 0;

    GameObject gm;

    // Start is called before the first frame update

    public Animator HeavyBandit_AnimController;
    private void Awake()
    {
        enemy = this;
    }
    void Start()
    {
        HeavyBandit_AnimController = GetComponent<Animator>();
        HeavyBandit_AnimController.SetInteger("AnimState", 0);
        rg = GetComponent<Rigidbody2D>();
        CanMove = false;
        CanCount = true;
    }

    // Update is called once per frame
    void Update()
    {
        //_________________________________________________________________________________________________________________________
         PlayerTransform = GameObject.Find("LightBandit").transform;
        //�õ�����ҵľ���
        distance_ = Mathf.Sqrt(Mathf.Abs(PlayerTransform.transform.position.x - transform.position.x) * Mathf.Abs(PlayerTransform.transform.position.x - transform.position.x) +Mathf.Abs(PlayerTransform.transform.position.y - transform.position.y) * Mathf.Abs(PlayerTransform.transform.position.y - transform.position.y));

        time = time + Time.deltaTime;

        if (isChange == false)
        {
            changeTime = Random.Range(minTime, maxTime);
            isChange = true;
        }
        if (time > changeTime)
        {
            direction = -direction;
            transform.localScale = new Vector3(-direction, 1, 1);
            time = 0;
            isChange = false;
        }

        transform.localScale = new Vector3(-direction, 1, 1);
        if (isFight)
        {
            //�ж�����ڹ�����ı�
            if (PlayerTransform.transform.position.x < transform.position.x)
            {
                direction = -1;

                HeavyBandit_AnimController.SetInteger("AnimState", 2);
                transform.localScale = new Vector3(-direction, 1, 1);
            }
            else
            {
                direction = 1;
                HeavyBandit_AnimController.SetInteger("AnimState", 2);
                transform.localScale = new Vector3(-direction, 1, 1);
             
            }
        }
        

        //�����ͣʱ�� ��ʼ��ͣ ����ͣ������ ����ֹͣ�ƶ� ������ͣʱ��
        if (CanCount)
        {
            isDwellRange = true;
            CanTimeCumulative = true;
            CanCount = false;
        }

        if (isDwellRange) //����ͣ������ ������ͣʱ�� 
        {
            Dwell_gap = Random.Range(DwellMinTime, DwellMaxTime);
            HeavyBandit_AnimController.SetInteger ("AnimState", 0);
       
            CanMove = false;
            isDwellRange = false;
        }

        if (CanTimeCumulative)
        {
            Dwell_Cumulative = Dwell_Cumulative + Time.deltaTime;
        }


        if (Dwell_Cumulative > Dwell_gap) //������ͣʱ�� ͣ������ ����ͣ������ ��������ʱ�� ��ʼ���߶���
        {
            HeavyBandit_AnimController.SetInteger("AnimState", 2);
            moveTime = Random.Range(moveMinTime, moveMaxTime);
            CanMove = true;
            Dwell_Cumulative = 0;
            CanTimeCumulative = false;
        }

        //����ʱ����� 
        if (CanMove)
        {

            moveTimeCumulative = moveTimeCumulative + Time.deltaTime;
            if (moveTimeCumulative > moveTime)
            {
                CanMove = false;
                moveTimeCumulative = 0;
                CanCount = true;
            }
        }

        //__________________________________________________________________________________________
        EnemyDie();
    }
    private void FixedUpdate()
    {
        //û��ͣ���Ϳ����ƶ�
        if (CanMove)
        {
            MoveMent();
        }

    }
    void MoveMent()
    {
        HeavyBandit_AnimController.SetInteger("AnimState", 2);
        if (TrackPlayer == false)
        {
            rg.velocity = new Vector2(1 * direction * MoveSpeed, 0);
        }
        else
        {
            if (distance_ < Distance)
            {

                if (PlayerTransform.transform.position.x < transform.position.x)
                {
                    rg.velocity = new Vector2(-1 * TrackSpeed, 0);
                }
                else
                {
                    rg.velocity = new Vector2(1 * TrackSpeed, 0);
                }
            }

        }

    }


    public void EnemyHurt()
    {
        if(EnemyHP>0)
        HeavyBandit_AnimController.SetTrigger("Hurt");
    }
    private void EnemyDie()
    {
        if(EnemyHP<=0)
        {
            isFight = false;
            isDead = true;
            HeavyBandit_AnimController.SetTrigger("Death");
             EnemyAttackRange.SetActive(false);
            CanMove = false;
            Invoke(nameof(Destroy), 3);
        }
    }
    public void Destroy()
    {
        Manager.Instance.PlayerMoney++;
        PlayerPrefs.SetInt("PlayerMoney", Manager.Instance.PlayerMoney);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D VigilanceRange)
    {
       
        if (VigilanceRange.transform.CompareTag("Player"))
        {

            if (isDead)
            {
                EnemyAttackRange.SetActive(false);
            }

                Invoke("OpenIsFight", 1);
        }
         

    }
    
    private void OnTriggerExit2D(Collider2D VigilanceRange)
    {
        if (VigilanceRange.transform.tag == "Player")
        {
            HeavyBandit_AnimController.SetInteger("AnimState", 0);
            isFight = false;
        }
    }
    public void OpenIsFight()
    {
        if (isDead == false)
        {

            HeavyBandit_AnimController.SetInteger("AnimState", 1);
            isFight = true;
            EnemyAttackRange.SetActive(true);
        }
        else
        {
            isFight = false;
        }
          
    }
    public void EnemyAttack()
    {
        if (isDead == false)
        {
            HeavyBandit_AnimController.SetTrigger("Attack");
        }
           
    }
    
}
