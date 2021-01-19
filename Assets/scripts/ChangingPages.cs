using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangingPages : MonoBehaviour
{

    [SerializeField] float SceneDelay = 5f;
    [SerializeField] GameObject Settingmeneu;
    [SerializeField] AudioClip Bubble;
    [Range(0f, 1f)] [SerializeField] float Volume = 0.1f;
    [SerializeField] GameObject music;
    [SerializeField] public bool popUpToggle=false;
    
    private bool toggle=true;
    //public string X;

    public void mute()
    {
        toggle = !toggle;
 
        if (toggle){
            AudioListener.volume = 1f;
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color=new Color(255,255,255,1);

        }
 
        else{
            
            AudioListener.volume = 0f;
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color=new Color(255,255,255,0.3f);
        }
        AudioSource.PlayClipAtPoint(Bubble, Camera.main.transform.position, Volume);
    }
    public void popup()
    {
        if (popUpToggle!=true)
        {
            Debug.Log("test");
            popUpToggle = true;
            Time.timeScale = 0f;
            Settingmeneu.SetActive(true);
            Debug.Log(Settingmeneu.active);
            AudioSource.PlayClipAtPoint(Bubble, Camera.main.transform.position, Volume);
            
        }
        else
        {
            popUpToggle = false;
            Time.timeScale = 1f;
            Settingmeneu.SetActive(false);
            AudioSource.PlayClipAtPoint(Bubble, Camera.main.transform.position, Volume);
        }
       
    }
    public void Play()
    {
        StartCoroutine(Delay());
        //string X = "Play";
        SceneManager.LoadScene("sort");
        //FindObjectOfType<GameSession>().Reset();
    }
    public void MainMeneu()
    {
        StartCoroutine(Delay());
        //string X = "Start";
        SceneManager.LoadScene("Start");
        //FindObjectOfType<GameSession>().Reset();
    }
    public void Panel()
    {
        StartCoroutine(Delay());
        //string X = "Panel1";
        SceneManager.LoadScene(4);
        //FindObjectOfType<GameSession>().Reset();
    }
    public void Store()
    {
        StartCoroutine(Delay());
        //string X = "Store";
        SceneManager.LoadScene(3);
        //FindObjectOfType<GameSession>().Reset();
    }
    public void GameOver()
    {
        StartCoroutine(Delay());
    }
    public void Exit()
    {
        Settingmeneu.SetActive(false);
    }
    IEnumerator Delay()
    {
        AudioSource.PlayClipAtPoint(Bubble, Camera.main.transform.position, Volume);
        //SceneManager.LoadScene(X);
        yield return new WaitForSeconds(SceneDelay);
        
    }

    public void OncontactUs()
    {
        SceneManager.LoadScene(5);
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene(1);

        
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Application.Quit();
            }
            else
            {
                GotoMainMenu();
            }
        }
    }

    public void GotoMultiPlayerGames()
    {
        SceneManager.LoadScene(6);

    }

    public void PurcherCategory()
    {
        //Open openAn Popup;
        //in popup say the price 
        //if he understand go to the Payment 
    }
}
