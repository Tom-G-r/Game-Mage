using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGameManagerTime : MonoBehaviour
{
    public Text TimerTextfield;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gmObj = GameObject.FindGameObjectWithTag("GameManager");
        if (gmObj != null) {
            _gameManager = gmObj.GetComponent<GameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        UpdateText();
    }


    public void UpdateText()
    {
        if (_gameManager != null) {
            TimeSpan time = TimeSpan.FromSeconds(_gameManager.Timer);

            //here backslash is must to tell that colon is
            //not the part of format, it just a character that we want in output
            // {(int)TimerTextfield.fontSize * 0.66}

            String sTime = time.ToString(@"hh\:mm\:ss");
            String sTimeFff = time.ToString(@"\:fff");

            String str = sTime + $"<size={(int)TimerTextfield.fontSize * 0.66}>" + sTimeFff + "</size>";

            TimerTextfield.text = str; // time.ToString(@"hh\:mm\:ss\<size=10>:fff</size>");



        }
    }

}
