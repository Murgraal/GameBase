using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CombatEncounter
{
    private int[] EntitiesInCombat;
    private Queue<int> combatQueue;
    
    public CombatEncounter(params int[] entityIDs)
    {
        
    }

    public IEnumerator RunEncounter()
    {
        yield return null;
    }

    
}
