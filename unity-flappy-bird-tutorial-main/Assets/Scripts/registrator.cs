using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class registrator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textBox;
    void Start()
    {
        textBox.text = "print your nickname and press ENTER";
    }
    private void FixedUpdate()
    {
        if (DataHolder.nick != null)
        {
            textBox.text = "print your telegram username and press ENTER";
        }
    }
}
