using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _horizontalDirection = 0f;
    private float _deathSpeed = 20.0f;
    private int _jumpCount = 0;
    private bool _alive = true;

    private Animator _animator;
    private CapsuleCollider2D _bodyCollider;
    private BoxCollider2D _feetCollider;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _runSpeed = 3.3f;
    [SerializeField] private float _jumpSpeed = 9.0f;
    [SerializeField] private float _playerGravityScale = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _bodyCollider = GetComponent<CapsuleCollider2D>();
        _feetCollider = GetComponent<BoxCollider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // FixedUpdate is called once per frame
    // Used for physics interactions
    void Update()
    {
        if(_alive)
        {
            Run();
            Jump();
            Climb();
            FlipSprite();
        }
    }

    private void Run()
    {
        _horizontalDirection = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(_horizontalDirection * _runSpeed, _rigidBody.velocity.y);
        _rigidBody.velocity = playerVelocity;

        bool running = _horizontalDirection != 0f;
        _animator.SetBool("Running", running);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _jumpCount < 1)
        {
            Vector2 playerVelocity = new Vector2(_rigidBody.velocity.x, _jumpSpeed);
            _rigidBody.velocity = playerVelocity;
            _jumpCount++;
        }

        if(_feetCollider.IsTouchingLayers(LayerMask.GetMask("Floor")))
            _jumpCount = 0;
    }

    private void Climb()
    {
        if (_bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            float verticalDirection = Input.GetAxis("Vertical");
            Vector2 playerVelocity = new Vector2(_rigidBody.velocity.x, verticalDirection * _runSpeed);
            _rigidBody.velocity = playerVelocity;
            _rigidBody.gravityScale = 0;
        }
        else
        {
            _rigidBody.gravityScale = _playerGravityScale;
        }
        _animator.SetBool("Climbing", _bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")));
    }

    private void FlipSprite()
    {
        if (_horizontalDirection != 0f)
            // Flip player if going left
            _spriteRenderer.flipX = _horizontalDirection < 0;
    }

    private void Die(Vector2 deathDirection)
    {
        _alive = false;
        float horizontalDirection = deathDirection.x < 0 ? -1 : 1;
        Vector2 playerVelocity = new Vector2(horizontalDirection * _deathSpeed, _deathSpeed);
        _rigidBody.velocity = playerVelocity;
        _animator.SetTrigger("Die");
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor") && !_alive)
        {
            Vector2 playerVelocity = new Vector2(0, 0);
            _rigidBody.velocity = playerVelocity;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies") && _alive)
        {
            List<ContactPoint2D> contacts = new List<ContactPoint2D>();
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(LayerMask.NameToLayer("Enemies"));
            collision.GetContacts(contacts);

            ContactPoint2D contact = contacts[0];
            Die(_rigidBody.GetPoint(contact.point));
        }
    }

    
}
