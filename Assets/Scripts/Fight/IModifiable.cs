using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifiable
{
    void addModifier(C_Modifier modifier);
    void removeModifier(C_Modifier modifier);
    void modifyValues();
    void unmodifyValues();

    C_Modifier getModifier<T>();
}
