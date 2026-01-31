using UnityEngine;
using LuduArts.InteractionSystem.Runtime.Player.Movement;

namespace LuduArts.InteractionSystem.Runtime.Player.Audio
{
    /// <summary>
    /// Listens for footstep events from HeadbobManager and plays appropriate audio.
    /// Handles surface detection via raycasts.
    /// </summary>
    public class FootstepManager : MonoBehaviour
    {
        #region Fields

        [Header("References")]
        [SerializeField] private HeadbobManager m_HeadbobManager;
        [SerializeField] private Transform m_FootTransform;

        [Header("Audio Settings")]
        [SerializeField] private AudioClip m_DefaultStepSound;
        [SerializeField] private AudioSource m_AudioSource;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            if (m_HeadbobManager != null)
            {
                m_HeadbobManager.OnFootstep += PlayFootstep;
            }
        }

        private void OnDisable()
        {
            if (m_HeadbobManager != null)
            {
                m_HeadbobManager.OnFootstep -= PlayFootstep;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Detects the surface beneath the player and plays a footstep sound.
        /// </summary>
        private void PlayFootstep()
        {
            if (m_FootTransform == null || m_AudioSource == null)
            {
                return;
            }

            // Raycast down to detect the surface material/layer
            if (Physics.Raycast(m_FootTransform.position, Vector3.down, out RaycastHit hit, 1.5f))
            {
                if (m_DefaultStepSound != null)
                {
                    m_AudioSource.PlayOneShot(m_DefaultStepSound);
                }
            }
        }

        #endregion
    }
}