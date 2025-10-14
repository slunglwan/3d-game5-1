using System;
using UnityEngine;

public class HPBarController : MonoBehaviour
{
    [SerializeField] private GameObject hpBarPrefab;
    
    private HPBar _hpBar;
    private Canvas _canvas;
    private RectTransform _hpBarRectTransform;
    private Camera _camera;
    private Vector3 _offset;

    private void Start()
    {
        _camera = Camera.main;
        _canvas = GameManager.Instance.Canvas;
        _hpBar = Instantiate(hpBarPrefab, _canvas.transform).GetComponent<HPBar>();
        _hpBarRectTransform = _hpBar.GetComponent<RectTransform>();
        _offset = new Vector3(0, 1.5f, 0);
    }

    public void SetHp(float hp)
    {
        _hpBar.SetHPGauge(hp);
    }

    private void LateUpdate()
    {
        var screenPosition = _camera.WorldToScreenPoint(transform.position + _offset);
        
        bool isVisible = screenPosition.z > 0 
                         && screenPosition.x > 0 && screenPosition.x < Screen.width
                         && screenPosition.y > 0 && screenPosition.y < Screen.height;
        
        _hpBar.gameObject.SetActive(isVisible);
        
        if (isVisible)
        {
            _hpBarRectTransform.position = screenPosition;
        }
    }
}
