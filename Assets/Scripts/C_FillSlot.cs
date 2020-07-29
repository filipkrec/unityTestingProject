using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FillSlot : MonoBehaviour
{
    public C_FightPlayer player;
    public int slot;

    
    public C_Box fillSlot(C_Box box)
    //return null if slot empty
    {
        C_Box toReturn = player.getSlot(slot);
        player.setSlot(slot, box);
        return toReturn;
    }

    public C_Box getBoxFromSlot()
    {
        return player.getSlot(slot);
    }
}
