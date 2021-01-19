using System;
using FiroozehGameService.Core;
using FiroozehGameService.Models;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public string userName;
    public string password;
    public string email;
    public GameObject loggin;
    public GameObject register;
    public GameObject haveAnAccount;
    public InputField passwordInputField;
    public InputField emailInputField;
    public InputField nameInputField;
    public Text error;
    public Text waiting;
    public GameObject panel;
    async void Start()
    {
        try
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                if (GameSaveSystem.Instans.IsLoginBefore())
                {
                    var userToken = GameSaveSystem.Instans.GetUserToken();
                    await GameService.Login(userToken);
                    SceneManager.LoadScene(1);

                }
                else
                {
                    panel.SetActive(true);
                    waiting.gameObject.SetActive(false);
                }
            }
            else
            {
                SceneManager.LoadScene(1);

            }

        }
        catch(GameServiceException e)
        {
            Debug.LogError(e.Message);
            error.text = e.Message;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void HaveAccountAlready()
    {
        register.SetActive(false);
        haveAnAccount.SetActive(false);
        nameInputField.gameObject.SetActive(false);
        loggin.SetActive(true);
    }

    public  async  void Login()
    {        error.text = null;

        try
        {
            var userToken = await GameService.Login( email, password);
            GameSaveSystem.Instans.SetLoginBefore(1);
            GameSaveSystem.Instans.SetUserToken(userToken);
            GameSaveSystem.Instans.SetPassword(password);
            SceneManager.LoadScene(1);
        }
        catch (GameServiceException e)
        {
            Console.WriteLine(e);
            error.text = e.Message;
            throw;
            
        }
       
    }

    public async void Register()
    {
        error.text = null;

        try
        {
            var userToken = await GameService.SignUp(userName, email, password);
            GameSaveSystem.Instans.SetUserToken(userToken);
            GameSaveSystem.Instans.SetPassword(password);
            GameSaveSystem.Instans.SetLoginBefore(1);

            SceneManager.LoadScene(1);
        }
        catch (GameServiceException e)
        {
            Console.WriteLine(e);
            error.text = e.Message;

            throw;
        }
       
    }

    public void PasswordField()
    {
        password = passwordInputField.text;
    }

    public void EmailField()
    {
        email = emailInputField.text;
    }

    public void NameField()
    {
        userName = nameInputField.text;
    }
    
}

