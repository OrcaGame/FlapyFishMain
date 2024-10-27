using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;

public class jsonServ : MonoBehaviour
{
    [SerializeField] string url = "http://46.148.227.196:1331";
    public static int value;
    public static string lidPul;
    List<string> liderPul = new List<string>();
    [SerializeField] string us;
    [SerializeField] string nick;
    private void Start()
    {
        us = DataHolder.us;
        nick = DataHolder.nick;
        DataHolder.coins = PlayerPrefs.GetInt("manys");
        StartCoroutine(GetData());
    }

    private void FixedUpdate()
    {
            lidPul = mesCont(liderPul);
    }
    IEnumerator GetData()
    {
        while (true)
        {
                using (UnityWebRequest request = UnityWebRequest.Get(url))
                {
                request.SetRequestHeader("Access-Control-Allow-Credentials", "true");
                request.SetRequestHeader("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
                request.SetRequestHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                request.SetRequestHeader("Access-Control-Allow-Origin", "*");

                yield return request.SendWebRequest();


                    if (request.result == UnityWebRequest.Result.ConnectionError)
                    {
                        print(request.error);
                    }
                    else
                    {
                        liderPul.Clear();
                        string JSON = request.downloadHandler.text;
                        SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(JSON);
                        value = stats["users"][us]["value"];
                    if (stats["users"][us]["coins"] > DataHolder.coins)
                    {
                        DataHolder.coins = stats["users"][us]["coins"];
                    }
                        print(value);
                        for (int i = 0; stats["lid"][i] != null; i++)
                        {
                            int coins = stats["lid"][i]["coins"];
                            int val = stats["lid"][i]["value"];
                            if (!liderPul.Contains(stats["lid"][i]["nick"] + ":" + (val+coins) + "/" + stats["lid"][i]["us"].ToString()))
                            {
                                liderPul.Add(stats["lid"][i]["nick"] + ":" + (val+coins) + "/" + stats["lid"][i]["us"].ToString());
                            }
                        }
                        PlayerPrefs.SetInt("manys", DataHolder.coins);
                    }
                    print("sad");
                }
            StartCoroutine(sendMes());
            yield return new WaitForSeconds(1);
        }
    }
    string mesCont(List<string> array)
    {
        string mes = "";
        for (int i = 0; i < array.Count; i++)
        {
            mes += $"{i+1}:"+array[i].Substring(0, array[i].IndexOf('/')) + "\n";
        }
        return mes;
    }
    public IEnumerator sendMes()
    {
        
            WWWForm form = new WWWForm();
            PostStruct post = new PostStruct()
            {
                nick = nick,
                us = us,
                value = value,
                coins = DataHolder.coins
            };
            string mes = JsonUtility.ToJson(post);
            UnityWebRequest request = UnityWebRequest.Post(url, form);
            byte[] postBytes = Encoding.UTF8.GetBytes(mes);
            UploadHandler UH = new UploadHandlerRaw(postBytes);
            request.uploadHandler = UH;
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            yield return new WaitForSeconds(1);
    }

    public struct PostStruct
    {
        public string nick;
        public string us;
        public int value;
        public int coins;
    }
}
