using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelect : MonoBehaviour
{
    public static List<GameObject> unit; // массив всех юнитов, которых мы можем выделить
    public static List<GameObject> unitSelected; // массив выделенных юнитов
    public static List<GameObject> ownUnitChoosed;
    public static List<GameObject> enemyUnitChoosed;


    public GUISkin skin;
    private Rect rect;
    private bool draw;
    private Vector2 startPos;
    private Vector2 endPos;

    void Awake()
    {
        unit = new List<GameObject>();
        unitSelected = new List<GameObject>();
        ownUnitChoosed = new List<GameObject>();
        enemyUnitChoosed = new List<GameObject>();
    }
    bool CheckUnit(GameObject unit)
    {
        bool result = false;
        foreach (GameObject u in unitSelected)
        {
            if (u == unit) result = true;
        }

        return result;
    }

    void Select()
    {
        for (int j = 0; j < unitSelected.Count; j++)
        {
            unitSelected[j].GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
    }

    void Deselect()
    {
        for (int j = 0; j < unitSelected.Count; j++)
        {
            if (unitSelected[j].GetComponent<UnitOwnTeam>())
            {
                unitSelected[j].GetComponent<MeshRenderer>().material.color = Color.blue;
            }
            else
            {
                unitSelected[j].GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
        unitSelected.Clear();
        ownUnitChoosed.Clear();
        enemyUnitChoosed.Clear();
    }

    void ChooseOwnUnit()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.GetComponent<UnitOwnTeam>() && unitSelected.Contains(hit.transform.gameObject))
        {
            hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            ownUnitChoosed.Add(hit.transform.gameObject);
            unitSelected.Remove(hit.transform.gameObject);
            if (ownUnitChoosed.Count > 1)
            {
                ownUnitChoosed[0].GetComponent<MeshRenderer>().material.color = Color.yellow;
                unitSelected.Add(ownUnitChoosed[0]);
                ownUnitChoosed.Clear();
                ownUnitChoosed.Add(hit.transform.gameObject);
            }
        }
    }
    void ChooseEnemyUnit()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.GetComponent<UnitEnemyTeam>() && unitSelected.Contains(hit.transform.gameObject))
        {
            hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
            enemyUnitChoosed.Add(hit.transform.gameObject);
            unitSelected.Remove(hit.transform.gameObject);
            if (enemyUnitChoosed.Count > 1)
            {
                enemyUnitChoosed[0].GetComponent<MeshRenderer>().material.color = Color.yellow;
                unitSelected.Add(enemyUnitChoosed[0]);
                enemyUnitChoosed.Clear();
                enemyUnitChoosed.Add(hit.transform.gameObject);
            }
        }
    }

    public static GameObject GetOwnUnit()
    {
        if (ownUnitChoosed.Count > 0)
        {
            return ownUnitChoosed[0];
        }
        return null;
    }

    public static GameObject GetEnemyUnit()
    {
        if (enemyUnitChoosed.Count > 0)
        {
            return enemyUnitChoosed[0];
        }
        return null;
    }
    

    void OnGUI()
    {
        GUI.skin = skin;
        GUI.depth = 99;

        if (Input.GetMouseButtonDown(0))
        {
            Deselect();
            startPos = Input.mousePosition;
            draw = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            draw = false;
            Select();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ChooseOwnUnit();
            ChooseEnemyUnit();
            Debug.Log(GetOwnUnit());
            Debug.Log(GetEnemyUnit());
        }
        
        if (draw)
        {
            endPos = Input.mousePosition;
            if (startPos == endPos) return;

            rect = new Rect(Mathf.Min(endPos.x, startPos.x),
                Screen.height - Mathf.Max(endPos.y, startPos.y),
                Mathf.Max(endPos.x, startPos.x) - Mathf.Min(endPos.x, startPos.x),
                Mathf.Max(endPos.y, startPos.y) - Mathf.Min(endPos.y, startPos.y)
            );

            GUI.Box(rect, "");

            for (int j = 0; j < unit.Count; j++)
            {
                // трансформируем позицию объекта из мирового пространства, в пространство экрана
                Vector2 tmp = new Vector2(Camera.main.WorldToScreenPoint(unit[j].transform.position).x,
                    Screen.height - Camera.main.WorldToScreenPoint(unit[j].transform.position).y);

                if (rect.Contains(tmp)) // проверка, находится-ли текущий объект в рамке
                {
                    if (unitSelected.Count == 0)
                    {
                        unitSelected.Add(unit[j]);
                    }
                    else if (!CheckUnit(unit[j]))
                    {
                        unitSelected.Add(unit[j]);
                    }
                }
            }
        }
    }
}