using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MonsterControllerScript : MonoBehaviour
{

    [SerializeField] private List<GameObject> monstersList;
    [SerializeField] private int score;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private AudioSource deadSong;
    [SerializeField] private MonsterSpawnerScript MCS;
    private void Awake()
    {
        score = 0;
        SaveDataScript.LoadData();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
            
    }

    public void AddMonster(GameObject newMonster)
    {
        monstersList.Add(newMonster);

       if (GameObject.FindGameObjectsWithTag("Monster").Length > 5)
       {
            //TODO Карутина запускается несколько раз хотя достаточно одного
           StartCoroutine(EndGame());

           //TODO Временная затычка пока не переработан спавнер
           MCS.StopSpawn();
           SaveDataScript.AddRecord(score);
           SaveDataScript.SaveData();
           SaveDataScript.PrintToLog();
       }
       
    }
    
    public void KillAll()
    {
        while (monstersList.Count > 0)
        {
            Destroy(monstersList[0]);
            monstersList.RemoveAt(0);
        }

    }

    public void KillThis(GameObject target)
    {
        Destroy(target);
        deadSong.Play();
        score += 1;
    }
    


    private IEnumerator EndGame()
    { 
        Debug.Log("end " + score);
        endPanel.GetComponentInChildren<Text>().text = "Score: " + score;
        endPanel.SetActive(true);
        yield return new WaitForSeconds(4f);;
        SceneManager.LoadScene("MainMenu");
    }

    
}
