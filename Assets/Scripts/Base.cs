using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Transform[] _botsSpawnPoints;
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private ScoreViewer _scoreViewer;

    private Queue<Transform> _goldPositions;
    private List<Bot> _bots;

    private float _goldCount = 0;

    private void Start()
    {
        _bots = new List<Bot>();
        _goldPositions = new Queue<Transform>();
        SpawnBots();
    }

    private void Update()
    {
        ScanArea();
    }

    public void TakeGold()
    {
        _goldCount++;
        _scoreViewer.SetScore(_goldCount);
    }

    private void SpawnBots()
    {
        for (int i = 0; i < _botsSpawnPoints.Length; i++)
        {
            Bot bot = Instantiate(_botPrefab, _botsSpawnPoints[i]);

            bot.SetParentBase(this);

            _bots.Add(bot);
        }
    }

    private void ScanArea()
    {
        Transform goldLocation = _scanner.TryGetNextGoldPosition();

        if(goldLocation != null)
        {
            _goldPositions.Enqueue(goldLocation);
        }

        if(_goldPositions.Count > 0)
        {
            TrySendBotForResources();
        }
    }

    private void TrySendBotForResources()
    {
        if(_bots != null)
        {
            foreach (Bot bot in _bots) 
            {
                if (bot.CurrentTarget == null)
                {
                    bot.SetTarget(_goldPositions.Dequeue());
                    return;
                }
            }
        }
    }
}
