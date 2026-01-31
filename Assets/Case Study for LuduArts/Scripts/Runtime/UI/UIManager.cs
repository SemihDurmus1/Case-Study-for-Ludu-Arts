using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace LuduArts.InteractionSystem.Runtime.UI
{
    /// <summary>
    /// Manages the UI elements for the interaction system, including prompts and progress bars.
    /// Implements a singleton pattern for easy global access.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        #region Fields

        public static UIManager Instance { get; private set; }

        [Header("Interaction UI")]
        [SerializeField] private GameObject m_InteractionPanel;
        [SerializeField] private TextMeshProUGUI m_PromptText;
        [SerializeField] private Image m_HoldProgressBar;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            ClearInteractionText();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Displays the interaction prompt with the specified message.
        /// </summary>
        /// <param name="message">The text to display (e.g., "Press E to Open").</param>
        public void EditInteractionText(string message)
        {
            if (m_InteractionPanel != null)
            {
                m_InteractionPanel.SetActive(true);
            }

            if (m_PromptText != null)
            {
                m_PromptText.text = message;
            }
        }

        /// <summary>
        /// Hides the interaction prompt and resets UI elements.
        /// </summary>
        public void ClearInteractionText()
        {
            if (m_InteractionPanel != null)
            {
                m_InteractionPanel.SetActive(false);
            }

            UpdateHoldProgress(0f);
        }

        /// <summary>
        /// Updates the fill amount of the hold progress bar.
        /// </summary>
        /// <param name="progress">Value between 0 and 1.</param>
        public void UpdateHoldProgress(float progress)
        {
            if (m_HoldProgressBar != null)
            {
                m_HoldProgressBar.gameObject.SetActive(progress > 0f);
                m_HoldProgressBar.fillAmount = Mathf.Clamp01(progress);
            }
        }

        #endregion
    }
}
