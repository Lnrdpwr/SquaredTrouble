using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject[] _staticEnemies;//Копья и меч. У статичных врагов не двигается модель(см. папку Prefabs)
    [SerializeField] private GameObject[] _unstaticEnemies;//Скимитар У нестатичных модель двигается
    [SerializeField] private float _timeToSpawn;
    [SerializeField] private Vector2 _arenaTopRight, _arenaBottomLeft;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        //Спавнит врага раз за промежуток
        while (true)
        {
            yield return new WaitForSeconds(_timeToSpawn);
            int chosenType = Random.Range(0, 2);//Выбирает тип врага
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
        int chosenEnemy = Random.Range(0, _staticEnemies.Length);//Выбирает из врагов
        Vector2 chosenPosition = new Vector2(Random.Range(_arenaBottomLeft.x, _arenaTopRight.x), Random.Range(_arenaBottomLeft.y, _arenaTopRight.y));//Создаём модель врага на самой арене
        Instantiate(_staticEnemies[chosenEnemy], chosenPosition, Quaternion.identity);
    }

    private void SpawnUnstaticEnemy()
    {
        int chosenEnemy = Random.Range(0, _unstaticEnemies.Length);//Выбираем из врагов
        int chosenSpawn = Random.Range(0, _spawnPoints.Length);//Выбираем спавн
        Instantiate(_unstaticEnemies[chosenEnemy], _spawnPoints[chosenSpawn].position, Quaternion.identity);//Создаём врага по углам
    }
}
