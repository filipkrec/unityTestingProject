using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_SetSpell : MonoBehaviour
{
    public List<C_Box> backpack;

    private void Start()
    {
        backpack[0].spell = new Sp_FreezeTime();
        backpack[1].spell = new Sp_DivineForce();
        backpack[2].spell = new Sp_Push();
    }
}