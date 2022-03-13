using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SelectObjects : MonoBehaviour
{
	//TODO сократить количество циклов и условий (оптимизировать)
	public static List<GameObject> unitRedTeam; //юниты красной команды
	public static List<GameObject> unitBlueTeam; //юниты синей команды
	public static List<GameObject> unitRedTeamSelected; //выделенные юниты красной команды
	public static List<GameObject> unitBlueTeamSelected; // выделенные юниты синей команды

	//выбор скина для выделения
	public GUISkin skin;
	private Rect rect;
	private bool draw;
	private Vector2 startPos;
	private Vector2 endPos;
	public GameObject selectedRedTeamUnit;
	public GameObject selectedBlueTeamUnit;
	
	void Awake () 
	{
		unitBlueTeam = new List<GameObject>();
		unitRedTeam = new List<GameObject>();
		unitRedTeamSelected = new List<GameObject>();
		unitBlueTeamSelected = new List<GameObject>();
	}

	// проверка, добавлен объект или нет
	bool CheckBlueTeamUnit (GameObject unit) 
	{
		bool result = false;
		foreach(GameObject u in unitBlueTeamSelected)
		{
			if(u == unit) result = true;
		}
		return result;
	}
	bool CheckRedTeamUnit (GameObject unit) 
	{
		bool result = false;
		foreach(GameObject u in unitRedTeamSelected)
		{
			if(u == unit) result = true;
		}
		return result;
	}

	//TODO оптимизировать кол-во циклов и условий
	private void Select()
	{
		if(unitBlueTeamSelected.Count > 0)
		{
			for(int j = 0; j < unitBlueTeamSelected.Count; j++)
			{
				// делаем что-либо с выделенными объектами
				unitBlueTeamSelected[j].GetComponent<MeshRenderer>().material.color = Color.yellow;
			}
		}
		if(unitRedTeamSelected.Count > 0)
		{
			for(int j = 0; j < unitRedTeamSelected.Count; j++)
			{
				// делаем что-либо с выделенными объектами
				unitRedTeamSelected[j].GetComponent<MeshRenderer>().material.color = Color.green;
			}
		}
	}

	//TODO оптимизировать кол-во циклов и условий
	private void Deselect()
	{
		if(unitBlueTeamSelected.Count > 0)
		{
			for(int j = 0; j < unitBlueTeamSelected.Count; j++)
			{
				// отменяем то, что делали с объектоми
				unitBlueTeamSelected[j].GetComponent<MeshRenderer>().material.color = Color.blue;
			}
		}
		if(unitRedTeamSelected.Count > 0)
		{
			for(int j = 0; j < unitRedTeamSelected.Count; j++)
			{
				// отменяем то, что делали с объектоми
				unitRedTeamSelected[j].GetComponent<MeshRenderer>().material.color = Color.red;
			}
		}
		for (int i = 0; i < unitBlueTeam.Count; i++)
		{
			unitBlueTeam[i].gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
		}
		for (int i = 0; i < unitRedTeam.Count; i++)
		{
			unitRedTeam[i].gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
		}
		//приводим к нулю чтобы данные передовлись корректно
		selectedBlueTeamUnit = null;
		selectedRedTeamUnit = null;
	}
	
	//TODO сделать все одним циклом
	private void Confirm()
	{
		//подтверждение выделения юнита для красной команды
		RaycastHit hit;
		for(int j = 0; j < unitRedTeamSelected.Count; j++)
		{
			if (unitRedTeamSelected[j].GetComponent<MeshRenderer>().material.color == Color.green)
			{
				//отправляем луч в сторону курсора чтобы проверить есть ли там объект, если есть, подтверждаем его выбор
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit))
				{
					if (hit.transform.gameObject.GetComponent<UnitRedTeam>())
					{
						unitRedTeamSelected.Remove(hit.transform.gameObject);
						hit.transform.GetComponent<MeshRenderer>().material.color = Color.magenta;
						selectedRedTeamUnit = hit.transform.gameObject;
						//если есть дочерние элементы, также их подтверждаем
						for (int i = 0; i < hit.transform.childCount; i++)
						{
							hit.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = Color.magenta;
							unitRedTeamSelected.Remove(hit.transform.GetChild(i).gameObject);
						}
					}
				}
			}
		}
		//все тоже самое для красной команды
		for(int j = 0; j < unitBlueTeamSelected.Count; j++)
		{
			if (unitBlueTeamSelected[j].GetComponent<MeshRenderer>().material.color == Color.yellow)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit))
				{
					if (hit.transform.gameObject.GetComponent<UnitBlueTeam>())
					{
						unitBlueTeamSelected.Remove(hit.transform.gameObject);
						hit.transform.GetComponent<MeshRenderer>().material.color = Color.cyan;
						selectedBlueTeamUnit = hit.transform.gameObject;
						for (int i = 0; i < hit.transform.childCount; i++)
						{
							hit.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = Color.cyan;
							unitBlueTeamSelected.Remove(hit.transform.GetChild(i).gameObject);
						}
					}
				}
			}
		}
	}

	//метод отмены выделения при подтверждении выбора юнита
	private void IsCancelled()
	{
		for (int i = 0; i < unitBlueTeamSelected.Count; i++)
		{
			unitBlueTeamSelected[i].GetComponent<MeshRenderer>().material.color = Color.blue;
		}
		for (int i = 0; i < unitRedTeamSelected.Count; i++)
		{
			unitRedTeamSelected[i].GetComponent<MeshRenderer>().material.color = Color.red;
		}
	}
	
	//гетеры для получения выбранных объектов
	public GameObject getSelectedRedTeamUnit()
	{
		return selectedRedTeamUnit;
	}
	
	public GameObject getSelectedBlueTeamUnit()
	{
		return selectedBlueTeamUnit;
	}
	
	void OnGUI()
	{
		GUI.skin = skin;
		GUI.depth = 99;
		
		//отмена выделения
		if (Input.GetMouseButtonDown(0))
		{
			Deselect();
			startPos = Input.mousePosition;
			draw = true;
		}
		//подтверждение выбора
		if (Input.GetKeyDown(KeyCode.E)) 
		{ 
			Confirm(); 
			IsCancelled();
		}

		//выделение
		if (Input.GetMouseButtonUp(0))
		{
			draw = false;
			Select();
		}
		if (draw)
		{
			unitBlueTeamSelected.Clear();
			unitRedTeamSelected.Clear();
			endPos = Input.mousePosition;
			if (startPos == endPos) return;

			rect = new Rect(Mathf.Min(endPos.x, startPos.x),
				Screen.height - Mathf.Max(endPos.y, startPos.y),
				Mathf.Max(endPos.x, startPos.x) - Mathf.Min(endPos.x, startPos.x),
				Mathf.Max(endPos.y, startPos.y) - Mathf.Min(endPos.y, startPos.y)
			);

			GUI.Box(rect, "");

			//TODO оптисмизировать при возможности
			for (int j = 0; j < unitBlueTeam.Count; j++)
			{
				// трансформируем позицию объекта из мирового пространства, в пространство экрана
				Vector2 tmp = new Vector2(Camera.main.WorldToScreenPoint(unitBlueTeam[j].transform.position).x,
						Screen.height - Camera.main.WorldToScreenPoint(unitBlueTeam[j].transform.position).y);

					if (rect.Contains(tmp)) // проверка, находится-ли текущий объект в рамке
					{
						if (unitBlueTeamSelected.Count == 0)
						{
							unitBlueTeamSelected.Add(unitBlueTeam[j]);
						}
						else if (!CheckBlueTeamUnit(unitBlueTeam[j]))
						{
							unitBlueTeamSelected.Add(unitBlueTeam[j]);
						}
					}
			}
			for (int j = 0; j < unitRedTeam.Count; j++)
			{
				// трансформируем позицию объекта из мирового пространства, в пространство экрана
				Vector2 tmp = new Vector2(Camera.main.WorldToScreenPoint(unitRedTeam[j].transform.position).x,
					Screen.height - Camera.main.WorldToScreenPoint(unitRedTeam[j].transform.position).y);

				if (rect.Contains(tmp)) // проверка, находится-ли текущий объект в рамке
				{
					if (unitRedTeamSelected.Count == 0)
					{
						unitRedTeamSelected.Add(unitRedTeam[j]);
					}
					else if (!CheckRedTeamUnit(unitRedTeam[j]))
					{
						unitRedTeamSelected.Add(unitRedTeam[j]);
					}
				}
			}
		}
	}
}
