using UnityEngine;

public interface IEnemyState
{
    void Enter(Character enemy);
    void Execute();
    void Exit();
    void OnCollisionEnter(Collider2D other);

}
