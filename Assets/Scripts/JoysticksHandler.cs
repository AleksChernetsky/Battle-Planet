using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class JoysticksHandler : MonoBehaviour
{
    [SerializeField] private RectTransform _movementJoystickBackGround;
    [SerializeField] private RectTransform _combatJoystickBackGround;

    private Vector2 _movementJoystickStartPosition;
    private Vector2 _combatJoystickStartPosition;

    private Joystick _joystick;

    public Vector3 MoveDirection => _joystick.GamePlay.Movement.ReadValue<Vector2>();
    public Vector3 RotateDirection => _joystick.GamePlay.Combat.ReadValue<Vector2>();

    private void Awake()
    {
        _joystick = new Joystick();
        _joystick.Enable();

        _movementJoystickStartPosition = _movementJoystickBackGround.anchoredPosition;
        _combatJoystickStartPosition = _combatJoystickBackGround.anchoredPosition;
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
            _movementJoystickBackGround.anchoredPosition = touchedFinger.screenPosition;
        }
        else
        {
            _combatJoystickBackGround.anchoredPosition = touchedFinger.screenPosition;
        }
    }
    private void OnJoystickUp(Finger releasedFinger)
    {
        _movementJoystickBackGround.anchoredPosition = _movementJoystickStartPosition;
        _combatJoystickBackGround.anchoredPosition = _combatJoystickStartPosition;
    }    
}