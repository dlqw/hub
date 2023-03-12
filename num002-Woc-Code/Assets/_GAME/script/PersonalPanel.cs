using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PersonalPanel : MonoBehaviour
{
    public float MaxPlayerHP = 200;
    public float MaxPlayerMP = 200;
    public float AttackValue = 20f;
    public static bool isPause = false;
    public TextMeshProUGUI HP;
    public TextMeshProUGUI MP;
    public TextMeshProUGUI Attack;
    public GameObject personalPanel;
    readonly LightBandit player;
    // Start is called before the first frame update
    private static PersonalPanel instance;
    public static PersonalPanel Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }
    private void Awake()
    {
        Instance = this;

        MaxPlayerHP = PlayerPrefs.GetFloat("MaxPlayerHP");
        MaxPlayerMP = PlayerPrefs.GetFloat("MaxPlayerMP");
        AttackValue = PlayerPrefs.GetFloat("AttackValue");
        PlayerPrefs.SetFloat("MaxPlayerHP", PersonalPanel.Instance.MaxPlayerHP);
        PlayerPrefs.SetFloat("MaxPlayerMP", PersonalPanel.Instance.MaxPlayerMP);
        PlayerPrefs.SetFloat("AttackValue", PersonalPanel.Instance.AttackValue);
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        HP.GetComponent<TMP_Text>().text = "MaxHP:" + PersonalPanel.Instance.MaxPlayerHP.ToString();
        MP.GetComponent<TMP_Text>().text = "MaxMP:" + PersonalPanel.Instance.MaxPlayerMP.ToString();
        Attack.GetComponent<TMP_Text>().text = "AttackValue:"+ PersonalPanel.Instance.AttackValue.ToString();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }


    }

    public void Resume()
    {
        personalPanel.SetActive(false);
        Time.timeScale = 1.0f;
        isPause = false;
    }
    public void Pause()
    {
        personalPanel.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
    }
}
