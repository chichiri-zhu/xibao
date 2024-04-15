using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FontPoint : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    public TextMeshProUGUI text;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
