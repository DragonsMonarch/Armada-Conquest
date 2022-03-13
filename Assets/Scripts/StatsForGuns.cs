using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StatsForGuns : MonoBehaviour
{
    //урон турелей 
    public int minGunATK = 10;
    public int maxGunATK = 20;
    //итоговый урон между минимальным и максиамльным значением
    public int finalGunATK;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        finalGunATK = Random.Range(minGunATK, maxGunATK);
    }
    
}
