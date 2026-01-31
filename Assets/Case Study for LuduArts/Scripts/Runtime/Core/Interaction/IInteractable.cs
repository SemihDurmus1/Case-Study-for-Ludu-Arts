using UnityEngine;

namespace LuduArts.InteractionSystem.Runtime.Core
{
    /// <summary>
    /// Interface for all objects that can be interacted with.
    /// Supports Instant, Hold, and Toggle interactions.
    /// </summary>
    public interface IInteractable
    {
        #region Properties

        /// <summary>
        /// Gets the type of interaction this object performs.
        /// </summary>
        InteractionType InteractionType { get; }

        /// <summary>
        /// Gets the duration required for hold interactions in seconds.
        /// </summary>
        float HoldDuration { get; }

        /// <summary>
        /// Gets whether the object is currently interactable.
        /// </summary>
        bool IsInteractable { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Called when the interaction is initiated or triggered.
        /// </summary>
        /// <param name="interactor">The GameObject that initiated the interaction.</param>
        void OnInteract(GameObject interactor);

        /// <summary>
        /// Called when the interaction input is released (primarily for Hold type).
        /// </summary>
        /// <param name="interactor">The GameObject that was interacting.</param>
        void OnInteractCancel(GameObject interactor);

        /// <summary>
        /// Gets the prompt text to display to the player.
        /// </summary>
        /// <returns>A string like "Press E to Open".</returns>
        string GetInteractionPrompt();

        #endregion
    }

    /// <summary>
    /// Enumeration of possible interaction types.
    /// </summary>
    public enum InteractionType
    {
        Instant,
        Hold,
        Toggle
    }
}
