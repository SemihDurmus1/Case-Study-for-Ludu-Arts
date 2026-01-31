using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TMPro.TextMeshProUGUI interactionText;
    public Slider holdProgressBar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EditInteractionText(string text)
    {
        interactionText.text = text;
    }
    public void ClearInteractionText()
    {
        interactionText.text = "";
    }

    public void UpdateHoldProgress(float progressValue)
    {
        interactionText.text = "";
        if (holdProgressBar == null) return;
        if (holdProgressBar.value >= 0f) holdProgressBar.gameObject.SetActive(true);
        else holdProgressBar.gameObject.SetActive(false);

        holdProgressBar.value = progressValue;
    }

    public void StartProgressDecay()
    {
        if (holdProgressBar == null) return;
        holdProgressBar.gameObject.SetActive(false);
        holdProgressBar.value = 0f;
    }

    public void HideHoldProgress()
    {
        if (holdProgressBar == null) return;
        holdProgressBar.gameObject.SetActive(false);
        holdProgressBar.value = 0f;
    }
}
