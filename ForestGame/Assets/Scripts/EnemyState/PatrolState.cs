using UnityEngine;


public class PatrolState : IEnemyState
{
    public void Enter(Character enemy)
    {
        
    }

    public void Execute()
    {
        Debug.Log("I'm patrolling");
    }

    public void Exit()
    {
        
    }

    public void OnCollisionEnter(Collider2D other)
    {
        
    }
}
