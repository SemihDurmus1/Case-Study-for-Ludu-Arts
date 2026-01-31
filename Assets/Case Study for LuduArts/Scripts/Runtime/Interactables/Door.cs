using LuduArts.InteractionSystem.Runtime.Core;
using LuduArts.InteractionSystem.Runtime.Player.Inventory;
using UnityEngine;

namespace LuduArts.InteractionSystem.Runtime.Interactables
{
    /// <summary>
    /// Represents a door that can be opened, closed, or locked.
    /// Uses Toggle interaction and checks for keys in the InventoryManager.
    /// </summary>
    public class Door : BaseInteractable
    {
        #region Fields

        [Header("Door Settings")]
        [SerializeField] private bool m_IsLocked = false;
        [SerializeField] private string m_RequiredKeyID;
        [SerializeField] private string m_LockedMessage = "Key Required";
        [SerializeField] private string m_OpenMessage = "Press E to Open Door";
        [SerializeField] private string m_CloseMessage = "Press E to Close Door";

        [Header("State")]
        [SerializeField] private bool m_IsOpen = false;

        [Header("Animator")]
        [SerializeField] private Animator m_DoorAnimator;

        #endregion

        #region Unity Methods

        private void Start()
        {
            m_InteractionType = InteractionType.Toggle;
            UpdatePrompt();
        }

        #endregion

        #region Public Methods

        public override void OnInteract(GameObject interactor)
        {
            if (m_IsLocked)
            {
                if (InventoryManager.Instance != null && InventoryManager.Instance.HasKey(m_RequiredKeyID))
                {
                    m_IsLocked = false;
                    Debug.Log("[Door] Door unlocked!");
                }
                else
                {
                    Debug.Log($"[Door] {m_LockedMessage}");
                    return;
                }
            }

            m_IsOpen = !m_IsOpen;
            m_DoorAnimator.SetBool("isOpen", true);
            UpdatePrompt();
            
            // Animation logic would go here
            Debug.Log($"[Door] Door is now {(m_IsOpen ? "Open" : "Closed")}");
        }

        public override string GetInteractionPrompt()
        {
            if (m_IsLocked)
            {
                return $"{m_LockedMessage} ({m_RequiredKeyID})";
            }
            return m_IsOpen ? m_CloseMessage : m_OpenMessage;
        }

        #endregion

        #region Private Methods

        private void UpdatePrompt()
        {
            m_InteractionPrompt = GetInteractionPrompt();
        }

        #endregion
    }
}
