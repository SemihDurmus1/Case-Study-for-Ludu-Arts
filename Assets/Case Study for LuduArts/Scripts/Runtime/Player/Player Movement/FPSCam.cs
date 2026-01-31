using UnityEngine;

namespace LuduArts.InteractionSystem.Runtime.Player.Movement
{
    /// <summary>
    /// Handles the vertical rotation (pitch) of the first-person camera.
    /// Manages cursor locking and look input application.
    /// </summary>
    public class FPSCam : MonoBehaviour
    {
        #region Fields

        [Header("Settings")]
        [SerializeField] private SO_MovementSettings m_MovementSettings;

        [Header("State")]
        [SerializeField] private float m_Pitch;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current look input (X for horizontal, Y for vertical).
        /// </summary>
        public Vector2 LookInput { get; set; }

        #endregion

        #region Unity Methods

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            // Get initial vertical rotation
            m_Pitch = transform.localEulerAngles.x;
            
            // Normalize pitch if it's over 180 (Unity rotation quirk)
            if (m_Pitch > 180f)
            {
                m_Pitch -= 360f;
            }
        }

        private void LateUpdate()
        {
            HandleCameraRotation();
        }

        #endregion

        #region Private Methods

        private void HandleCameraRotation()
        {
            if (m_MovementSettings == null)
            {
                return;
            }

            // Mouse Y input rotates camera on the X axis
            float mouseY = LookInput.y * m_MovementSettings.YSensitivity * Time.deltaTime;

            m_Pitch -= mouseY;
            m_Pitch = Mathf.Clamp(m_Pitch, m_MovementSettings.MinPitch, m_MovementSettings.MaxPitch);

            // Apply local rotation (PlayerMovementManager handles the body's Y rotation)
            transform.localRotation = Quaternion.Euler(m_Pitch, 0f, 0f);
        }

        #endregion
    }
}