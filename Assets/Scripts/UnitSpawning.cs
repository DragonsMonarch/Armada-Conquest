using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawning : MonoBehaviour
{
    public static List<GameObject> spawningAreas;
    public static List<GameObject> prefabToSpawn;
    public static List<GameObject> spawningSlots;

    GameObject ownSpawnArea;
    GameObject enemySpawnArea;
    GameObject slotForSpawning;


    void Awake()
    {
        spawningAreas = new List<GameObject>();
        prefabToSpawn = new List<GameObject>();
        spawningSlots = new List<GameObject>();
    }

    void Start()
    {
        //Instantiate(gameObject, Input.mousePosition, Quaternion.identity);
        
        for (int i = 0; i < spawningAreas.Count; i++)
        {
            if (spawningAreas[i].GetComponent<OwnSpawnArea>())
            {
                ownSpawnArea = spawningAreas[i];
            } 
            // else
            // {
            //     enemySpawnArea = spawningAreas[i];
            // }
        }
        
    }

    void Update()
    {
        //Debug.Log(spawningAreas.Count);
    
    }

    void SpawnUnit(int type)
    {
        Instantiate(prefabToSpawn[type], slotForSpawning.transform.position + Vector3.up, Quaternion.identity);
    }

    // void ChooseSlotForSpawning()
    // {
    //     for (int i = 0; i < spawningSlots.Count; i++)
    //     {
    //         slotForSpawning = spawningSlots[]
    //     }
    // }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Добавить тип1"))
        {
            slotForSpawning = spawningSlots[0];
            SpawnUnit(0);
            spawningSlots.Remove(spawningSlots[0]);
            Debug.Log(spawningSlots.Count);
        }
        if (GUI.Button(new Rect(10, 50, 100, 30), "Добавить тип2"))
        {
            slotForSpawning = spawningSlots[0];
            SpawnUnit(1);
            spawningSlots.Remove(spawningSlots[0]);
            Debug.Log(spawningSlots.Count);
        }
        if (GUI.Button(new Rect(10, 90, 100, 30), "Добавить тип2"))
        {
            slotForSpawning = spawningSlots[0];
            SpawnUnit(2);
            spawningSlots.Remove(spawningSlots[0]);
            Debug.Log(spawningSlots.Count);
        }
    }
}
