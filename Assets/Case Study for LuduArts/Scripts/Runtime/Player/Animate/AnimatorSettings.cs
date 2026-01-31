using UnityEngine;

namespace LuduArts.InteractionSystem.Runtime.Player.Animation
{
    /// <summary>
    /// Configuration asset for animator parameters.
    /// Stores runtime values for animation blend trees and states.
    /// </summary>
    [CreateAssetMenu(fileName = "AnimatorSettings", menuName = "Ludu Arts/Player/Animator Settings")]
    public class AnimatorSettings : ScriptableObject
    {
        #region Fields

        [Header("Runtime State")]
        [SerializeField] private float m_MoveSpeed;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current movement speed for animations.
        /// </summary>
        public float MoveSpeed 
        { 
            get => m_MoveSpeed; 
            set => m_MoveSpeed = value; 
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the current speed value.
        /// </summary>
        /// <param name="speed">The new speed value.</param>
        public void SetSpeed(float speed)
        {
            m_MoveSpeed = speed;
        }

        #endregion
    }
}
