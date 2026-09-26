using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("Lives Display")]
    [SerializeField] private TextMeshProUGUI livesText;

    [Header("Marbles Display")]
    [SerializeField] private TextMeshProUGUI marblesText;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLivesChanged += UpdateLivesDisplay;
            GameManager.Instance.OnMarblesChanged += UpdateMarblesDisplay;

            // initialize with current values immediately, don't wait for the next change
            UpdateLivesDisplay(GameManager.Instance.Lives);
            UpdateMarblesDisplay(GameManager.Instance.Marbles);
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLivesChanged -= UpdateLivesDisplay;
            GameManager.Instance.OnMarblesChanged -= UpdateMarblesDisplay;
        }
    }

    private void UpdateLivesDisplay(int lives)
    {
        livesText.text = lives.ToString();
    }

    private void UpdateMarblesDisplay(int marbles)
    {
        marblesText.text = marbles.ToString();
    }
}