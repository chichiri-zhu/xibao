using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rigidbody2d;
    private Vector2 lookDirection = new Vector2(1, 0);
    //[SerializeField] private int speed = 3;//速度

    private MyInput myInput;
    private Vector2 moveInput;

    private void Awake()
    {
        player = GetComponent<Player>();
        myInput = new MyInput();
        myInput.Enable();
        myInput.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        myInput.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    //private void Update()
    //{
    //    //HandleMovement();
    //}

    private void FixedUpdate()
    {
        player.SetMoveDir(moveInput);
    }

    //private void FixedUpdate()
    //{
    //    moveInput = new Vector2(1,0);
    //    if(moveInput != Vector2.zero)
    //    {
    //        Vector2 pos = transform.position;
    //        pos = pos + 10 * moveInput * Time.fixedDeltaTime;
    //        player.playerController.SetPosition(pos);
    //    }

    //}

    //private void HandleMovement()
    //{
    //    //当前玩家输入的某个轴向不为0
    //    if (!Mathf.Approximately(moveInput.x, 0) || !Mathf.Approximately(moveInput.y, 0))
    //    {
    //        lookDirection.Set(moveInput.x, moveInput.y);
    //        lookDirection.Normalize();
    //    }

    //    Vector2 position = transform.position;

    //    position = position + speed * moveInput * Time.deltaTime;

    //    transform.position = position;
    //    //rigidbody2d.MovePosition(position);
    //}
}
