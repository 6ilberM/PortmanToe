using _Game.Player.StateMachine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D)), DisallowMultipleComponent()]
public class PlayerController : MonoBehaviour
{

    public const float GroundSpeed = 6.66f;
    public const float jumpForce = 6.5f;

    [SerializeField, HideInInspector] private CapsuleCollider2D capsuleCollider;
    [SerializeField, HideInInspector] private Rigidbody2D rb;

    //FSM
    private PlayerBaseState m_currentState = null;

    private readonly GroundedState m_groundedState = new GroundedState();
    private readonly JumpingState m_jumpingState = new JumpingState();
    public GroundedState GroundedState => m_groundedState;
    public JumpingState JumpingState => m_jumpingState;

    public float moveSpeed;
    [Range(0, .3f), SerializeField] private float m_smoothingFactor = 0.1666f;

    public Vector2 targetMoveSpeed => Vector2.right * moveSpeed * _horizontal + (Vector2.up * rb.velocity.y);
    public Rigidbody2D GetRigibody => rb;
    public float SmoothingFactor => m_smoothingFactor;

    //vars
    int _horizontal = 0;
    int _vertical = 0;

    internal Vector2 m_VelocityVar;

    private void Start() { ChangeState(m_groundedState); }

    private void Update()
    {
        _horizontal = 0;

        if (Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.D))) { _horizontal = -1; }
        else if (Input.GetKey(KeyCode.D)) { _horizontal = 1; }

        if (Input.GetKeyDown(KeyCode.W)) { m_currentState.OnPlayerJump(this); }

        if (Input.GetKey(KeyCode.W) && !(Input.GetKey(KeyCode.S))) { _vertical = -1; }
        else if (Input.GetKey(KeyCode.W)) { _vertical = 1; }

    }

    private void FixedUpdate() { m_currentState.FixedUpdate(this); }

    private void OnCollisionEnter2D(Collision2D collision) { m_currentState.OnCollisionEnter(this, collision); }

    public void ChangeState(PlayerBaseState state)
    {
        //Guard Statement
        if (state == m_currentState) { return; }

        m_currentState = state;
        m_currentState.EnterState(this);
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            return;
        }

        if (capsuleCollider == null) { capsuleCollider = GetComponent<CapsuleCollider2D>(); }
        if (rb == null) { rb = GetComponent<Rigidbody2D>(); }
    }
}
