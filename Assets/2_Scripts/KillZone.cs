using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField]
    private float delay = 1.0f; // 인스펙터에서 조정 가능

    [SerializeField]
    private AudioClip destroySound; // 파괴 사운드 클립

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // AudioSource가 없으면 자동으로 추가
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 파괴 사운드 재생
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
