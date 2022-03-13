using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stats : MonoBehaviour
{
    //максимальное хп
    public int maxHealth = 100;
    //текущее хп 
    public int currentHealth;
    //коэффициент сопративления урона для форумлы вычета урона из хп. Чем больше, тем меньше урона получает
    public int resistance = 1; //в процентах
    //урон
    public int damage = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

    }
}
