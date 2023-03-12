using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonForest : MonoBehaviour
{
    public AudioSource PlayerAudio;
    public GameObject TaskBarUI;
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
        PlayerAudio.Play();
        SceneManager.LoadScene(2);
        TaskBarUI.SetActive(false);
    }
}
