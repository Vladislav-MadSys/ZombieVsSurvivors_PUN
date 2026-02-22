using Fusion;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Enemy))]
public class EnemyMovement : NetworkBehaviour
{
    [SerializeField] private float Speed = 5;

    private Transform _target;
    private Enemy _enemy;
    private Rigidbody2D _rb;
    
    
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public override void FixedUpdateNetwork()
    {
        if (_target == null)
        {
            if (_enemy.Target == default)
            {
                _enemy.UpdateTarget();
                _target = _enemy.GetTarget();
            }
        }

        if (_target != null)
        {
            _rb.MovePosition(Vector3.MoveTowards(transform.position, _target.position, Speed * Time.fixedDeltaTime));
            Debug.Log("Move To Player!");
        }
        else
        {
            Debug.Log("No Player!");
        }
    }
}
