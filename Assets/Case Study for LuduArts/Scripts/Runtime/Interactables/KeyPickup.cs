using LuduArts.InteractionSystem.Runtime.Core;
using LuduArts.InteractionSystem.Runtime.Player.Inventory;
using UnityEngine;

namespace LuduArts.InteractionSystem.Runtime.Interactables
{
    /// <summary>
    /// Represents a key that can be picked up by the player.
    /// Uses Instant interaction and adds itself to the InventoryManager.
    /// </summary>
    public class KeyPickup : BaseInteractable
    {
        #region Fields

        [Header("Key Settings")]
        [SerializeField] private string m_KeyID = "GeneralKey";

        #endregion

        #region Unity Methods

        private void Start()
        {
            m_InteractionType = InteractionType.Instant;
            m_InteractionPrompt = $"Press E to Pickup {m_KeyID}";
        }

        #endregion

        #region Public Methods

        public override void OnInteract(GameObject interactor)
        {
            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.AddKey(m_KeyID);
                Debug.Log($"[KeyPickup] Picked up key: {m_KeyID}");
                
                // Hide or destroy the key object
                gameObject.SetActive(false);
            }
        }

        #endregion
    }
}
