using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayCasterScript : MonoBehaviour
{
    
    [SerializeField] private AudioSource clickSong;
    
    private RaycastHit hit;

    void Update()
    {
        Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0) 
                                          && !EventSystem.current.IsPointerOverGameObject()) {
            if (hit.transform.gameObject.GetComponent<IClickable>() != null)
            {
                hit.transform.gameObject.GetComponent<IClickable>().Click();
                clickSong.Play();
            }
            
            if(hit.transform.gameObject.tag == "Enemy")
                Destroy(hit.transform.gameObject);

        }
    }
    
    
}
