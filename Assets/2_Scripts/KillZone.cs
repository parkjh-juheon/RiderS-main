using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField]
    private float delay = 1.0f; // �ν����Ϳ��� ���� ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(StopGameWithDelay());
    }

    private System.Collections.IEnumerator StopGameWithDelay()
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.GameStop();
    }
}
