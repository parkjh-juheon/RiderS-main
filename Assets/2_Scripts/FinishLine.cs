using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private float reloadDelay = 2f;
    [SerializeField] private ParticleSystem finishEffect; // 인스펙터에서 할당
    [SerializeField] [Tooltip("재생할 오디오 클립")] private AudioClip finishSound; // 인스펙터에서 할당
    [SerializeField] private GameObject clearPanel; // 인스펙터에서 연결할 패널

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
            StartCoroutine(PlayEffectAndStopGame());
        }
    }

    private IEnumerator PlayEffectAndStopGame()
    {
        yield return new WaitForSeconds(0.3f); // 원하는 지연 시간

        if (finishEffect != null)
            finishEffect.Play();

        if (finishSound != null && audioSource != null)
            audioSource.PlayOneShot(finishSound);

        // 배경음악 볼륨 0으로 설정
        BGMVolumeController bgmController = FindObjectOfType<BGMVolumeController>();
        if (bgmController != null && bgmController.bgmSource != null)
        {
            bgmController.bgmSource.volume = 0f;
            if (bgmController.volumeSlider != null)
                bgmController.volumeSlider.value = 0f; // UI 슬라이더도 0으로
        }

        GameManager.Instance.GameStop();

        // 인스펙터에서 연결한 패널 열기
        if (clearPanel != null)
            clearPanel.SetActive(true);
    }
}
