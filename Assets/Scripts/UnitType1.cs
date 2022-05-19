using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitType1 : MonoBehaviour
{
    void Start()
    {
        UnitSpawning.prefabToSpawn.Add(gameObject);
    }
}
