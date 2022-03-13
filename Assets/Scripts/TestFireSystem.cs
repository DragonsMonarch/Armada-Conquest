using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class TestFireSystem : MonoBehaviour
{
    //дистанция для выстрела
    public int maxDistance = 10;
    //выбранная цель
    public GameObject target;
    //скорость поворота юнита
    public float rotatespeed = 100f;
    public double dd1;
    public double dd2;
    public int DMG;
    public int modulHealth;
    public int corpusHealth;
    public int damagedHealth;
    void Start()
    {
        
    }
    
    void Update()
    {
        SelectObjects selectObjects = new SelectObjects();
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Input.GetMouseButtonDown(0))
        {
            double dx = Input.mousePosition.x - Screen.width / 2.0;
            double dy = Input.mousePosition.y - Screen.height / 2.0;
            dd1 = dx;
            dd2 = dy;
        }

        float sdx = (float)dd1;
        float sdy = (float)dd2;

        float sR = Mathf.Atan2(sdx, sdy);
        float sD = 360 * sR / (2 * Mathf.PI);

        float startAngle_x = transform.rotation.eulerAngles.x;
        float startAngle_y = sD;
        float startAngle_z = transform.rotation.eulerAngles.z;

        Quaternion target = Quaternion.Euler(startAngle_x, startAngle_y, startAngle_z);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * rotatespeed);
        if (Physics.Raycast(transform.position, fwd, out hit, maxDistance))
        {
            DMG = hit.transform.GetComponent<StatsForGuns>().finalGunATK;
            //modulHealth = hit.transform.GetComponent<StatsForModules>().currentHealth; 
            corpusHealth = hit.transform.GetComponent<StatsForCorpus>().currentHealth;
            damagedHealth = corpusHealth - DMG;
            hit.transform.GetComponent<StatsForCorpus>().currentHealth = 0;
        }
    }
}
