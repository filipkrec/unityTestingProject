using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FightPlayer : MonoBehaviour
{
    public int numberOfSlots; 
    public C_Box[] slots;
    public int numberOfSpells;
    public C_Spell[] spells;
    public int pushForce;
    public int pushDefence;
    public int timeSpeedup;
    public int timeSlowdown;
    public int cooldownSpeedup;
    public int cooldownSlowdown;
    public int spellEffectivenessUp;
    public int spellEffectivenessDown;

    // Start is called before the first frame update
    void Start()
    {
        numberOfSlots = 5;
        slots = new C_Box[numberOfSlots];
    }

    public C_Box getSlot(int i)
    {
        if (i < numberOfSlots)
            return slots[i];
        else
            return null;
    }

    public void setSlot(int i, C_Box box)
    {
        slots[i] = box;
    }
}
