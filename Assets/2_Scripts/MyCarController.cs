using UnityEngine;

public class MyCarController : MonoBehaviour
{
    private SurfaceEffector2D surfaceEffector2D;
    private Rigidbody2D rb;
    private bool onGround = false;

    public float jumpForce = 7f;

    // 왼쪽과 오른쪽 회전 토크를 따로 선언 (인스펙터에서 조정 가능)
    public float leftRotationTorque = 5f;
    public float rightRotationTorque = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<SurfaceEffector2D>(out var effector))
        {
            onGround = true;
            surfaceEffector2D = effector;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<SurfaceEffector2D>(out var effector))
        {
            onGround = false;
        }
    }

    private void Update()
    {
        if (surfaceEffector2D == null) return;

        // 표면 속도 제어
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            surfaceEffector2D.speed = 20f;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            surfaceEffector2D.speed = -5f;
        }

        // 공중에서 회전 제어
        if (!onGround)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.AddTorque(-rightRotationTorque); // 오른쪽 회전 (시계 반대 방향)
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.AddTorque(leftRotationTorque);  // 왼쪽 회전 (시계 방향)
            }
        }

        UIManager.Instance.UpdateSurfaceText($"Surface Speed : {surfaceEffector2D.speed:F1}");

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            Jump();
        }

        UIManager.Instance.UpdateCarSpeedText($"Car Speed : {rb.linearVelocity.magnitude:F1}");
    }

    private void Jump()
    {
        onGround = false;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
