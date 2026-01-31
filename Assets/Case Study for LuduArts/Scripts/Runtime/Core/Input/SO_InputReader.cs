using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace LuduArts.InteractionSystem.Runtime.Input
{
    /// <summary>
    /// -ScriptableObject that acts as a central hub for all player input events.
    /// -Its okay for single player games but we cannot use it in a multiplayer game because if we centralize the input,
    /// then every player will receive the same inputs. So there would've been a mess.
    /// -Perform methods connects to the Input Actions, and set the actions values to the events.
    /// -InputManager scripts listens the Events.
    /// 
    /// Setup:
    /// You should set the input references to this scripts variables on the inspector. NOT ON THE SCRIPTABLEOBJECT ASSET!
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "SO_InputReader", menuName = "Ludu Arts/Input/Input Reader")]
    public class SO_InputReader : ScriptableObject
    {
        #region Fields

        [Header("Movement Actions")]
        [SerializeField] private InputActionReference m_MoveAction;
        [SerializeField] private InputActionReference m_SprintAction;
        [SerializeField] private InputActionReference m_LookAction;
        [SerializeField] private InputActionReference m_JumpAction;

        [Header("Interaction Actions")]
        [SerializeField] private InputActionReference m_InteractAction;

        [Header("Combat Actions")]
        [SerializeField] private InputActionReference m_AttackAction;

        #endregion

        #region Events
        
        /// <summary> Triggered when the related inputs are performed. </summary>
        public UnityEvent<Vector2> OnMoveEvent;
        public UnityEvent<bool> OnSprintEvent;
        public UnityEvent<Vector2> OnLookEvent;
        public UnityEvent OnJumpEvent;
        public UnityEvent<bool> OnInteractEvent;
        public UnityEvent<bool> OnAttackEvent;
        public UnityEvent OnReloadEvent;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            Subscribe(m_MoveAction, OnMovePerformed);
            Subscribe(m_SprintAction, OnSprintPerformed);
            Subscribe(m_LookAction, OnLookPerformed);
            Subscribe(m_JumpAction, OnJumpPerformed);
            Subscribe(m_InteractAction, OnInteractPerformed);
            Subscribe(m_AttackAction, OnAttackPerformed);
        }

        private void OnDisable()
        {
            Unsubscribe(m_MoveAction, OnMovePerformed);
            Unsubscribe(m_SprintAction, OnSprintPerformed);
            Unsubscribe(m_LookAction, OnLookPerformed);
            Unsubscribe(m_JumpAction, OnJumpPerformed);
            Unsubscribe(m_InteractAction, OnInteractPerformed);
            Unsubscribe(m_AttackAction, OnAttackPerformed);
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// This method subscribes to the performed and canceled events of the given InputActionReference.
        /// </summary>
        /// <param name="actionRef"></param>
        /// <param name="callback"></param>
        private void Subscribe(InputActionReference actionRef, Action<InputAction.CallbackContext> callback)
        {
            if (actionRef == null || actionRef.action == null) return;

            actionRef.action.Enable();
            actionRef.action.performed += callback;
            actionRef.action.canceled += callback;
        }

        /// <summary>
        /// This method unsubscribes from the performed and canceled events of the given InputActionReference.
        /// </summary>
        /// <param name="actionRef"></param>
        /// <param name="callback"></param>
        private void Unsubscribe(InputActionReference actionRef, Action<InputAction.CallbackContext> callback)
        {
            if (actionRef == null || actionRef.action == null) return;

            actionRef.action.performed -= callback;
            actionRef.action.canceled -= callback;
            actionRef.action.Disable();
        }

        /// <summary>
        /// This script handles the move input action. And the rest of the methods are do the same job at the referenced inputs.
        /// </summary>
        /// <param name="ctx"></param>
        private void OnMovePerformed(InputAction.CallbackContext ctx)
        {
            OnMoveEvent?.Invoke(ctx.ReadValue<Vector2>());
        }

        private void OnSprintPerformed(InputAction.CallbackContext ctx)
        {
            OnSprintEvent?.Invoke(ctx.performed);
        }

        private void OnLookPerformed(InputAction.CallbackContext ctx)
        {
            OnLookEvent?.Invoke(ctx.ReadValue<Vector2>());
        }

        private void OnJumpPerformed(InputAction.CallbackContext ctx)
        {
            if (ctx.performed) OnJumpEvent?.Invoke();
        }

        private void OnInteractPerformed(InputAction.CallbackContext ctx)
        {
            if (ctx.performed) OnInteractEvent?.Invoke(true);
            else if (ctx.canceled) OnInteractEvent?.Invoke(false);
        }

        private void OnAttackPerformed(InputAction.CallbackContext ctx)
        {
            if (ctx.performed) OnAttackEvent?.Invoke(true);
            else if (ctx.canceled) OnAttackEvent?.Invoke(false);
        }

        #endregion
    }
}
