using System.Collections;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private LevelResultingPanel _resultingPanel;
    [SerializeField] private TMP_Text _secondsCounter;

    private bool _gameIsRunning = true;
    private int _seconds = 0;

    private void Start()
    {
        StartCoroutine(CountSeconds());
    }

    public void StopGame()
    {
        _secondsCounter.gameObject.SetActive(false);
        _gameIsRunning = false;
        _spawner.StopAllCoroutines();
        _resultingPanel.ShowPanel(_seconds);
    }

    IEnumerator CountSeconds()
    {
        while (_gameIsRunning)
        {
            yield return new WaitForSeconds(1);
            _seconds++;
            _secondsCounter.text = _seconds.ToString();
        }
    }
}
