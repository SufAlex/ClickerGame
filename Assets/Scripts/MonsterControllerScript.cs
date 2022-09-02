using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MonsterControllerScript : MonoBehaviour
{

    [SerializeField] private List<GameObject> monstersList;
    // Start is called before the first frame update
    [SerializeField] private int score;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private AudioSource deadSong;
    private void Awake()
    {
        score = 0;
    }

    private void Update()
    {

    }

    public void AddMonster(GameObject newMonster)
    {
       monstersList.Add(newMonster);
       
       //Debug.Log("AddMonster");
       //Debug.Log(monstersList.Count);

       if (GameObject.FindGameObjectsWithTag("Monster").Length > 5)
       {
           StartCoroutine(EndGame());
       }
       
       

    }
    
    public void KillAll()
    {
        while (monstersList.Count > 0)
        {
            //Debug.Log("KillALL");
            Destroy(monstersList[0]);
            monstersList.RemoveAt(0);
            //Debug.Log(monstersList.Count);

        }

    }

    public void KillThis(GameObject target)
    {
        Debug.Log("KillThis");
        Destroy(target);
        
       /* if(deadSong == null)
            Debug.Log("NULL");
        else
            Debug.Log("NOT NULL");*/
       
        deadSong.Play();
        Debug.Log("before " + score);
        score += 1;
        Debug.Log("after " + score);
    }
    


    private IEnumerator EndGame()
    {
        //deadSong.Play();

        Debug.Log("end " + score);
        endPanel.GetComponentInChildren<Text>().text = "Score: " + score;
        endPanel.SetActive(true);
        yield return new WaitForSeconds(4f);;
        SceneManager.LoadScene("MainMenu");
    }

    
}
