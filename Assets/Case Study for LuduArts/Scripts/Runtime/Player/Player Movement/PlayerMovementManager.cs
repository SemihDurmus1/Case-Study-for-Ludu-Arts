using LuduArts.InteractionSystem.Runtime.Player.Animation;
using UnityEngine;

namespace LuduArts.InteractionSystem.Runtime.Player.Movement
{
    /// <summary>
    /// Handles the physical movement and rotation of the player.
    /// Manages ground checking, sprinting, and jump logic.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementManager : MonoBehaviour
    {
        #region Fields

        [Header("References")]
        [SerializeField] private SO_MovementSettings m_MovementSettings;
        [SerializeField] private AnimatorSettings m_AnimatorSettings;
        [SerializeField] private Transform m_GroundCheckPoint;
        [SerializeField] private Transform m_CameraTransform;

        [Header("State")]
        [SerializeField] private bool m_IsGrounded;
        [SerializeField] private float m_CurrentSpeed;

        private Rigidbody m_Rigidbody;

        #endregion


        #region Properties
        /// <summary> SO_InputReader datas sets these properties. </summary>
        
        public Vector2 MoveInput { get; set; }//WASD

        public Vector2 LookInput { get; set; }//Mouse

        public bool IsSprintingInput { get; set; }//Checks for sprint key held

        /// <summary> Gets the current physical velocity of the player. </summary>
        public Vector3 ActualVelocity => m_Rigidbody != null ? m_Rigidbody.linearVelocity : Vector3.zero;

        public bool IsGrounded => m_IsGrounded;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            
            if (m_CameraTransform == null && Camera.main != null)
            {
                m_CameraTransform = Camera.main.transform;
            }
        }

        private void LateUpdate()
        {
            CheckGrounded();
            HandleRotation();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        #endregion

        #region Public Methods

        public void HandleJump()
        {
            CheckGrounded();
            
            if (m_IsGrounded)
            {
                // Reset vertical velocity for consistent jump height
                m_Rigidbody.linearVelocity = new Vector3(m_Rigidbody.linearVelocity.x, 0f, m_Rigidbody.linearVelocity.z);
                m_Rigidbody.AddForce(Vector3.up * m_MovementSettings.JumpForce, ForceMode.Impulse);
            }
        }

        /// <summary>
        /// Determines if the player is currently in a sprinting state.
        /// Returns true if held sprint and moving forward.
        /// </summary>
        public bool IsSprinting()
        {
            return IsSprintingInput && MoveInput.y > 0.1f;
        }

        #endregion

        #region Private Methods

        private void CheckGrounded()
        {
            if (m_GroundCheckPoint == null || m_MovementSettings == null)
            {
                return;
            }

            m_IsGrounded = Physics.CheckSphere(
                m_GroundCheckPoint.position, 
                m_MovementSettings.GroundDistance, 
                m_MovementSettings.GroundMask
            );
        }

        private void HandleMovement()
        {
            if (m_MovementSettings == null)
            {
                return;
            }

            Vector3 moveDirection = transform.forward * MoveInput.y + transform.right * MoveInput.x;
            moveDirection.Normalize();

            float targetSpeed = IsSprinting() 
                ? m_MovementSettings.BaseSpeed * m_MovementSettings.SprintMultiplier 
                : m_MovementSettings.BaseSpeed;

            if (MoveInput == Vector2.zero)
            {
                targetSpeed = 0f;
            }

            m_CurrentSpeed = Mathf.Lerp(
                m_CurrentSpeed, 
                targetSpeed, 
                m_MovementSettings.Acceleration * Time.fixedDeltaTime
            );

            Vector3 appliedVelocity = moveDirection * m_CurrentSpeed;
            m_Rigidbody.linearVelocity = new Vector3(appliedVelocity.x, m_Rigidbody.linearVelocity.y, appliedVelocity.z);

            // Update animator settings if available
            if (m_AnimatorSettings != null)
            {
                m_AnimatorSettings.MoveSpeed = m_CurrentSpeed;
            }
        }

        private void HandleRotation()
        {
            if (m_MovementSettings == null)
            {
                return;
            }

            float mouseX = LookInput.x * m_MovementSettings.XSensitivity * Time.deltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0f, mouseX, 0f);
            m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
        }

        #endregion
    }
}