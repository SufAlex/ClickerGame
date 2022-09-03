using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerScript : MonoBehaviour
{
    
    [SerializeField] private GameObject[] monstersForSpown;
    [SerializeField] private GameObject controller;
    void Awake()
    {
        InvokeRepeating("Spown",1,1);
    }
    
    private void Spown()
    {
        GameObject newMonster = Instantiate(monstersForSpown[Random.Range(0,monstersForSpown.Length)], 
            transform.position + 
            new Vector3(Random.Range(-10, 10),Random.Range(0, 0),Random.Range(-10, 10)), 
            Quaternion.identity);
        
        controller.GetComponent<MonsterControllerScript>().AddMonster(newMonster);
        //newMonster.GetComponent<IClickable>().MCS = controller;
    }

    public void StopSpawn()
    {
        CancelInvoke("Spown");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
