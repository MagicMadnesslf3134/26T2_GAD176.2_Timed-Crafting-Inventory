using TMPro.EditorUtilities;
using UnityEngine;

public class Weapon: Item
{
    protected override void UseItem()
    {
        Attack();
    }
    public virtual void Attack()
    {

    }
    
}

