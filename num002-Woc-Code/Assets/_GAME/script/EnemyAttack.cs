using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;


public class EnemyAttack : MonoBehaviour
{
    public Collider2D collision;
    public Enemy enemy;
    private float timer = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
       // enemy = GetComponentsInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision) 
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent<LightBandit>(out var player))
            {
              // player.LightBanditHurt();
              if(timer<0)
                {
                    player.PlayerHP -= 20;
                    enemy.EnemyAttack();
                    //GameObject.Find("LightBandit").SendMessage("LightBanditHurt");
                    timer = 1.0f;
                    Invoke("PlayerBeAttacked", 0.5f);
                }
                timer = timer - Time.fixedDeltaTime;
              
           
            }
            




        }

    }
    private void PlayerBeAttacked()
    {

       // GameObject.Find("HeavyBandit").SendMessage("EnemyAttack");

        //enemy.EnemyAttack();

         GameObject.Find("LightBandit").SendMessage("LightBanditHurt");



    }

}
