using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TMPro.TextMeshProUGUI interactionText;

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

    public void UpdateHoldProgress(float time)
    {

    }
}
