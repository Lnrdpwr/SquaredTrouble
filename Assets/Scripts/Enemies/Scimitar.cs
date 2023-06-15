using UnityEngine;

public class Scimitar : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeToSelfDestroy;

    private Rigidbody2D _scimitarRigidbody;
    private Vector3 _target;
    private Vector2 _direction;

    private void Start()
    {
        _target = PlayerHealth.Instance.transform.position;//Позиция игрока
        _direction = Vector2.ClampMagnitude(_target - transform.position, _speed);//Направление полёта

        _scimitarRigidbody = GetComponent<Rigidbody2D>();
        _scimitarRigidbody.velocity = _direction;//Задаём скорость

        Invoke("SelfDestroy", _timeToSelfDestroy);
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
