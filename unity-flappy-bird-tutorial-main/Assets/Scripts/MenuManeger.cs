using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MenuManeger : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] TextMeshProUGUI liderPUL;
    [SerializeField] TMP_InputField nick;
    [SerializeField] TMP_InputField us;
    [SerializeField] TextMeshProUGUI user;
    [SerializeField] GameObject[] go;
    string nickname,username;
    private void Start()
    {
        if (go.Length >0)
        {
            if (DataHolder.us == null)
            {
                go[0].SetActive(true);
                go[1].SetActive(false);
            }
            else
            {
                go[0].SetActive(false);
                go[1].SetActive(true);
            }
        }
        DataHolder.coins = PlayerPrefs.GetInt("manys");
    }

    private void Update()
    {
        if (text != null)
        {
            text.text = (DataHolder.coins + jsonServ.value).ToString();
            GameManager.SaveCoins();
        }
        if (liderPUL != null)
        {
            liderPUL.text = jsonServ.lidPul;
        }
        if(DataHolder.nick != null& user != null) {
            user.text = DataHolder.nick;
        }
    }
    public void GoToScene(int num)
    {
        SceneManager.LoadScene(num);
        GameManager.LikeGameOver();
        print(DataHolder.coins);
    }
    public void TG()
    {
        Application.OpenURL("https://t.me/JJaVVabot");
    }
    public void setAckTrue(GameObject go)
    {
        go.SetActive(true);
    }
    public void setAckFalse(GameObject go)
    {
        go.SetActive(false);
    }
    public void showTeext()
    {
        DataHolder.nick = nick.text;
        DataHolder.us = us.text;
        print(DataHolder.us);
    }
}
