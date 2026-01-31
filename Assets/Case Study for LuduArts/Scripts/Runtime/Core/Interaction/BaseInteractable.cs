using UnityEngine;

namespace LuduArts.InteractionSystem.Runtime.Core
{
    /// <summary>
    /// Base class for all interactable objects in the world.
    /// Provides common fields and logic for different interaction types.
    /// </summary>
    public abstract class BaseInteractable : MonoBehaviour, IInteractable
    {
        #region Fields

        [Header("Base Interaction Settings")]
        [SerializeField] protected InteractionType m_InteractionType = InteractionType.Instant;
        [SerializeField] protected float m_HoldDuration = 1.0f;
        [SerializeField] protected bool m_IsInteractable = true;
        [SerializeField] protected string m_InteractionPrompt = "Press E to Interact";

        #endregion

        #region Properties

        public virtual InteractionType InteractionType => m_InteractionType;
        public virtual float HoldDuration => m_HoldDuration;
        public virtual bool IsInteractable => m_IsInteractable;

        #endregion

        #region Public Methods

        public virtual string GetInteractionPrompt()
        {
            return m_InteractionPrompt;
        }

        public abstract void OnInteract(GameObject interactor);

        public virtual void OnInteractCancel(GameObject interactor)
        {
            // Optional: Handle release for Hold type if needed by subclasses
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Logs an interaction event for debugging.
        /// </summary>
        protected virtual void LogInteraction(GameObject interactor)
        {
            Debug.Log($"[Interaction] {interactor.name} interacted with {gameObject.name}");
        }

        #endregion
    }
}
