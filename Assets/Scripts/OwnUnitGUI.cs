using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnUnitGUI : MonoBehaviour
{
    private void OnGUI()
    {
        if (UnitSelect.GetOwnUnit() != null) 
        {
            
        }
        else
        {
            GUI.Box(new Rect(10, 10, 100, 250), "Мои юниты");
            GUI.Label(new Rect(20, 35, 100, 250),"Кол-во доступных юнитов:");
            int ownUnitCount = 0;
            for (int i = 0; i < UnitSelect.unit.Count; i++)
            {
                if (UnitSelect.unit[i].GetComponent<UnitOwnTeam>())
                {
                    ownUnitCount++;
                }
            }
            GUI.Label(new Rect(75, 65, 100, 200), ownUnitCount.ToString());
            GUI.Label(new Rect(20, 85, 100, 200), "Доступные юниты:");
            // //GameObject[] unitsList = new GameObject[UnitSelect.unit.get];
            // int y = 105;
            // String unit;
            // for (int i = 0; i < unitsList.Length; i++)
            // {
            //     unit = unitsList[i].name;
            //     GUI.Label(new Rect(15, y, 100, 200), unit);
            //     y += 20;
            // }
            // //GUI.Button(new Rect(20, 105, 100, 200), "Юнит 1");
        }
    }
}
