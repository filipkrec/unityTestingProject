using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifiable
{
    void AddModifier(C_Modifier modifier);
    void RemoveModifier(C_Modifier modifier);
    void ModifyValues();
    void UnmodifyValues();

    C_Modifier GetModifier<T>();
}
