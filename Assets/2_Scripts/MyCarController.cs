using UnityEngine;

public class MyCarController : MonoBehaviour
{
    private SurfaceEffector2D surfaceEffector2D;
    private Rigidbody2D rb;
    private bool onGround = false;

    public float jumpForce = 7f;
    private AudioSource moveAudioSource;

    [SerializeField] private AudioClip moveSound; // 인스펙터에서 할당
    [SerializeField] private float minSpeed = 0f;
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float minVolume = 0.1f;
    [SerializeField] private float maxVolume = 1f;



    // 왼쪽과 오른쪽 회전 토크를 따로 선언 (인스펙터에서 조정 가능)
    public float leftRotationTorque = 5f;
    public float rightRotationTorque = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAudioSource = gameObject.AddComponent<AudioSource>();
        moveAudioSource.clip = moveSound;
        moveAudioSource.loop = true;
        moveAudioSource.playOnAwake = false;
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

        HandleMoveSound();
    }

    private void Jump()
    {
        onGround = false;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void HandleMoveSound()
    {
        float speed = rb.linearVelocity.magnitude;
        bool isMoving = speed > 0.5f;

        if (onGround && isMoving)
        {
            if (!moveAudioSource.isPlaying)
            {
                moveAudioSource.Play();
            }

            // 속도에 따라 볼륨 조절
            float t = Mathf.InverseLerp(minSpeed, maxSpeed, speed); // 0 ~ 1 사이로 정규화
            float volume = Mathf.Lerp(minVolume, maxVolume, t);     // 정규화된 t를 볼륨 범위에 매핑
            moveAudioSource.volume = volume;
        }
        else
        {
            if (moveAudioSource.isPlaying)
            {
                moveAudioSource.Pause();
            }
        }
    }


}
