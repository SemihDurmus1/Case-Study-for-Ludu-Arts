using UnityEngine;

namespace LuduArts.InteractionSystem.Runtime.Player.Movement
{
    /// <summary>
    /// Configuration asset for player movement parameters.
    /// Used by PlayerMovementManager to define speeds, jump force, and rotation sensitivity.
    /// </summary>
    [CreateAssetMenu(fileName = "SO_MovementSettings", menuName = "Ludu Arts/Player/Movement Settings")]
    public class SO_MovementSettings : ScriptableObject
    {
        #region Fields

        [Header("Movement Settings")]
        [SerializeField] private float m_BaseSpeed = 4f;
        [SerializeField] private float m_SprintMultiplier = 2f;
        [SerializeField] private float m_Acceleration = 10f;

        [Header("Rotation Settings")]
        [SerializeField] private float m_XSensitivity = 10f;
        [SerializeField] private float m_YSensitivity = 15f;
        [SerializeField] private float m_MinPitch = -80f;
        [SerializeField] private float m_MaxPitch = 80f;

        [Header("Jump Settings")]
        [SerializeField] private float m_JumpForce = 5f;
        [SerializeField] private LayerMask m_GroundMask;
        [SerializeField] private float m_GroundDistance = 0.4f;

        #endregion

        #region Properties

        public float BaseSpeed => m_BaseSpeed;
        public float SprintMultiplier => m_SprintMultiplier;
        public float Acceleration => m_Acceleration;
        public float XSensitivity => m_XSensitivity;
        public float YSensitivity => m_YSensitivity;
        public float MinPitch => m_MinPitch;
        public float MaxPitch => m_MaxPitch;
        public float JumpForce => m_JumpForce;
        public LayerMask GroundMask => m_GroundMask;
        public float GroundDistance => m_GroundDistance;

        #endregion
    }
}