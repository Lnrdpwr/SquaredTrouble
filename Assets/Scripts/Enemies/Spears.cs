using System.Collections;
using UnityEngine;

public class Spears : MonoBehaviour
{
    [SerializeField] private float _timeToAppear, _timeToHide;
    [SerializeField] private GameObject _spawningEffect, _appearEffect;
    [SerializeField] private GameObject _spears;
    [SerializeField] private AnimationCurve _disappearCurve;
    [SerializeField] private AudioClip _spearsAppear;

    private SpriteRenderer _spearsRenderer;

    private void Start()
    {
        _spearsRenderer = _spears.GetComponent<SpriteRenderer>();
        StartCoroutine(AppearAndHide());
    }

    IEnumerator AppearAndHide()
    {
        yield return new WaitForSeconds(_timeToAppear);
        Instantiate(_appearEffect, transform.position, Quaternion.identity);
        _spawningEffect.SetActive(false);
        _spears.SetActive(true);
        SoundManager.Instance.PlayClip(_spearsAppear);

        for (float i = 0; i < _timeToHide; i += Time.deltaTime)
        {
            _spearsRenderer.color = new Color(1, 1, 1, _disappearCurve.Evaluate(i / _timeToHide));
            yield return new WaitForEndOfFrame();       
        }
        Destroy(gameObject);
    }
}
