using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour
{
    [SerializeField] private AnimationCurve _slideBackCurve;//Кривая для отскока от земли(см. инспектор)
    [SerializeField] private GameObject _shadow;
    [SerializeField] private float _timeToLand, _timeToSlideBack;
    [SerializeField] private GameObject _effect;
    [SerializeField] private GameObject _damagingZone;
    [SerializeField] private AudioClip _groundHit;

    private Vector3 _target, _startPosition;
    private SpriteRenderer _shadowRenderer, _swordRenderer;

    private void Awake()
    {
        _shadowRenderer = _shadow.GetComponent<SpriteRenderer>();
        _swordRenderer = GetComponent<SpriteRenderer>();

        _target = transform.parent.position;//Позиция, куда преземлится меч
        _startPosition = transform.position;

        _shadow.SetActive(true);

        StartCoroutine(Land());
    }

    IEnumerator Land()
    {
        float progress = 0f;
        while (progress < _timeToLand)
        {
            progress += Time.deltaTime;
            transform.position = Vector2.Lerp(_startPosition, _target, progress / _timeToLand);//Изменяем позицию между стартовой и конечной относительно пройденного времени
            _shadowRenderer.color = new Color(1, 1, 1, progress / _timeToLand);//Делаем тень темнее
            yield return new WaitForEndOfFrame();
        }

        //После преземления выключаем тень, создаём эффект и меняем позицию для отскока
        _shadow.SetActive(false);
        Instantiate(_effect, transform.position, Quaternion.identity);
        _startPosition = transform.position;
        _target = new Vector3(transform.position.x, transform.position.y + 1);

        _damagingZone.SetActive(true);//Создаём область с уроном на 0.1 секунду
        yield return new WaitForSeconds(0.1f);
        _damagingZone.SetActive(false);

        StartCoroutine(SlideBack());
    }

    IEnumerator SlideBack()
    {
        float progress = 0f;
        while (progress < _timeToSlideBack)
        {
            progress += Time.deltaTime;
            transform.position = Vector2.Lerp(_startPosition, _target, _slideBackCurve.Evaluate(progress / _timeToSlideBack));//Отскок меча вверх
            _swordRenderer.color = new Color(1, 1, 1, _timeToSlideBack - progress);//Меняем альфу меча
            yield return new WaitForEndOfFrame();
        }
        Destroy(transform.parent.gameObject);
    }
}
