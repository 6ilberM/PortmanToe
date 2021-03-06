﻿using _Game.Player.StateMachine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(CollisionHelper)), DisallowMultipleComponent()]
public class PlayerController : MonoBehaviour
{
    public const float GroundSpeed = 6.66f;
    public const float StandardJumpForce = 6.5f;
    public const float CoyoteTime = .285f;

    [SerializeField, HideInInspector] private CircleCollider2D circleCollider;
    [SerializeField, HideInInspector] private Rigidbody2D rb;
    [SerializeField, HideInInspector] private CollisionHelper collisionsHelper;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TetriminioBlockSpawner tetriminioSpawner;

    #region FSM
    private PlayerBaseState m_currentState = null;

    private readonly GroundedState m_groundedState = new GroundedState();
    private readonly JumpingState m_jumpingState = new JumpingState();
    private readonly FallingState m_fallingState = new FallingState();

    public GroundedState GroundedState => m_groundedState;
    public JumpingState JumpingState => m_jumpingState;
    public FallingState FallingState => m_fallingState;
    #endregion

    [Header("Customizables"), Space(5)]
    public float moveSpeed;
    public float jumpHeight;
    public float fastFallMultiplier = 2f;

    [Range(0, .3f), SerializeField] private float m_smoothingFactor = 0.1666f;

    public Vector2 targetMoveSpeed => Vector2.right * moveSpeed * _horizontal + (Vector2.up * rb.velocity.y);
    public Rigidbody2D GetRigibody => rb;
    public float SmoothingFactor => m_smoothingFactor;

    public CollisionHelper GetCollisionsHelper => collisionsHelper;
    //vars
    private int _horizontal = 0;

#pragma warning disable 0414 // Remove unread private members
    private int _vertical = 0;
#pragma warning restore 0414 // Remove unread private members

    private bool isHolding = false;

    internal Vector2 m_VelocityVar;
    private float coyoteTimer;

    private WorldTetriminioController heldTetriminio = null;

    public float CoyoteTimer
    {
        get => coyoteTimer;
        set => coyoteTimer = value;
    }

    public SpriteRenderer GetSpriteRenderer => spriteRenderer;

    private void Awake()
    {
        ChangeState(m_groundedState);
    }

    private void Start()
    {
        GameManager.Instance.onGameOver.AddListener(() => { isHolding = false; });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayer();
        }
        //Populate Input
        _horizontal = _vertical = 0;

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) { _horizontal = -1; }
        else if (Input.GetKey(KeyCode.D)) { _horizontal = 1; }

        if (Input.GetKeyDown(KeyCode.W)) { m_currentState.OnPlayerJump(this); }

        if (Input.GetKey(KeyCode.W) && !(Input.GetKey(KeyCode.S))) { _vertical = -1; }
        else if (Input.GetKey(KeyCode.W)) { _vertical = 1; }

        if (_horizontal != 0) { spriteRenderer.flipX = _horizontal < 0 ? true : false; }

        //ToDo: Move Functionality functionality under this to other functions or classes
        //This is Pulling The Block
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (tetriminioSpawner.TryPopulateTetriminio() && !isHolding)
            {
                //face Placement 
                Vector3 point = transform.position + Vector3.right * (spriteRenderer.flipX ? -1.5f : 1.5f);

                heldTetriminio = Instantiate(tetriminioSpawner.Tetrminio, point, Quaternion.Euler(0, 0, GameManager.Instance.activeBlockRot), this.transform).GetComponent<WorldTetriminioController>();
                GameManager.Instance.onPullBlock?.Invoke();

                heldTetriminio.transform.position = new Vector3(Mathf.RoundToInt(point.x), Mathf.RoundToInt(point.y), point.z);
                isHolding = true;
            }
            else if (GameManager.Instance.canPlace && isHolding)
            {
                GameManager.Instance.onPlaceBlock?.Invoke();
                GameManager.Instance.pullCharge--;
                GameManager.Instance.canPlace = false;
                isHolding = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isHolding) { heldTetriminio.ToggleFreezePosition(); }

        m_currentState.Update(this);
    }

    private void FixedUpdate()
    {
        m_currentState.FixedUpdate(this);
        if (rb.velocity.y > 10f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }
    }

    public float GetJumpForce(float f_override = 1f)
    {
        return Mathf.Sqrt(rb.gravityScale * Physics2D.gravity.magnitude * 2 * jumpHeight * Mathf.Pow(rb.mass, 2)) * f_override;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_currentState.OnCollisionEnter(this, collision);
        if (collision.gameObject.CompareTag("KillBox") || collision.gameObject.CompareTag("Enemy"))
        {
            ResetPlayer();
        }
    }

    public void ChangeState(PlayerBaseState state)
    {
        //Guard Statement
        if (state == m_currentState) { return; }

        m_currentState = state;
        m_currentState.EnterState(this);

    }

    private void OnDestroy()
    {
        GameManager.Instance.onGameOver.RemoveListener(() => { isHolding = false; });
    }

    private void OnValidate()
    {
        if (Application.isPlaying) { return; }

        if (circleCollider == null) { circleCollider = GetComponent<CircleCollider2D>(); }
        if (rb == null) { rb = GetComponent<Rigidbody2D>(); }
        if (collisionsHelper == null) { collisionsHelper = GetComponent<CollisionHelper>(); }
    }
    void ResetPlayer()
    {
        SoundManager.Instance.PlaySound("Death02");
        if (GameManager.Instance.lives != 0)
        {
            GameManager.Instance.lives--;
            if (GameManager.Instance.spawnPoint == 1)
            {
                this.transform.position = GameManager.Instance.spawnPoint1.position;
            }
            else
            {
                this.transform.position = GameManager.Instance.spawnPoint2.position;
            }
            GameManager.Instance.onGameOver?.Invoke();
        }
        else
        {
            GameManager.Instance.spawnPoint = 1;
            this.transform.position = GameManager.Instance.spawnPoint1.position;
            GameManager.Instance.lives = 3;
        }
    }
}
