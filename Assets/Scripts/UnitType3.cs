using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitType3 : MonoBehaviour
{
    void Start()
    {
        UnitSpawning.prefabToSpawn.Add(gameObject);
    }
}
