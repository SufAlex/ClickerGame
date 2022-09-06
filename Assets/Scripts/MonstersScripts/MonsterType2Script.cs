using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterType2Script : MonoBehaviour, IClickable
{
    [SerializeField] private GameObject controller;
    private int _hp = 2;
    

    private void Awake()
    {
        controller = GameObject.Find("MonsterController");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Click()
    {
        if(--_hp <=0 )
        {
            controller.GetComponent<GameController>().KillThis(gameObject);
        }
    }


}
