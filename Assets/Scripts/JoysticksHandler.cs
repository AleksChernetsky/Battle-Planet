using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class JoysticksHandler : MonoBehaviour
{
    [SerializeField] private RectTransform _movementJoystickBG;
    [SerializeField] private RectTransform _rotateJoystickBG;

    private Vector2 _movementJoystickStartPosition;
    private Vector2 _rotateJoystickStartPosition;

    private Joystick _joystick;

    public Vector3 MoveDirection => _joystick.GamePlay.Movement.ReadValue<Vector2>();
    public Vector3 RotateDirection => _joystick.GamePlay.Combat.ReadValue<Vector2>();

    private void Awake()
    {
        _joystick = new Joystick();
        _joystick.Enable();

        _movementJoystickStartPosition = _movementJoystickBG.anchoredPosition;
        _rotateJoystickStartPosition = _rotateJoystickBG.anchoredPosition;
    }

    //-----------------------------------------------------------------------------------------------------------------

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += OnJoystickDown;
        ETouch.Touch.onFingerUp += OnJoystickUp;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= OnJoystickDown;
        ETouch.Touch.onFingerUp -= OnJoystickUp;
        EnhancedTouchSupport.Disable();
    }

    //-----------------------------------------------------------------------------------------------------------------

    private void OnJoystickDown(Finger touchedFinger)
    {
        if (touchedFinger.screenPosition.x <= Screen.width / 2)
        {
            _movementJoystickBG.anchoredPosition = touchedFinger.screenPosition;
        }
        else
        {
            _rotateJoystickBG.anchoredPosition = touchedFinger.screenPosition;
        }
    }
    private void OnJoystickUp(Finger releasedFinger)
    {
        _movementJoystickBG.anchoredPosition = _movementJoystickStartPosition;
        _rotateJoystickBG.anchoredPosition = _rotateJoystickStartPosition;
    }    
}