using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetect : MonoBehaviour
{
    [SerializeField] private ParticleSystem crashEffect;
    [SerializeField] private AudioClip crashSound;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            crashEffect.Play(); // 이펙트 재생
            audioSource.PlayOneShot(crashSound); // 충돌 사운드 재생
            GameManager.Instance.GameStop();
        }
    }
}
