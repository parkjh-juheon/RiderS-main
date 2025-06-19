using System.Collections;
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
            crashEffect.Play(); // ����Ʈ ���
            Debug.Log("Crash detected with ground!");
            audioSource.PlayOneShot(crashSound); // �浹 ���� ���
            StartCoroutine(DelayedGameStop());
        }
    }


    private IEnumerator DelayedGameStop()
    {
        yield return new WaitForSeconds(0.3f);
        Debug.Log("Game stopped due to crash!");
        GameManager.Instance.GameStop();
    }
}
