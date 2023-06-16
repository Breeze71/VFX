using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private const string Hit_Position = "_HitPosition";
    private const string Displacement_Strength = "_DisplacementStrength";
    private const string Dissolve = "_Dissolve";


    private Renderer _renderer;
    [SerializeField] private AnimationCurve _DisplacementCurve;
    [SerializeField] private float _DisplacementMagnitude;
    [SerializeField] private float _LerpSpeed;
    [SerializeField] private float _DisolveSpeed;
    private bool _shieldOn;
    private Coroutine _disolveCoroutine;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                HitShield(hit.point);
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            OpenCloseShield();
        }
    }

    public void HitShield(Vector3 hitPos)
    {
        _renderer.material.SetVector(Hit_Position, hitPos);
        StopAllCoroutines();
        StartCoroutine(Coroutine_HitDisplacement());
    }

    public void OpenCloseShield()
    {
        float target = 1;
        if (_shieldOn)
        {
            target = 0;
        }
        _shieldOn = !_shieldOn;
        if (_disolveCoroutine != null)
        {
            StopCoroutine(_disolveCoroutine);
        }
        _disolveCoroutine = StartCoroutine(Coroutine_DisolveShield(target));
    }

    IEnumerator Coroutine_HitDisplacement()
    {
        float lerp = 0;
        while (lerp < 1)
        {
            _renderer.material.SetFloat(Displacement_Strength, _DisplacementCurve.Evaluate(lerp) * _DisplacementMagnitude);
            lerp += Time.deltaTime*_LerpSpeed;
            yield return null;
        }
    }

    IEnumerator Coroutine_DisolveShield(float target)
    {
        float start = _renderer.material.GetFloat(Dissolve);
        float lerp = 0;
        while (lerp < 1)
        {
            _renderer.material.SetFloat(Dissolve, Mathf.Lerp(start,target,lerp));
            lerp += Time.deltaTime * _DisolveSpeed;
            yield return null;
        }
    }
}
