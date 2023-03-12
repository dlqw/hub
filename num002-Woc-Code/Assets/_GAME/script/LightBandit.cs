using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class LightBandit : MonoBehaviour
{
    public bool isOnGround = false;
    public float xLast;
    public Rigidbody2D rb;
    public Collider2D coll;
    public float jumpForce = 9f;
    public float LightBanditSpeed = 5f;
    public float LightBanditSpeed2;
    public float MPtimer = 1.0f;
    public float AttackValue;
    public bool isAttack = false;

    public float MaxPlayerHP ;
    public float PlayerHP = 200;
    public float MaxPlayerMP ;
    public float PlayerMP = 200;
    private Vector3 Skill_aulerangles;

    private const float AttackInterval = 42;//攻击间隔
    public float AttackTimer = AttackInterval;
    //  private SpriteRenderer sprite_renderer;//引用精灵渲染器组件
    private bool isDashing;
    public bool canDash=true;
    private float dashingPower=10f;
    private float dashingTime = 0.1f;
    private float dashingCooldown = 3f;//冷却

    [SerializeField] private TrailRenderer tr;//轨迹渲染器
    
    
    
    public Animator LightBandit_AnimController;
    public AudioSource PlayerAudio;
    public AudioClip[] manAudio;
    public GameObject AttackRange;
    public GameObject SkillRange;
    GameObject HPBar = null;
    GameObject MPBar = null;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetFloat("MaxPlayerHP", PersonalPanel.Instance.MaxPlayerHP);
        //PlayerPrefs.SetFloat("MaxPlayerMP", PersonalPanel.Instance.MaxPlayerMP);
        //PlayerPrefs.SetFloat("AttackValue", PersonalPanel.Instance.AttackValue);
        AttackValue = PlayerPrefs.GetFloat("AttackValue");
        MaxPlayerHP = PlayerPrefs.GetFloat("MaxPlayerHP");
        MaxPlayerMP = PlayerPrefs.GetFloat("MaxPlayerMP");
        LightBanditSpeed2 = LightBanditSpeed;
        LightBandit_AnimController = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LightBandit_jump();

        if (isDashing)
        {
            return;
        }
        //LightBandit_move();
        //LightBandit_jump();
        SetBar();
        SetRenew();
        PlayerDie();
      

    }



    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
      
        LightBandit_move();
      
        LightBandit_attack();
        if (Input.GetMouseButton(1) && PlayerMP > 1&&canDash)
        {
            //transform.Translate(Vector3.right * xLast*1.5f, Space.World);
            PlayerMP -= 20;
            StartCoroutine(Dash());
        }
      
    }



    private void LightBandit_move()// 移动
    {
        float x = Input.GetAxisRaw("Horizontal");
        //返回由 axisName 标识的虚拟轴的值,监听玩家水平轴的输入,这里用Input.GetAxisRaw的原因是其取得的值只有-1,0,1，效果比Input.GetAxis更稳定

        transform.Translate(Vector3.right * x * LightBanditSpeed * Time.fixedDeltaTime, Space.World);
        //Vector3(1, 0, 0)，变量x控制正反方向，速度为每秒三米做相对于世界坐标系的变换
        //rb.velocity = new Vector2(LightBanditSpeed  * x, 0);
        int xAbs = Mathf.Abs((int)x);
        ;
        LightBandit_AnimController.SetInteger("AnimState", 2 * xAbs);

        if (x == 0)
        {
            return;
        }
        else
        {
            xLast = x;
        }
        if(x>0)
        {
            Skill_aulerangles = new Vector3(0, 0, 0);
        }
        else if(x<0)
        {
            Skill_aulerangles = new Vector3(0, 180, 0);
        }
        
        transform.localScale = new Vector3(-1 * (int)xLast, 1, 0);
        //if (Input.GetMouseButton(1) && isOnGround && PlayerMP > 1)
        //{
        //    //transform.Translate(Vector3.right * xLast*1.5f, Space.World);
        //    PlayerMP -= 1;
        //    StartCoroutine(Dash());
        //}

    }
    private void LightBandit_jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isOnGround = false;
            PlayerAudio.clip = manAudio[1];

            if (!PlayerAudio.isPlaying)
            {
                PlayerAudio.Play();
            }


        }
    }
    private void LightBandit_attack()
    {
        if (AttackTimer <= AttackInterval)
        {
            LightBanditSpeed = 0.6f;
            AttackTimer++;
            if (AttackTimer > 18)
            {
                isAttack = true;

            }
        }
        else
        {
            LightBanditSpeed = LightBanditSpeed2;
            isAttack = false;
        }

        if (Input.GetMouseButton(0) && AttackTimer >= AttackInterval)
        {

            LightBandit_AnimController.SetTrigger("Attack");
            AttackTimer = 0;
            LightBanditSpeed = 0;
            

        }
        if (isAttack)
        {
            AttackRange.SetActive(true);
            transform.Translate(Vector3.right * 0.001f * xLast, Space.Self);
            //解决OnTriggerEnter2D不触发的问题。
            //OnTriggerEnter是这样触发的：必须有一方是is trigger；主动活动的一方有刚体；enable collider 为true时，并不产生触发，只有collider运动时才触发
            //就好比两物体collider接触中，突然一个物体消失，那么OnTriggerExit不会触发的道理是一样的。
            //所以我让coll向面向方向位移0.001f来保证OnTriggerEnter2D正常触发
        }
        else
        {
            AttackRange.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Q)&&(PlayerMP>=50))//在用户开始按下 name 标识的键的帧期间返回 true,
        {
            PlayerMP -= 50;
            Instantiate(SkillRange, (transform.localPosition + new Vector3(0, 1.2f, 0)), Quaternion.Euler/*返回一个欧拉角度的旋转*/(transform.eulerAngles/*将 X、Y 和 Z 值转换为四元数的内部格式*/+ Skill_aulerangles/*应该旋转的角度*/));
        }
    }

   
    
    public void LightBanditHurt()
    {
        if (PlayerHP > 0)
            LightBandit_AnimController.SetTrigger("Hurt");
    }
    public void PlayerDie()
    {
        if(PlayerHP<=0)
        {
            SceneManager.LoadScene(1);
           
        }
    }
    private void SetBar()//状态条
    {
        if(HPBar ==null)
        {
            HPBar = GameObject.FindGameObjectWithTag("HPBar");
        }
        HPBar.GetComponent<Image>().fillAmount = PlayerHP / MaxPlayerHP;
        if (MPBar == null)
        {
            MPBar = GameObject.FindGameObjectWithTag("MPBar");
        }
        MPBar.GetComponent<Image>().fillAmount = PlayerMP / MaxPlayerMP;
    }
    private void SetRenew()
    {
        MPtimer -= Time.deltaTime;
        if (MPtimer <= 0)
        {
            if (PlayerMP < MaxPlayerMP)
            {
                PlayerMP += 5;
                MPtimer = 1.0f;
            }
        }
    }

        private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("ground")|| collision.transform.CompareTag("enemy"))
        {
            isOnGround = true; //jumpis为true便可以跳跃
           
            LightBandit_AnimController.SetBool("Grounded", true);
        }
       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "ground")
        {
            isOnGround = false;
            LightBandit_AnimController.SetBool("Grounded", false);
            LightBandit_AnimController.SetFloat("AirSpeed", 0);
            LightBandit_AnimController.SetTrigger("Jump");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerAudio.clip = manAudio[1];

        if (!PlayerAudio.isPlaying)
        {
            PlayerAudio.Play();
            //PlayerAudio.PlayOneShot(manAudio[1]);
        }
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy!=null)
        {
            enemy.EnemyHurt();
            enemy.EnemyHP -= AttackValue;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(-transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

}






