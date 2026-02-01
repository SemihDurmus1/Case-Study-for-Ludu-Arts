using System;
using System.Collections.Generic;
using UnityEngine;

namespace LuduArts.InteractionSystem.Runtime.Player.Inventory
{
    /// <summary>
    /// Manages the player's inventory, specifically for storing keys and items.
    /// Acts as a central data store for collected interactables.
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        #region Fields

        public static InventoryManager Instance { get; private set; }

        [Header("Settings")]
        [SerializeField] private List<string> m_CollectedKeys = new List<string>();

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
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a key ID to the inventory.
        /// </summary>
        /// <param name="keyID">The unique identifier for the key.</param>
        public void AddKey(string keyID)
        {
            if (!m_CollectedKeys.Contains(keyID))
            {
                m_CollectedKeys.Add(keyID);
                Debug.Log($"[InventoryManager] Added key: {keyID}");
            }
        }

        /// <summary>
        /// Checks if the player has a specific key.
        /// </summary>
        /// <param name="keyID">The unique identifier for the key.</param>
        /// <returns>True if the key is in the inventory.</returns>
        public bool HasKey(string keyID)
        {
            return m_CollectedKeys.Contains(keyID);
        }

        /// <summary>
        /// Removes a key from the inventory.
        /// </summary>
        /// <param name="keyID">The unique identifier for the key.</param>
        public void RemoveKey(string keyID)
        {
            if (m_CollectedKeys.Contains(keyID))
            {
                m_CollectedKeys.Remove(keyID);
            }
        }

        #endregion
    }
}
