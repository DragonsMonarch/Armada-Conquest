using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnSpawnSlot : MonoBehaviour
{
    void Start()
    {
        UnitSpawning.spawningSlots.Add(gameObject);
    }
}
