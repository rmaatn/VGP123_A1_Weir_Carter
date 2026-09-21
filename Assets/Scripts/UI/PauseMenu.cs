using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.OnPauseChanged += HandlePauseChanged;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnPauseChanged -= HandlePauseChanged;
    }

    private void HandlePauseChanged(bool isPaused)
    {
        gameObject.SetActive(isPaused);
    }
}