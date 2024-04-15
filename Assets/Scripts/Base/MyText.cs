using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MyText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string str)
    {
        textMesh?.SetText(str);
    }
}
