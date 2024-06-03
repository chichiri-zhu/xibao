using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMerge : MonoBehaviour
{
    private Transform icon;
    private Tower tower;

    private void Awake()
    {
        tower = GetComponent<Tower>();
    }

    private void Start()
    {
        icon = Instantiate(AssetManager.Instance.mergeIcon, transform);
        icon.localPosition = new Vector3(0f, 5.72f, 0f);
        icon.gameObject.SetActive(false);
    }

    public void Show()
    {
        CanvasManager.Instance.towerMergeUI.Show(tower);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.GetGameStatus() == GameStatus.Prepare)
        {
            icon.gameObject.SetActive(true);
            InputManager.Instance.QAction = Show;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.GetGameStatus() == GameStatus.Prepare)
        {
            icon.gameObject.SetActive(false);
        }
        InputManager.Instance.QAction = null;
    }
}
