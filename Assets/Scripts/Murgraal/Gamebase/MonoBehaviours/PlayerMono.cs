using Murgraal.Gamebase;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerMono : Entity
{
    [SerializeField] private Rigidbody2D _rigid;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpSpeed;
    protected override void Init()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private Vector2 input; 
    protected override void Execute()
    {
        
    }

    protected override void FixedExecute()
    {
        PlayerProcesses.MovePlayerHorizontally(_rigid,_moveSpeed);
        PlayerProcesses.HandleJump(_rigid,_jumpSpeed);
    }
}

public static class PlayerProcesses
{
    public static void MovePlayerHorizontally(Rigidbody2D rigid, float moveSpeed)
    {
        var motion = new Vector2(InputHandler.Data.DirectionalInput.x, 0);
        rigid.AddForce(motion * moveSpeed,ForceMode2D.Impulse);
    }

    public static void HandleJump(Rigidbody2D rigid, float jumpSpeed)
    {
        if (InputHandler.Data.Jump)
        {
            rigid.AddForce(Vector2.up * jumpSpeed,ForceMode2D.Impulse);
        }
    }
}

