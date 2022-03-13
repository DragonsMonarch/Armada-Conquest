using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsForModules : Stats
{
    // //куда пристыковывается модуль
    public GameObject parentObject;
    void Start()
    {
        //при старте модуль жив и имеет хп равное макисмальному
        currentHealth = maxHealth;
    }
    
    // Update is called once per frame
    void Update()
    {
        //выбор объекта родителя
        transform.SetParent(parentObject.transform, true);
        if (damage > 0)
        {
            currentHealth = currentHealth - (damage - ((damage / 100) * resistance));
        }
        //если текущее хп больше максимального, приравнивает чтобы коректно отображалось
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        //если хп ниже нуля, модуль мертв, приравнивание к нулю чтобы коректно ображалось
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
        }
    }
}
