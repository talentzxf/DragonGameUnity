using System.Collections;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] private float upSpeed = 5f;
    [SerializeField] private float glideGravityScale = 0.1f;
    [SerializeField] private float rotationVelocity = 100f;
    [SerializeField] private float forwardBackwardVelocity = 5.0f;
    [SerializeField] public float moveSpeed = 5f;

    private bool _isOnGround = false;
    private bool _isGliding = false;

    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private Transform _transform;

    public void SetIsOnGround(bool isOnGround)
    {
        _isOnGround = isOnGround;

        Debug.Log("_isOnGround:" + _isOnGround);
    }

    public bool IsGliding
    {
        get { return _isGliding; }
        set
        {
            _isGliding = value;
            if (_isGliding)
            {
                _meshRenderer.material.color = Color.red;
            }
            else
            {
                _meshRenderer.material.color = Color.gray;
            }
        }
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isOnGround)
            IsGliding = false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed!");
            if (_isOnGround)
            {
                _rigidbody.AddForce(Vector3.up * upSpeed, ForceMode.Force);
            }
            else if (!_isGliding)
            {
                IsGliding = true;
                StartCoroutine(Gliding());
            }
            else
            {
                IsGliding = false;
                _meshRenderer.material.color = Color.gray;
            }
        }

        DirectionMove();
    }

    public void DirectionMove()
    {
        float vertical = Input.GetAxis("Vertical");

        Vector3 forwardVelocity = vertical * Time.deltaTime * forwardBackwardVelocity * _transform.forward;

        _rigidbody.AddForce(forwardVelocity, ForceMode.VelocityChange);

        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            float rotationAmount = rotationVelocity * horizontal * Time.deltaTime;
            Quaternion deltaRotation = Quaternion.Euler(_transform.up * rotationAmount);
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);

            Vector3 velocity = _rigidbody.velocity;
            velocity = Quaternion.Euler(rotationAmount * Time.deltaTime * _transform.up) * velocity;
            _rigidbody.velocity = velocity.normalized * moveSpeed;

        }
    }

    public IEnumerator Gliding()
    {
        Vector3 remainGravity = (glideGravityScale - 1) * Physics.gravity;
        Vector3 curVelocity = _rigidbody.velocity;
        curVelocity.y = 0.0f;
        _rigidbody.velocity = curVelocity;

        while (_isGliding)
        {
            // Add force to deduct gravity.
            _rigidbody.velocity += Time.deltaTime * remainGravity;

            Debug.Log("IsGliding, velocity:" + _rigidbody.velocity);

            yield return null;
        }
    }
}