using LuduArts.InteractionSystem.Runtime.Core;
using UnityEngine;
using UnityEngine.Events;

namespace LuduArts.InteractionSystem.Runtime.Interactables
{
    /// <summary>
    /// A switch or lever that can trigger other objects via UnityEvents.
    /// Uses Toggle interaction to maintain an ON/OFF state.
    /// </summary>
    public class SwitchInteractable : BaseInteractable
    {
        #region Fields

        [Header("Switch Settings")]
        [SerializeField] private UnityEvent m_OnSwitchOn;
        [SerializeField] private UnityEvent m_OnSwitchOff;

        [Header("State")]
        [SerializeField] private bool m_IsOn = false;

        #endregion

        #region Unity Methods

        private void Start()
        {
            m_InteractionType = InteractionType.Toggle;
            UpdatePrompt();
        }

        #endregion

        #region Public Methods

        public override void OnInteract(GameObject interactor)
        {
            m_IsOn = !m_IsOn;
            
            if (m_IsOn)
            {
                m_OnSwitchOn?.Invoke();
            }
            else
            {
                m_OnSwitchOff?.Invoke();
            }

            UpdatePrompt();
            Debug.Log($"[Switch] Switch is now {(m_IsOn ? "ON" : "OFF")}");
        }

        #endregion

        #region Private Methods

        private void UpdatePrompt()
        {
            m_InteractionPrompt = m_IsOn ? "Press E to Turn OFF" : "Press E to Turn ON";
        }

        #endregion
    }
}
