using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private float reloadDelay = 2f;
    [SerializeField] private ParticleSystem finishEffect; // �ν����Ϳ��� �Ҵ�
    [SerializeField] [Tooltip("����� ����� Ŭ��")] private AudioClip finishSound; // �ν����Ϳ��� �Ҵ�
    [SerializeField] private GameObject clearPanel; // �ν����Ϳ��� ������ �г�

    private AudioSource audioSource;
    private bool isFinished = false; // �ߺ� ��� ���� �÷���

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
        yield return new WaitForSeconds(0.3f); // ���ϴ� ���� �ð�

        if (finishEffect != null)
            finishEffect.Play();

        if (finishSound != null && audioSource != null)
            audioSource.PlayOneShot(finishSound);

        // ������� ���� 0���� ����
        BGMVolumeController bgmController = FindObjectOfType<BGMVolumeController>();
        if (bgmController != null && bgmController.bgmSource != null)
        {
            bgmController.bgmSource.volume = 0f;
            if (bgmController.volumeSlider != null)
                bgmController.volumeSlider.value = 0f; // UI �����̴��� 0����
        }

        GameManager.Instance.GameStop();

        // �ν����Ϳ��� ������ �г� ����
        if (clearPanel != null)
            clearPanel.SetActive(true);
    }
}
