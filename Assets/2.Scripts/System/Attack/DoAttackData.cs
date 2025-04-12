using UnityEngine;

public class DoAttackData
{
    public float speed = 0;
    public Vector2 direction = Vector2.zero;
    public Transform transform;

    public DoAttackData()
    {
        speed = 0;
        direction = Vector2.zero;
        transform = null;
    }

    public DoAttackData(float _speed, Vector2 _direction, Transform _transform)
    {
        speed = _speed;
        direction = _direction;
        transform = _transform;
    }
}