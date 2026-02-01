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
        [SerializeField] private float m_ChestHoldDuration = 2.0f;

        [Header("State")]
        [SerializeField] private bool m_IsOpened = false;

        [Header("Chest Cover")]
        [SerializeField] private Transform m_ChestCover;
        [SerializeField] private float m_OpenAngleZ = 90f;
        [SerializeField] private float m_RotationSpeed = 2f;

        private Quaternion m_TargetRotation;
        private bool m_IsRotating = false;
        [SerializeField] private const float k_rotationIgnoreThreshold = 0.2f;

        #endregion

        #region Unity Methods

        private void Start()
        {
            m_InteractionType = InteractionType.Hold;
            m_HoldDuration = m_ChestHoldDuration;
            m_InteractionPrompt = "Hold E to Open Chest";

            if (m_ChestCover != null)
            {
                m_TargetRotation = Quaternion.Euler(m_ChestCover.localEulerAngles.x,
                                                    m_ChestCover.localEulerAngles.y,
                                                    -m_OpenAngleZ);
            }
        }
        private void Update()
        {
            if (m_IsRotating) ApplyRotation();
        }

        #endregion

        #region Public Methods

        private void ApplyRotation()
        {
            m_ChestCover.localRotation = Quaternion.Slerp(m_ChestCover.localRotation,
                                                        m_TargetRotation,
                                                        Time.deltaTime * m_RotationSpeed);

            if (Quaternion.Angle(m_ChestCover.localRotation, m_TargetRotation) < k_rotationIgnoreThreshold)
            {
                m_ChestCover.localRotation = m_TargetRotation;
                m_IsRotating = false;
            }
        }

        public override void OnInteract(GameObject interactor)
        {
            if (m_IsOpened) return;

            m_IsOpened = true;
            m_IsRotating = true;
            m_IsInteractable = false; // Disable further interaction
            m_InteractionPrompt = "Opened";

            RotateCover();

            Debug.Log($"[Chest] Chest opened!");
        }

        private void RotateCover()
        {
            if (m_ChestCover == null)
            {
                return;
            }

            Quaternion targetRotation = m_TargetRotation;

            // Smoothly interpolate to target rotation
            m_ChestCover.localRotation = Quaternion.Slerp(m_ChestCover.localRotation,
                targetRotation, Time.deltaTime * m_RotationSpeed);

            // Check if rotation is complete (within a small threshold)
            float angle = Quaternion.Angle(m_ChestCover.localRotation, targetRotation);
            if (angle < k_rotationIgnoreThreshold)
            {
                m_ChestCover.localRotation = targetRotation;
                m_IsRotating = false;
            }
        }

        public override void OnInteractCancel(GameObject interactor)
        {
            Debug.Log("[Chest] Interaction interrupted.");
        }

        #endregion
    }
}
