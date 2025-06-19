using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField]
    private float delay = 1.0f; // �ν����Ϳ��� ���� ����

    [SerializeField]
    private AudioClip destroySound; // �ı� ���� Ŭ��

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // AudioSource�� ������ �ڵ����� �߰�
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �ı� ���� ���
        if (destroySound != null && audioSource != null)
        {
            audioSource.PlayOneShot(destroySound);
        }
        StartCoroutine(StopGameWithDelay());
    }

    private System.Collections.IEnumerator StopGameWithDelay()
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.GameStop();
    }
}
