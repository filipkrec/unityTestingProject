using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float[] position;
    public SaveData (C_BasicMovement mainCharacter)
    {
        position = new float[3];
        position[0] = mainCharacter.transform.position.x;
        position[1] = mainCharacter.transform.position.y;
        position[2] = mainCharacter.transform.position.z;
    }
}
