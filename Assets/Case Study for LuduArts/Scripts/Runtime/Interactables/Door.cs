using LuduArts.InteractionSystem.Runtime.Core;
using LuduArts.InteractionSystem.Runtime.Player.Inventory;
using UnityEngine;

namespace LuduArts.InteractionSystem.Runtime.Interactables
{
    /// <summary>
    /// Represents a door that can be opened, closed, or locked.
    /// Uses Toggle interaction and checks for keys in the InventoryManager.
    /// Smoothly rotates around a hinge point when opened/closed.
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

        [Header("Rotation Settings")]
        [SerializeField] private Transform m_DoorHinge;
        [SerializeField] private float m_OpenAngle = 90f;
        [SerializeField] private float m_RotationSpeed = 2f;

        [Header("State")]
        [SerializeField] private bool m_IsOpen = false;
        
        private Quaternion m_ClosedRotation;
        private Quaternion m_OpenRotation;
        private bool m_IsRotating = false;
        [SerializeField]private const float k_rotationIgnoreThreshold = 0.2f;

        #endregion

        #region Unity Methods

        private void Start()
        {
            m_InteractionType = InteractionType.Toggle;
            
            // Store the closed rotation and calculate open rotation
            if (m_DoorHinge != null)
            {
                m_ClosedRotation = m_DoorHinge.localRotation;
                m_OpenRotation = m_ClosedRotation * Quaternion.Euler(0f, m_OpenAngle, 0f);
            }
            else
            {
                Debug.LogWarning("[Door] Door hinge not assigned! Using door transform as fallback.");
                m_DoorHinge = transform;
                m_ClosedRotation = transform.localRotation;
                m_OpenRotation = m_ClosedRotation * Quaternion.Euler(0f, m_OpenAngle, 0f);
            }
            
            UpdatePrompt();
        }

        private void Update()
        {
            if (m_IsRotating)
            {
                RotateDoor();
            }
        }

        #endregion

        #region Public Methods

        public override void OnInteract(GameObject interactor)
        {
            // Prevent interaction while door is rotating
            if (m_IsRotating)
            {
                return;
            }

            // Check if door is locked
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

            // Toggle door state and start rotation
            m_IsOpen = !m_IsOpen;
            m_IsRotating = true;
            
            Debug.Log($"[Door] Door is now {(m_IsOpen ? "opening" : "closing")}...");
        }

        public override string GetInteractionPrompt()
        {
            if (m_IsRotating)
            {
                return "Door is moving...";
            }
            
            if (m_IsLocked)
            {
                return $"{m_LockedMessage} ({m_RequiredKeyID})";
            }
            
            return m_IsOpen ? m_CloseMessage : m_OpenMessage;
        }

        public override bool IsInteractable => base.IsInteractable && !m_IsRotating;

        #endregion

        #region Private Methods

        private void RotateDoor()
        {
            if (m_DoorHinge == null)
            {
                return;
            }

            Quaternion targetRotation = m_IsOpen ? m_OpenRotation : m_ClosedRotation;
            
            // Smoothly interpolate to target rotation
            m_DoorHinge.localRotation = Quaternion.Slerp(
                m_DoorHinge.localRotation,
                targetRotation,
                Time.deltaTime * m_RotationSpeed
            );

            // Check if rotation is complete (within a small threshold)
            float angle = Quaternion.Angle(m_DoorHinge.localRotation, targetRotation);
            if (angle < k_rotationIgnoreThreshold)
            {
                m_DoorHinge.localRotation = targetRotation;
                m_IsRotating = false;
                UpdatePrompt();
                Debug.Log($"[Door] Door is now {(m_IsOpen ? "open" : "closed")}");
            }
        }

        private void UpdatePrompt()
        {
            m_InteractionPrompt = GetInteractionPrompt();
        }

        #endregion
    }
}
