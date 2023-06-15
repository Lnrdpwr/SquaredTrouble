using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private PlayerAnimations _playerAnimations;

    private Rigidbody2D _playerRigidbody;
    private bool _isMoving;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Vector2 movementDirection = new Vector2(horizontalMovement, verticalMovement) * _speed;
        _playerRigidbody.velocity = Vector2.ClampMagnitude(movementDirection, _speed);//ClampMagnitude возвращает новый вектор с таким же направлением и с заданной длиной

        if(!_isMoving && movementDirection.magnitude != 0)//Если игрок движется
        {
            _isMoving = true;
            _playerAnimations.SetMovingAnimation(_isMoving);
        }
        else if(_isMoving && movementDirection.magnitude == 0)//Если игрок стоит
        {
            _isMoving = false;
            _playerAnimations.SetMovingAnimation(_isMoving);
        }
    }
}
