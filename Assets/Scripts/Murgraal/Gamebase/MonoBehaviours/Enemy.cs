using Murgraal.Gamebase;
using UnityEngine;

public class Enemy : Entity
{
    protected override void Init()
    {
        
    }
    protected  override  void Execute()
    {
        
    }
}

public static class EnemyProcesses
{
    public static void FollowTarget()
    {
        
    }

    public static void TryToShootAgainstTarget(int targetGuid)
    {
        var target = GameSession.GetEntityLocationData(targetGuid);
    }
}