using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Enemy> _enemyPrefabs;
    [SerializeField] float _spawnCooldown;
    [SerializeField] float _spawnCooldownReductionMultiplier;
    [SerializeField] Transform _player;         // Pelaajan Transform
    [SerializeField] float _safeDistance = 3f;  // Minimi etäisyys pelaajasta

    float _currentCooldown;
    [SerializeField] Tilemap _groundTiles;
    List<Vector3> _spawnPositions = new();

    void Start()
    {
        SetEnemySpawnPositions();
        InvokeRepeating(nameof(HandleGameDifficultyIncrease), 1f, 1f);
    }

    void Update()
    {
        HandleEnemySpawning();
    }

    void HandleEnemySpawning()
    {
        _currentCooldown -= Time.deltaTime;

        if (_currentCooldown > Time.time)
            return;

        _currentCooldown = Time.time + _spawnCooldown;
        SpawnEnemyToRandomLocation();
    }

    Vector3 GetRandomSafePosition()
    {
        Vector3 spawnPosition;
        int maxTries = 50; // Vältetään infinite loop
        int tries = 0;

        do
        {
            spawnPosition = _spawnPositions[Random.Range(0, _spawnPositions.Count)];
            tries++;
        }
        while (Vector3.Distance(spawnPosition, _player.position) < _safeDistance && tries < maxTries);

        return spawnPosition;
    }

    void SpawnEnemyToRandomLocation()
    {
        if (_enemyPrefabs.Count == 0) return;

        Enemy randomEnemy = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];
        Vector3 spawnPos = GetRandomSafePosition();
        Instantiate(randomEnemy, spawnPos, Quaternion.identity);
    }

    void SetEnemySpawnPositions()
    {
        foreach (Vector3Int position in _groundTiles.cellBounds.allPositionsWithin)
        {
            if (_groundTiles.HasTile(position))
            {
                _spawnPositions.Add(_groundTiles.GetCellCenterWorld(position));
            }
        }
    }

    void HandleGameDifficultyIncrease()
    {
        _spawnCooldown *= _spawnCooldownReductionMultiplier;
    }
}
