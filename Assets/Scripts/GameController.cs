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
using UIScripts;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private Rect spawnZone;
    [SerializeField] private GameObject[] monstersForSpawn;
    [SerializeField] private float spawnRate;
    private List<MonsterPresenter> _monsters;
    
    private int _score;

    private CancellationTokenSource _ctsSpawn;
    private UniTask _spawnTask;
    
    private CancellationTokenSource _ctsEndGame;

    [SerializeField] private UIView gameUIView;
    private UIPresenter _gameUI;
    
    private void Awake()
    {
        SaveDataScript.LoadData();
        _monsters = new List<MonsterPresenter>();
        _gameUI = CreateGameUI();
        
        _ctsSpawn = new CancellationTokenSource();
        SpawnTask(_ctsSpawn.Token).Forget();
        
        _ctsEndGame = new CancellationTokenSource();
        EndGameTask(_ctsEndGame.Token).Forget();
    }

    private async UniTaskVoid EndGameTask(CancellationToken ct)
    {
        while(_monsters.Count <= 10)
        {
            if (ct.IsCancellationRequested) {
                break;
            }
            await UniTask.Yield();
        }

        if (!ct.IsCancellationRequested) {
            _ctsSpawn.Cancel();
            
            SaveDataScript.AddRecord(_score);
            SaveDataScript.SaveData();

            _gameUI.PrintScorePanel(_score);
            await UniTask.Delay(4000);
            _gameUI.ToMainMenu();
        }
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
        //TODO ???????????? ?????????????? ?????????????????????????? ???????????? ???????????????? ?? ???????????????????? ????(???????????) ?? ???????? ?????? ???????????? ???????????? ???????????????????????
        MonsterModel monsterModel = new MonsterModel(2);
        //TODO ?????????????????? ???????????????????? ??????????????
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

    private UIPresenter CreateGameUI()
    {
        //NOTE ????????????????. ?? ?????? ?????? ?????????? ?????????
        UIModel uiModel = new UIModel();
        UIPresenter uiPresenter = new UIPresenter(uiModel, gameUIView);

        uiPresenter.Boost1 += () => KillAllMonsters();
        
        return uiPresenter;
    }

    private void OnDestroy()
    {
        _ctsSpawn.Cancel();
        _ctsEndGame.Cancel();
    }
    
}
