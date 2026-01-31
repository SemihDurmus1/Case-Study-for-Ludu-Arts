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
        /// Gets the percentage of progress for hold interactions (0 to 1).
        /// </summary>
        float InteractionProgress { get; }

        /// <summary>
        /// Gets whether the object is currently interactable.
        /// </summary>
        bool IsInteractable { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Called when the interaction is triggered.
        /// </summary>
        /// <param name="interactor">The GameObject that initiated the interaction.</param>
        void OnInteract(GameObject interactor);

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
