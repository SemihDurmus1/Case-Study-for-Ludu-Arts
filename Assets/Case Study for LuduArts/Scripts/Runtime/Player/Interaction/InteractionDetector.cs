using System;
using LuduArts.InteractionSystem.Runtime.Core;
using LuduArts.InteractionSystem.Runtime.UI;
using UnityEngine;

namespace LuduArts.InteractionSystem.Runtime.Player
{
    /// <summary>
    /// Handles detecting and interacting with interactable objects in the world.
    /// Optimized for raycast detection from the camera's perspective.
    /// </summary>
    public class InteractionDetector : MonoBehaviour
    {
        #region Fields

        [Header("Detection Settings")]
        [SerializeField] private Transform m_CameraTransform;
        [SerializeField] private float m_InteractDistance = 5f;
        [SerializeField] private LayerMask m_InteractableLayers;

        private IInteractable m_CurrentInteractable;
        private float m_HoldStartTime;
        private bool m_IsHolding;
        private bool m_IsInteractionHandled;

        #endregion

        #region Unity Methods

        private void Start()
        {
            if (m_CameraTransform == null && Camera.main != null)
            {
                m_CameraTransform = Camera.main.transform;
            }
        }

        private void FixedUpdate()
        {
            ScanInteractables();
        }

        private void Update()
        {
            HandleHoldProgress();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Processes the interaction input state.
        /// </summary>
        /// <param name="isPressed">True if the interaction key is currently pressed.</param>
        public void HandleInteract(bool isPressed)
        {
            if (m_CurrentInteractable == null)
            {
                CancelHold();
                return;
            }

            if (isPressed)
            {
                InitiateInteraction();
            }
            else
            {
                CancelHold();
            }
        }

        #endregion

        #region Private Methods

        private void ScanInteractables()
        {
            if (m_CameraTransform == null)
            {
                return;
            }

            Ray ray = new Ray(m_CameraTransform.position, m_CameraTransform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, m_InteractDistance, m_InteractableLayers))
            {
                if (hit.collider.TryGetComponent(out IInteractable interactable))
                {
                    if (interactable.IsInteractable)
                    {
                        if (m_CurrentInteractable != interactable)
                        {
                            SetCurrentInteractable(interactable);
                        }
                        
                        if (UIManager.Instance != null)
                        {
                            UIManager.Instance.EditInteractionText(interactable.GetInteractionPrompt());
                        }
                        return;
                    }
                }
            }

            ClearCurrentInteractable();
        }

        private void InitiateInteraction()
        {
            if (m_CurrentInteractable == null) return;

            switch (m_CurrentInteractable.InteractionType)
            {
                case InteractionType.Instant:
                case InteractionType.Toggle:
                    m_CurrentInteractable.OnInteract(gameObject);
                    break;

                case InteractionType.Hold:
                    m_HoldStartTime = Time.time;
                    m_IsHolding = true;
                    m_IsInteractionHandled = false;
                    break;
            }
        }

        private void HandleHoldProgress()
        {
            if (!m_IsHolding || m_CurrentInteractable == null || m_CurrentInteractable.InteractionType != InteractionType.Hold)
            {
                return;
            }

            float elapsed = Time.time - m_HoldStartTime;
            float progress = Mathf.Clamp01(elapsed / m_CurrentInteractable.HoldDuration);

            if (UIManager.Instance != null)
            {
                UIManager.Instance.UpdateHoldProgress(progress);
            }

            if (progress >= 1.0f && !m_IsInteractionHandled)
            {
                m_CurrentInteractable.OnInteract(gameObject);
                m_IsInteractionHandled = true;
                
                // Hide progress bar immediately after successful interaction
                if (UIManager.Instance != null)
                {
                    UIManager.Instance.HideHoldProgress();
                }
                
                m_IsHolding = false;
            }
        }

        private void CancelHold()
        {
            if (m_IsHolding)
            {
                if (m_CurrentInteractable != null)
                {
                    m_CurrentInteractable.OnInteractCancel(gameObject);
                }
                
                m_IsHolding = false;
                
                // Start smooth decay instead of instant reset
                if (UIManager.Instance != null)
                {
                    UIManager.Instance.StartProgressDecay();
                }
            }
        }

        private void SetCurrentInteractable(IInteractable interactable)
        {
            CancelHold();
            m_CurrentInteractable = interactable;
        }

        private void ClearCurrentInteractable()
        {
            if (m_CurrentInteractable != null)
            {
                CancelHold();
                m_CurrentInteractable = null;
                
                if (UIManager.Instance != null)
                {
                    UIManager.Instance.ClearInteractionText();
                }
            }
        }

        #endregion
    }
}
