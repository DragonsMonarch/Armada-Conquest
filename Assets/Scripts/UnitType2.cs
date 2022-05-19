using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitType2 : MonoBehaviour
{
    void Start()
    {
        UnitSpawning.prefabToSpawn.Add(gameObject);
    }
}
