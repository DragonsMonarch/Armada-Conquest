using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitOwnTeam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<MeshRenderer>().material.color = Color.blue;
        UnitSelect.unit.Add(gameObject);
    }
}