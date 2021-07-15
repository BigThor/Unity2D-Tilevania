using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _horizontalDirection = 0f;

    private Animator _animator;
    private BoxCollider2D _wallCollider;
    private PolygonCollider2D _bodyCollider;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _runSpeed = 3.3f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _wallCollider = GetComponent<BoxCollider2D>();
        _bodyCollider = GetComponent<PolygonCollider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _horizontalDirection = _runSpeed;
        Run();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Run()
    {
        Vector2 velocity = new Vector2(_horizontalDirection * _runSpeed, _rigidBody.velocity.y);
        _rigidBody.velocity = velocity;
    }

    private void ChangeDirection()
    {
        _wallCollider.offset = new Vector2(-_wallCollider.offset.x, _wallCollider.offset.y);
        _horizontalDirection *= -1;
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
        Run();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "Foreground")
        {
            ChangeDirection();
        }
    }

    

}
