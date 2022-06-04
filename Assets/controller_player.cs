using UnityEngine;

public class controller_player : MonoBehaviour
{
    public float SmoothMoving = 4f;

    private InputActions_player _PlayerInputActions;
    private Animator _Animator;
    
    private Vector2 TouchPosition;
    private float y;
    private Vector2 OldTouchPosition = new Vector2(0, 0);

    private void Awake()
    {
        _Animator = GetComponent<Animator>();
        
        _PlayerInputActions = new InputActions_player();
        _PlayerInputActions.Enable();
        
        y = this.transform.position.y;
    }

    private void FixedUpdate()
    {
        TouchPosition = _PlayerInputActions.Player.Move.ReadValue<Vector2>();
        
        if (TouchPosition != Vector2.zero)
        {
            Move();
            SetAnimation();
        }
    }

    private void Move()
    {
        Vector3 ScreenCoordinates 
            = new Vector3(TouchPosition.x, TouchPosition.y, Camera.main.nearClipPlane);
        Vector3 WorldCoordinates = Camera.main.ScreenToWorldPoint(ScreenCoordinates);
        WorldCoordinates.z = 0;

        Vector3 SmoothPosition = Vector3.Lerp(transform.position, 
            WorldCoordinates, SmoothMoving * Time.fixedDeltaTime);
        transform.position = SmoothPosition;
    }

    private void SetAnimation()
    {
        Vector2 Offset = TouchPosition - OldTouchPosition;
        if (Offset != Vector2.zero)
        {
            if (Offset.y > 0.1) 
                _Animator.SetInteger("animation_number", 0);
            if (Offset.y < -0.1) 
                _Animator.SetInteger("animation_number", 1);
            if (Offset.x > 5) 
                _Animator.SetInteger("animation_number", 2);
            if (Offset.x < -5) 
                _Animator.SetInteger("animation_number", 3);
        }
        else _Animator.SetInteger("animation_number", 0);

        OldTouchPosition = TouchPosition;
    }
}