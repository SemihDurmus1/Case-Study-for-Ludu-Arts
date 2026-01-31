using UnityEngine;

namespace LuduArts.InteractionSystem.Runtime.Player.Animation
{
    /// <summary>
    /// Synchronizes the animator with runtime states stored in AnimatorSettings.
    /// Handles triggering common animations like attacks.
    /// </summary>
    public class PlayerAnimateManager : MonoBehaviour
    {
        #region Fields

        [Header("References")]
        [SerializeField] private Animator m_Animator;
        [SerializeField] private AnimatorSettings m_AnimatorSettings;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (m_Animator == null)
            {
                m_Animator = GetComponent<Animator>();
            }

            if (m_Animator == null)
            {
                Debug.LogError("[PlayerAnimateManager] Animator component missing!");
            }
        }

        private void Update()
        {
            if (m_Animator != null && m_AnimatorSettings != null)
            {
                SetSpeed(m_AnimatorSettings.MoveSpeed);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Manually sets the speed parameter in the animator.
        /// </summary>
        /// <param name="speed">The speed value to set.</param>
        public void SetSpeed(float speed)
        {
            if (m_Animator != null)
            {
                m_Animator.SetFloat("speed", speed);
            }
        }

        /// <summary>
        /// Triggers the attack animation.
        /// </summary>
        public void TriggerAttack()
        {
            if (m_Animator != null)
            {
                m_Animator.SetTrigger("Attack");
            }
        }

        #endregion
    }
}