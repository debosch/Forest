using UnityEngine;

public class GreenMonster : Character
{
    private IEnemyState currentState;

    override protected void Start()
    {
        base.Start();
        ChangeState(new PatrolState());
    }

    void Update()
    {
        currentState.Execute();
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
            currentState.Exit();
        currentState = newState;
        currentState.Enter(this);
    }
}
