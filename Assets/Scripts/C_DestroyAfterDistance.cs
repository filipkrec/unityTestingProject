using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_DestroyAfterDistance : MonoBehaviour
{
    private Vector3 startingPosition;
    private float maxDistance;

    public void Instantiate(Vector3 _startingPosition, float _maxDistance)
    {
        startingPosition = _startingPosition;
        maxDistance = _maxDistance;
    }


    private void Update()
    {
        if(Vector3.Distance(startingPosition,transform.position) > maxDistance)
            Destroy(gameObject);
    }

}
