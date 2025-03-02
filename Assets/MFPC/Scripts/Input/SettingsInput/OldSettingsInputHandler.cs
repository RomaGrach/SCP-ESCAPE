using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MFPC.Input.SettingsInput
{
    public class OldSettingsInputHandler : MonoBehaviour, ISettingsInput
    {
        public event Action OnOpenSettings;

        private GameObject _settingsField;

        [SerializeField] // Отображаем в инспекторе, чтобы можно было назначить действие
        private InputActionReference openSettingsAction;

        private void Update()
        {
            // Проверяем, активировано ли действие (например, нажата клавиша Tab)
            if (openSettingsAction != null && openSettingsAction.action.WasPressedThisFrame())
            {
                OnOpenSettings?.Invoke();

                if (OnOpenSettings != null) UpdateUIInput();
            }
        }

        public void SetSettingsField(GameObject settingsField)
        {
            _settingsField = settingsField;
        }

        public void UpdateUIInput()
        {
            Cursor.lockState = (_settingsField.activeSelf)
                ? CursorLockMode.None
                : CursorLockMode.Locked;
        }
    }
}