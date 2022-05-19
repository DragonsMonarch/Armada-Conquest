using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnArea : MonoBehaviour
{
    void Start()
    {
        UnitSpawning.spawningAreas.Add(gameObject);
    }
}
