using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayCasterScript : MonoBehaviour
{
    
    //[SerializeField] private Camera camera;
    [SerializeField] private AudioSource clickSong;
    
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
//gameObject.camera
        Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0) 
                                          && !EventSystem.current.IsPointerOverGameObject()) {

            if (hit.transform.gameObject.GetComponent<IClickable>() != null)
            {
                //Debug.Log("CLICK!!!");
                hit.transform.gameObject.GetComponent<IClickable>().Click();
                clickSong.Play();
            }


            if(hit.transform.gameObject.tag == "Enemy")
                Destroy(hit.transform.gameObject);
            // Do something with the object that was hit by the raycast.
        }
    }
}
