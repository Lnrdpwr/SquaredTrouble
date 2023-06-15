using System.Collections;
using UnityEngine;

public class Spears : MonoBehaviour
{
    [SerializeField] private float _timeToAppear, _timeToHide;
    [SerializeField] private GameObject _spawningEffect, _appearEffect;
    [SerializeField] private GameObject _spears;
    [SerializeField] private AnimationCurve _disappearCurve;//Кривая для изменения невидимости(см. инспектор)
    [SerializeField] private AudioClip _spearsAppear;

    private SpriteRenderer _spearsRenderer;

    private void Start()
    {
        _spearsRenderer = _spears.GetComponent<SpriteRenderer>();
        StartCoroutine(AppearAndHide());
    }

    IEnumerator AppearAndHide()
    {
        yield return new WaitForSeconds(_timeToAppear);//Ждём время для появления
        Instantiate(_appearEffect, transform.position, Quaternion.identity);//Создаём эффект
        _spawningEffect.SetActive(false);//Выключаем эффект появления
        _spears.SetActive(true);//Показываем копья

        for (float i = 0; i < _timeToHide; i += Time.deltaTime)
        {
            _spearsRenderer.color = new Color(1, 1, 1, _disappearCurve.Evaluate(i / _timeToHide));//Через время изменяет альфу спрайта
            yield return new WaitForEndOfFrame();       
        }
        Destroy(gameObject);//Уничтожаем копья
    }
}
