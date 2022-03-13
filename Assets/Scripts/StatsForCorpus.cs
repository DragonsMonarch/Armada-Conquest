using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatsForCorpus : Stats
{
    //общее хп всего юнита
    public int spaceshipHealth;

    void Start()
    {
        //при старте корпус жив и имеет хп равное макисмальному
        currentHealth = maxHealth;
    }
    void Update()
    {
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
            Destroy(transform.GetChild(transform.childCount));
        }
        
        if (transform.childCount > 0 && transform.GetChild(0) != null)
        {
            //берет хп из всех модулкй и добавляет к общему хп
            int[] modules = new int[transform.childCount];
            spaceshipHealth = currentHealth;
            for (int i = 0; i < modules.Length; i++)
            {
                modules[i] = transform.GetChild(i).GetComponent<StatsForModules>().currentHealth;
                spaceshipHealth += modules[i];
            }
        }
    }
    private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 250), "Unit stats");
        GUI.Label(new Rect(15, 35, 100, 200), "Здоровье юнита:" + spaceshipHealth.ToString());
        GUI.Label(new Rect(15, 75, 100, 200), "Количество подключенных модулей:" + transform.childCount.ToString());
        GUI.Label(new Rect(15, 135, 100, 200), "Модули:");
        String[] modulesName = new String[transform.childCount];
        int y = 155;
        for (int i = 0; i < modulesName.Length; i++)
        {
            modulesName[i] = transform.GetChild(i).GetComponent<StatsForModules>().gameObject.name;
            GUI.Label(new Rect(15, y, 100, 200), modulesName[i]);
            y += 20;
        }
    }
}
