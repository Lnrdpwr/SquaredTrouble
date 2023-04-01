using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject[] _staticEnemies;//Enemies with static models
    [SerializeField] private GameObject[] _unstaticEnemies;//Enemies with unstatic models
    [SerializeField] private float _timeToSpawn;
    [SerializeField] private Vector2 _arenaTopRight, _arenaBottomLeft;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeToSpawn);
            int chosenType = Random.Range(0, 2);
            switch (chosenType)
            {
                case 0:
                    SpawnStaticEnemy();
                    break;
                case 1:
                    SpawnUnstaticEnemy();
                    break;
            }
        }
    }

    private void SpawnStaticEnemy()
    {
        int chosenEnemy = Random.Range(0, _staticEnemies.Length);
        Vector2 chosenPosition = new Vector2(Random.Range(_arenaBottomLeft.x, _arenaTopRight.x), Random.Range(_arenaBottomLeft.y, _arenaTopRight.y));
        Instantiate(_staticEnemies[chosenEnemy], chosenPosition, Quaternion.identity);
    }

    private void SpawnUnstaticEnemy()
    {
        int chosenEnemy = Random.Range(0, _unstaticEnemies.Length);
        int chosenSpawn = Random.Range(0, _spawnPoints.Length);
        Instantiate(_unstaticEnemies[chosenEnemy], _spawnPoints[chosenSpawn].position, Quaternion.identity);
    }
}
