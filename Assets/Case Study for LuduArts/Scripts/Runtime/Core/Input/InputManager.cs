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
        [SerializeField] private PlayerInteractionManager m_InteractionManager;

        [Header("Visual & Camera References")]
        [SerializeField] private FPSCam m_FpsCam;
        [SerializeField] private PlayerAnimateManager m_AnimateManager;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            if (m_InputReader == null)
            {
                Debug.LogError("[InputManager] Input Reader is not assigned!");
                return;
            }

            m_InputReader.OnMoveEvent.AddListener(HandleMove);
            m_InputReader.OnSprintEvent.AddListener(HandleSprint);
            m_InputReader.OnLookEvent.AddListener(HandleLook);
            m_InputReader.OnJumpEvent.AddListener(HandleJump);
            m_InputReader.OnInteractEvent.AddListener(HandleInteract);
            m_InputReader.OnAttackEvent.AddListener(HandleAttack);
        }

        private void OnDisable()
        {
            if (m_InputReader == null) return;

            m_InputReader.OnMoveEvent.RemoveListener(HandleMove);
            m_InputReader.OnSprintEvent.RemoveListener(HandleSprint);
            m_InputReader.OnLookEvent.RemoveListener(HandleLook);
            m_InputReader.OnJumpEvent.RemoveListener(HandleJump);
            m_InputReader.OnInteractEvent.RemoveListener(HandleInteract);
            m_InputReader.OnAttackEvent.RemoveListener(HandleAttack);
        }

        #endregion

        #region Event Handlers
        /// <summary>
        /// These scripts are apply the input values comes from the Input Reader to the Players itself.
        /// So they send the inputs to the PlayerMovement script.
        /// </summary>
        /// <param name="moveInput"></param>
        private void HandleMove(Vector2 moveInput)
        {
            if (m_PlayerMovement != null) m_PlayerMovement.MoveInput = moveInput;
        }

        private void HandleSprint(bool isSprinting)
        {
            if (m_PlayerMovement != null) m_PlayerMovement.IsSprintingInput = isSprinting;
        }

        private void HandleLook(Vector2 lookInput)
        {
            if (m_PlayerMovement != null) m_PlayerMovement.LookInput = lookInput;
            if (m_FpsCam != null) m_FpsCam.LookInput = lookInput;
        }

        private void HandleJump()
        {
            if (m_PlayerMovement != null) m_PlayerMovement.HandleJump();
        }

        private void HandleInteract()
        {
            if (m_InteractionManager != null) m_InteractionManager.HandleInteract();
        }

        private void HandleAttack(bool isPressed)
        {
            
        }

        #endregion
    }
}
