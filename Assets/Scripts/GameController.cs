using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private Rect spawnZone;
    [SerializeField] private GameObject[] monstersForSpawn;
    [SerializeField] private float spawnRate;
    
    private List<GameObject> _monstersList;
    private int _score;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private AudioSource deadSong;

    private CancellationTokenSource _cts;
    private UniTask _spawnTask;
    
    private void Awake()
    {
        SaveDataScript.LoadData();
        _cts = new CancellationTokenSource();
        _monstersList = new List<GameObject>();
        SpawnTask(_cts.Token).Forget();
    }

    private async UniTaskVoid SpawnTask(CancellationToken ct)
    {
        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        while (!ct.IsCancellationRequested)
        {
            var newMonster = Instantiate(monstersForSpawn[Random.Range(0, monstersForSpawn.Length)]);
            _monstersList.Add(newMonster);
            await UniTask.Delay(TimeSpan.FromSeconds(spawnRate), cancellationToken: linkedCts.Token);
        }
        linkedCts.Cancel();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
            
    }

    public void AddMonster(GameObject newMonster)
    {
        _monstersList.Add(newMonster);

       if (GameObject.FindGameObjectsWithTag("Monster").Length > 5)
       {
           _cts.Cancel();
           StartCoroutine(EndGame());

           //TODO Временная затычка пока не переработан спавнер

           SaveDataScript.AddRecord(_score);
           SaveDataScript.SaveData();
           SaveDataScript.PrintToLog();
       }
       
    }
    
    public void KillAll()
    {
        while (_monstersList.Count > 0)
        {
            Destroy(_monstersList[0]);
            _monstersList.RemoveAt(0);
        }

    }

    public void KillThis(GameObject target)
    {
        Destroy(target);
        deadSong.Play();
        _score += 1;
    }
    


    private IEnumerator EndGame()
    { 
        Debug.Log("end " + _score);
        endPanel.GetComponentInChildren<Text>().text = "Score: " + _score;
        endPanel.SetActive(true);
        yield return new WaitForSeconds(4f);;
        SceneManager.LoadScene("MainMenu");
    }

    
}
