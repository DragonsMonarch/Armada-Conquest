using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnSpawnArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnitSpawning.spawningAreas.Add(gameObject);
    }
}
