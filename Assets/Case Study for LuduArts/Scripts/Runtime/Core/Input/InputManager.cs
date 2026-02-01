using UnityEngine;
using LuduArts.InteractionSystem.Runtime.Player.Animation;
using LuduArts.InteractionSystem.Runtime.Player.Movement;
using LuduArts.InteractionSystem.Runtime.Input;
using LuduArts.InteractionSystem.Runtime.Player;

namespace LuduArts.InteractionSystem.Runtime.Core.Input
{
    /// <summary>
    /// Connects input events from SO_InputReader to various player systems.
    /// Acts as a bridge between the data-driven input system and gameplay controllers.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        #region Fields

        [Header("Settings")]
        [SerializeField] private SO_InputReader m_InputReader;

        [Header("Player System References")]
        [SerializeField] private PlayerMovementManager m_PlayerMovement;
        [SerializeField] private InteractionDetector m_InteractionDetector;

        [Header("Visual & Camera References")]
        [SerializeField] private FPSCam m_FpsCam;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            if (m_InputReader == null)
            {
                Debug.LogError("[InputManager] Input Reader is not assigned!");
                return;
            }

            m_InputReader.OnMoveEvent.AddListener(HandleMoveInput);
            m_InputReader.OnSprintEvent.AddListener(HandleSprintInput);
            m_InputReader.OnLookEvent.AddListener(HandleLookInput);
            m_InputReader.OnJumpEvent.AddListener(HandleJumpInput);
            m_InputReader.OnInteractEvent.AddListener(HandleInteractInput);
        }

        private void OnDisable()
        {
            if (m_InputReader == null) return;

            m_InputReader.OnMoveEvent.RemoveListener(HandleMoveInput);
            m_InputReader.OnSprintEvent.RemoveListener(HandleSprintInput);
            m_InputReader.OnLookEvent.RemoveListener(HandleLookInput);
            m_InputReader.OnJumpEvent.RemoveListener(HandleJumpInput);
            m_InputReader.OnInteractEvent.RemoveListener(HandleInteractInput);
        }

        #endregion

        #region Event Handlers

        private void HandleMoveInput(Vector2 moveInput)
        {
            if (m_PlayerMovement != null)
            {
                m_PlayerMovement.MoveInput = moveInput;
            }
        }

        private void HandleSprintInput(bool isSprinting)
        {
            if (m_PlayerMovement != null)
            {
                m_PlayerMovement.IsSprintingInput = isSprinting;
            }
        }

        private void HandleLookInput(Vector2 lookInput)
        {
            if (m_PlayerMovement != null)
            {
                m_PlayerMovement.LookInput = lookInput;
            }

            if (m_FpsCam != null)
            {
                m_FpsCam.LookInput = lookInput;
            }
        }

        private void HandleJumpInput()
        {
            if (m_PlayerMovement != null)
            {
                m_PlayerMovement.HandleJump();
            }
        }

        private void HandleInteractInput(bool isPressed)
        {
            if (m_InteractionDetector != null)
            {
                m_InteractionDetector.HandleInteract(isPressed);
            }
        }

        #endregion
    }
}
