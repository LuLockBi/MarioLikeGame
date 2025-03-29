using UnityEngine;


public class TouchingDirections : MonoBehaviour
{
    public delegate void GroundedChangeHandler(bool isGrounded);

    [Header("Touching Settings")]
    [SerializeField] private ContactFilter2D groundContactFilter;
    [SerializeField] private float groundDistance = 0.07f;
    [SerializeField] private float wallDistance = 0.2f;
    //[SerializeField] private float ceilingDistance = 0.2f;

    [Header("References")]
    [SerializeField] private CapsuleCollider2D touchingCollider;

    private readonly RaycastHit2D[] groundHits = new RaycastHit2D[5];
    private readonly RaycastHit2D[] wallHits = new RaycastHit2D[5];
    //private readonly RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    private bool _isGrounded;
    private bool _isOnWall;
    //private bool _isOnCeiling;
    private Vector2 direction;

    public event GroundedChangeHandler OnGroundedChange;

    private void Awake()
    {
        touchingCollider = GetComponent<CapsuleCollider2D>();
    }

    public bool IsGrounded
    {
        get => _isGrounded;
        private set
        {
            if (_isGrounded != value)
            {
                _isGrounded = value;
                OnGroundedChange?.Invoke(_isGrounded);
            }
        }
    }

    public bool IsOnWall
    {
        get => _isOnWall;
        private set
        {
            if (_isOnWall != value)
            {
                _isOnWall = value;
            }
        }
    }

    //public bool IsOnCeiling
    //{
    //    get => _isOnCeiling;
    //    set
    //    {
    //        if (_isOnCeiling != value)
    //        {
    //            _isOnCeiling = value;
    //        }
    //    }
    //}

    private void FixedUpdate()
    {
        CheckGroundStatus();

        CheckWallStatus();

        //CheckCeilingStatus();
    }



    private void CheckGroundStatus()
    {
        int hitCount = touchingCollider.Cast(Vector2.down, groundContactFilter, groundHits, groundDistance);

        IsGrounded = hitCount > 0;
    }

    private void CheckWallStatus()
    {
        direction = Vector2.right * Mathf.Sign(transform.localScale.x);
        int hitCount = touchingCollider.Cast(direction, groundContactFilter, wallHits, wallDistance);

        IsOnWall = hitCount > 0;
    }

    //private void CheckCeilingStatus()
    //{
    //    int hitCount = touchingCollider.Cast(Vector2.up, groundContactFilter, ceilingHits, ceilingDistance);

    //    IsOnCeiling = hitCount > 0;
    //}
}
