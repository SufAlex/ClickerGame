using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using MonstersScripts;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private Rect spawnZone;
    [SerializeField] private GameObject[] monstersForSpawn;
    [SerializeField] private float spawnRate;
    private List<GameObject> _monstersList;
    private List<MonsterPresenter> _monsters;
    
    private int _score;
    
    [SerializeField] private GameObject endPanel;
    [SerializeField] private AudioSource deadSong;

    private CancellationTokenSource _cts;
    private UniTask _spawnTask;
    
    private void Awake()
    {
        SaveDataScript.LoadData();
        _monstersList = new List<GameObject>();
        _monsters = new List<MonsterPresenter>();
        
        _cts = new CancellationTokenSource();
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
        
        if (_monstersList.Count > 5)
        {
            _cts.Cancel();
            StartCoroutine(EndGame());

            SaveDataScript.AddRecord(_score);
            SaveDataScript.SaveData();
            SaveDataScript.PrintToLog();
        }
    }

    private MonsterPresenter CreateMonster()
    {
        MonsterModel monsterModel = new MonsterModel(5);
        MonsterView monsterView = Instantiate(monstersForSpawn[Random.Range(0, monstersForSpawn.Length)]).GetComponent<MonsterView>();
        
        var monsterPresenter = new MonsterPresenter(monsterModel, monsterView);
        monsterPresenter.Death += () => KillMonster(monsterPresenter);
        _monsters.Add(monsterPresenter);
        return monsterPresenter;
    }

    private void KillMonster(MonsterPresenter monsterPresenter)
    {
        _monsters.Remove(monsterPresenter);
        Destroy(monsterPresenter.MonsterView.gameObject);
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
