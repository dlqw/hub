using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRange : MonoBehaviour
{
    public float Skillspeed = 12;
    public LightBandit player;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(destory), 10f);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(transform.right * Skillspeed * Time.deltaTime, Space.World);
    }

    private void destory()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "enemy":
                if (collision.gameObject.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy.EnemyHP -= 50;
                }
                    Destroy(gameObject);
                break;
            case "ground":
                Destroy(gameObject);
                //Debug.LogError("摧毁");
                break;
            default:
                break;
        }
    }
}
