using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField]
    private float delay = 1.0f; // 인스펙터에서 조정 가능

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
