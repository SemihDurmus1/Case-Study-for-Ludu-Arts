using System;
using UnityEngine;
using LuduArts.InteractionSystem.Runtime.Player.Movement;

namespace LuduArts.InteractionSystem.Runtime.Player.Movement
{
    /// <summary>
    /// Handles camera head bobbing effects based on player movement speed.
    /// Triggers footstep events at the lowest point of the bob cycle.
    /// </summary>
    public class HeadbobManager : MonoBehaviour
    {
        #region Fields

        [Header("References")]
        [SerializeField] private PlayerMovementManager m_MovementManager;

        [Header("Walking Settings")]
        [SerializeField] private float m_WalkingBobbingSpeed = 14f;
        [SerializeField] private float m_BobbingAmount = 0.05f;

        [Header("Sprinting Settings")]
        [SerializeField] private float m_SprintingBobbingSpeed = 18f;
        [SerializeField] private float m_SprintingBobbingAmount = 0.1f;

        [Header("Smoothing")]
        [SerializeField] private float m_TransitionSpeed = 10f;

        private float m_DefaultPosY;
        private float m_Timer;
        private float m_CurrentBobAmount;
        private float m_CurrentBobSpeed;
        private bool m_IsStepTaken;

        #endregion

        #region Events

        /// <summary>
        /// Triggered when a footstep is detected during the head bob cycle.
        /// </summary>
        public event Action OnFootstep;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_DefaultPosY = transform.localPosition.y;
        }

        private void Update()
        {
            HandleBobbing();
            CheckFootstep();
        }

        #endregion

        #region Private Methods

        private void HandleBobbing()
        {
            if (m_MovementManager == null) return;

            Vector3 velocity = new Vector3(m_MovementManager.ActualVelocity.x, 0, m_MovementManager.ActualVelocity.z);
            float speedSqr = velocity.sqrMagnitude;

            float targetBobAmount = 0f;
            float targetBobSpeed = 0f;

            if (speedSqr > 0.1f && m_MovementManager.IsGrounded)
            {
                if (m_MovementManager.IsSprinting())
                {
                    targetBobAmount = m_SprintingBobbingAmount;
                    targetBobSpeed = m_SprintingBobbingSpeed;
                }
                else
                {
                    targetBobAmount = m_BobbingAmount;
                    targetBobSpeed = m_WalkingBobbingSpeed;
                }
            }

            // Interpolate values for smooth transitions between walking/sprinting/stopping
            m_CurrentBobAmount = Mathf.Lerp(m_CurrentBobAmount, targetBobAmount, Time.deltaTime * m_TransitionSpeed);
            m_CurrentBobSpeed = Mathf.Lerp(m_CurrentBobSpeed, targetBobSpeed, Time.deltaTime * m_TransitionSpeed);

            m_Timer += Time.deltaTime * m_CurrentBobSpeed;

            // Reset timer to prevent overflow
            if (m_Timer > Mathf.PI * 4)
            {
                m_Timer -= Mathf.PI * 4;
            }

            if (m_CurrentBobAmount > 0.001f)
            {
                float waveY = Mathf.Sin(m_Timer) * m_CurrentBobAmount;
                float waveX = Mathf.Cos(m_Timer / 2f) * m_CurrentBobAmount * 2f;

                transform.localPosition = new Vector3(waveX, m_DefaultPosY + waveY, transform.localPosition.z);
            }
            else
            {
                Vector3 targetPos = new Vector3(0, m_DefaultPosY, transform.localPosition.z);
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * m_TransitionSpeed);
            }
        }

        private void CheckFootstep()
        {
            // The lowest point of the sine wave (Mathf.Sin(m_Timer) close to -1) indicates a foot landing
            if (Mathf.Sin(m_Timer) < -0.95f)
            {
                if (!m_IsStepTaken)
                {
                    OnFootstep?.Invoke();
                    m_IsStepTaken = true;
                }
            }
            else
            {
                m_IsStepTaken = false;
            }
        }

        #endregion
    }
}