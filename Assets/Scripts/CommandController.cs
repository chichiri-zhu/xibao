using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    [SerializeField] private Transform circleTransform;

    private MyInput myInput;
    private float commandRof = 10f;
    private bool isCommand = false;
    private bool isChoose = false;

    private List<Soldier> soldierChooseList;

    private void Start()
    {
        HideCircle();
        layerMask = LayerMask.GetMask("Soldier");
        soldierChooseList = new List<Soldier>();
        myInput = new MyInput();
        myInput.Enable();

        myInput.Player.Command.started += Command_started;
        myInput.Player.Command.canceled += Command_canceled;
    }

    private void Update()
    {
        HandleChooseSoldier();
    }

    private LayerMask layerMask;
    private void HandleChooseSoldier()
    {
        if (isChoose)
        {
            Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, 10f, layerMask);
            foreach (Collider2D collider in collider2DArray)
            {
                Soldier soldier = collider.GetComponent<Soldier>();
                if (soldier != null && !soldierChooseList.Contains(soldier))
                {
                    soldierChooseList.Add(soldier);
                    FollowController followController = soldier.gameObject.AddComponent<FollowController>();
                    followController.SetTarget(transform);
                }
            }
        }
    }

    private void Command_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isCommand)
        {
            //设置选择单位的巡逻点
            isCommand = false;
            foreach (var soldier in soldierChooseList)
            {
                soldier.transform.position = transform.position;
                FollowController followController = soldier.GetComponent<FollowController>();
                if(followController != null)
                {
                    DestroyImmediate(followController);
                }
                soldier.SetPatrolPosition(soldier.transform.position);
            }
            soldierChooseList.Clear();
            HideCircle();
        }
        else
        {
            //选择单位
            //CreateCircle();
            ShowCircle();
            isCommand = true;
            isChoose = true;
        }
    }

    private void Command_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isCommand)
        {
            isChoose = false;
        }
    }

    public void ShowCircle()
    {
        circleTransform.gameObject.SetActive(true);
    }

    public void HideCircle()
    {
        circleTransform.gameObject.SetActive(false);
    }

    //public float radius = 10f;
    //public int numSegments = 100;
    //private LineRenderer lineRenderer;
    //private void CreateCircle()
    //{
    //    lineRenderer = gameObject.AddComponent<LineRenderer>();
    //    lineRenderer.positionCount = numSegments + 1;
    //    lineRenderer.loop = true;
    //    Vector3 center = transform.position;
    //    float angleIncrement = 2f * Mathf.PI / numSegments;

    //    for (int i = 0; i <= numSegments; i++)
    //    {
    //        float angle = i * angleIncrement;
    //        float x = center.x + radius * Mathf.Cos(angle);
    //        float y = center.y + radius * Mathf.Sin(angle);
    //        Vector3 point = new Vector3(x, y, 0f);
    //        lineRenderer.SetPosition(i, point);
    //    }
    //}
}
