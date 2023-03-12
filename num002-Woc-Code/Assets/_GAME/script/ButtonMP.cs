using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMP : MonoBehaviour
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
        if (Manager.Instance.PlayerMoney >= 30)
        {
            PlayerAudio.Play();
            PersonalPanel.Instance.MaxPlayerMP++;
            Manager.Instance.PlayerMoney -= 30;
            PlayerPrefs.SetFloat("MaxPlayerMP", PersonalPanel.Instance.MaxPlayerMP);
            PlayerPrefs.SetInt("PlayerMoney", Manager.Instance.PlayerMoney);
        }
    }
}
