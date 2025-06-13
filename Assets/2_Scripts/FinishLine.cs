using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private float reloadDelay = 2f;
    [SerializeField] private ParticleSystem finishEffect; // �ν����Ϳ��� �Ҵ�
    [SerializeField] [Tooltip("����� ����� Ŭ��")] private AudioClip finishSound; // �ν����Ϳ��� �Ҵ�

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
            finishEffect.Play(); // null üũ ���� �ٷ� ��ƼŬ ���
            audioSource.Play();
            GameManager.Instance.GameStop();
        }
    }
 }
