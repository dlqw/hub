using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAttack : MonoBehaviour
{
    public AudioSource PlayerAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseDown()
    {
        if (Manager.Instance.PlayerMoney >= 50)
        {
            PlayerAudio.Play();
            PersonalPanel.Instance.AttackValue++;
            Manager.Instance.PlayerMoney -= 50;
            PlayerPrefs.SetFloat("AttackValue", PersonalPanel.Instance.AttackValue);
            PlayerPrefs.SetInt("PlayerMoney", Manager.Instance.PlayerMoney);
        }
    }
}
