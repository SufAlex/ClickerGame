using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
    private List<MonsterPresenter> _monsters;
    
    private int _score;
    
    [SerializeField] private GameObject endPanel;

    private CancellationTokenSource _ctsSpawn;
    private UniTask _spawnTask;

    //TODO перенести сюда ссылку UIView
    
    private void Awake()
    {
        SaveDataScript.LoadData();
        _monsters = new List<MonsterPresenter>();
        
        _ctsSpawn = new CancellationTokenSource();
        SpawnTask(_ctsSpawn.Token).Forget();
        
        EndGameTask().Forget();

    }

    private async UniTaskVoid EndGameTask()
    {
        while(_monsters.Count <= 10) {
            await UniTask.Yield();
        }
        
        _ctsSpawn.Cancel();
        
        SaveDataScript.AddRecord(_score);
        SaveDataScript.SaveData();
        SaveDataScript.PrintToLog();
        
        endPanel.GetComponentInChildren<Text>().text = "Score: " + _score;
        endPanel.SetActive(true);

        await UniTask.Delay(4000);
        
        SceneManager.LoadScene("MainMenu");

    }
    
    private async UniTaskVoid SpawnTask(CancellationToken ct)
    {
        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        while (!ct.IsCancellationRequested)
        {

            CreateMonster();
            
            await UniTask.Delay(TimeSpan.FromSeconds(spawnRate), cancellationToken: linkedCts.Token);
        }
        linkedCts.Cancel();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
        
    }

    private MonsterPresenter CreateMonster()
    {
        MonsterModel monsterModel = new MonsterModel(2);
        //TODO начальный кватернион монстра
        MonsterView monsterView = Instantiate(monstersForSpawn[Random.Range(0, monstersForSpawn.Length)], 
                new Vector3(Random.Range(spawnZone.xMin, spawnZone.xMax),
                    Random.Range(-3,10),
                    Random.Range(spawnZone.yMin, spawnZone.yMax)), 
                Quaternion.identity)
            .GetComponent<MonsterView>();
        
        var monsterPresenter = new MonsterPresenter(monsterModel, monsterView);
        monsterPresenter.Death += () => KillMonster(monsterPresenter);
        _monsters.Add(monsterPresenter);
        return monsterPresenter;
    }

    private void KillMonster(MonsterPresenter monsterPresenter)
    {
        _monsters.Remove(monsterPresenter);
        Destroy(monsterPresenter.MonsterView.gameObject);
        _score++;
    }
    
    public void KillAllMonsters()
    {
        while (_monsters.Count > 0)
        {
            KillMonster(_monsters[0]);
        }
    }

}
