using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class contactUsPanel : MonoBehaviour
{
    public InputField email;

    public InputField name;

    public InputField message;

    public Text messageSend;
    public Button send;

    public Button rest;
    // Start is called before the first frame update
    void Start()
    {
        rest.onClick.AddListener(OnReset);
        send.onClick.AddListener(OnSend);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSend()
    {
        messageSend.gameObject.SetActive(true);
    }

    public void OnReset()
    {
        messageSend.gameObject.SetActive(false);

        email.text = null;
        name.text = null;
        message.text = null;
    }
}
