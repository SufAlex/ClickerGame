using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectForSpown;
    void Awake()
    {
        InvokeRepeating("Spown",1,1);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spown()
    {
        Instantiate(objectForSpown, transform.position + 
                                    new Vector3(Random.Range(-10, 10),Random.Range(0, 0),Random.Range(-10, 10)), 
                                    Quaternion.identity);
    }
}
