using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FillSlot : MonoBehaviour
{
    public int slot;
    
    public C_Box fillSlot(C_Box box)
    //return null if slot empty
    {
        C_Box toReturn = Globals.Player.getSlot(slot);
        Globals.Player.setSlot(slot, box);
        return toReturn;
    }

    public C_Box getBoxFromSlot()
    {
        return Globals.Player.getSlot(slot);
    }
}
