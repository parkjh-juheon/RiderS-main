using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private float reloadDelay = 2f;
    [SerializeField] private ParticleSystem finishEffect; // 인스펙터에서 할당
    [SerializeField] [Tooltip("재생할 오디오 클립")] private AudioClip finishSound; // 인스펙터에서 할당

    private AudioSource audioSource;
    private bool isFinished = false; // 중복 재생 방지 플래그

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFinished)
        {
            isFinished = true;
            finishEffect.Play(); // null 체크 없이 바로 파티클 재생
            audioSource.Play();
            GameManager.Instance.GameStop();
        }
    }
 }
