namespace Misc
{
    public enum PhaseStateType
    {
        None,
        Input,
        Decision,
        EnemyTurn,
        End,
        Idle
    }
    
    public enum ActionType
    {
        None,
        OnGameStarted,
        OnHeroDead,
        OnEnemyDead,
        OnLevelFinished,
    }
    
    public enum FunctionType
    {
        None,
        GetEnemyDead,
        GetRandomHero, 
        IsAllEnemiesDead,
        IsAllHeroesDead,
    }
    
}