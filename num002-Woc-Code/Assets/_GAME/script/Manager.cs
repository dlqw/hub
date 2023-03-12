using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public int PlayerMoney = 0;
   
    public TextMeshProUGUI PlayerMoneyText;
    // public Text PlayerPlayChancesText;

    private static Manager instance;

    public static Manager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;//声明动态类型的语法：
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerMoney = PlayerPrefs.GetInt("PlayerMoney");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoneyText.GetComponent<TMP_Text>().text = PlayerMoney.ToString();
    }
}
