using LuduArts.InteractionSystem.Runtime.Core;
using UnityEngine;

namespace LuduArts.InteractionSystem.Runtime.Interactables
{
    /// <summary>
    /// A chest that requires the player to hold the interact button to open.
    /// Once opened, it cannot be interacted with again.
    /// </summary>
    public class Chest : BaseInteractable
    {
        #region Fields

        [Header("Chest Settings")]
        [SerializeField] private string m_ItemName = "Gold Coins";
        [SerializeField] private float m_ChestHoldDuration = 2.0f;

        [Header("State")]
        [SerializeField] private bool m_IsOpened = false;

        #endregion

        #region Unity Methods

        private void Start()
        {
            m_InteractionType = InteractionType.Hold;
            m_HoldDuration = m_ChestHoldDuration;
            m_InteractionPrompt = "Hold E to Open Chest";
        }

        #endregion

        #region Public Methods

        public override void OnInteract(GameObject interactor)
        {
            if (m_IsOpened) return;

            m_IsOpened = true;
            m_IsInteractable = false; // Disable further interaction
            m_InteractionPrompt = "Opened";
            
            Debug.Log($"[Chest] Chest opened! Found: {m_ItemName}");
            // Animation/Loot logic would go here
        }

        public override void OnInteractCancel(GameObject interactor)
        {
            Debug.Log("[Chest] Interaction interrupted.");
        }

        #endregion
    }
}
