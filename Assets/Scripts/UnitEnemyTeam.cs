using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEnemyTeam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<MeshRenderer>().material.color = Color.red;
        UnitSelect.unit.Add(gameObject);
    }
}
