using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHP : MonoBehaviour
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
        if(Manager.Instance.PlayerMoney>=20)
        {
            PlayerAudio.Play();
            PersonalPanel.Instance.MaxPlayerHP++;
            Manager.Instance.PlayerMoney -= 20;
            PlayerPrefs.SetFloat("MaxPlayerHP", PersonalPanel.Instance.MaxPlayerHP);
            
            PlayerPrefs.SetInt("PlayerMoney", Manager.Instance.PlayerMoney);
        }
        
    }
}
