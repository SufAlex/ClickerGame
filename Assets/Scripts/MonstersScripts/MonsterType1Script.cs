using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterType1Script : MonoBehaviour, IClickable
{
    [SerializeField] private GameObject controller;
    private int _hp = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        if (--_hp <= 0)
        {
            //Debug.Log("MONSTER1");
            controller.GetComponent<MonsterControllerScript>().KillThis(gameObject);
        }
            //Destroy(gameObject);
    }
}
