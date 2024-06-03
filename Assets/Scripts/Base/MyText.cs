using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    //private void Awake()
    //{
    //    textMesh = GetComponent<TextMeshProUGUI>();
    //    Debug.Log(textMesh);
    //}

    public void SetText(string str)
    {
        Debug.Log(textMesh);
        textMesh?.SetText(str);
    }
}
