using UnityEngine;
using TMPro;

public class KeyGameManager : MonoBehaviour
{
    public static KeyGameManager Instance;
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] TextMeshProUGUI printBox;
    [SerializeField] GameObject go;
    [SerializeField] GameObject serv;

    private void Start()
    {
        Instance = this;
        printBox.text = "";
        textBox.text = "";
    }

    public void DeleteLetter()
    {
        if(textBox.text.Length != 0) {
            textBox.text = textBox.text.Remove(textBox.text.Length - 1, 1);
        }
    }

    public void AddLetter(string letter)
    {
        textBox.text = textBox.text + letter;    }

    public void SubmitWord()
    {
        if (textBox.text.Length != 0)
        {
            printBox.text = textBox.text;
            dataSever(textBox.text);
            textBox.text = "";
            print("text out");
        }
        // Debug.Log("Text submitted successfully!");
    }
    public void dataSever(string text)
    {
        if(DataHolder.nick == null)
        {
            DataHolder.nick = text;
        }
        else
        {
            DataHolder.us = text;
            go.SetActive(false);
            serv.SetActive(true);
        }
    }
}
