using System.Collections;
using UnityEngine;

public class EnityFx : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Material originalMat;
    [SerializeField] private Material onHitMat;
    [SerializeField] private float effectTime = 0.2f;
    private IEnumerator blinkCoroutine;
    private void Awake()
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        originalMat = spriteRenderer.material;
    }
    public void OnHitFx()
    {
        StartCoroutine(IOnHitFx());
    }
    IEnumerator IOnHitFx()
    {
        spriteRenderer.material = onHitMat;
        yield return new WaitForSeconds(effectTime);
        ResetMaterial();
    }
    private void ResetMaterial()
    {
        spriteRenderer.material = originalMat;
    }
    public void BlinkRed(float repeatTime)
    {
        CancelBlink();
        blinkCoroutine = IBlinkRed(repeatTime);
        StartCoroutine(blinkCoroutine);
    }
    private IEnumerator IBlinkRed(float repeatTime)
    {
        while (true)
        {
            RedColorBlink();
            yield return new WaitForSeconds(repeatTime);
        }
    }
    private void RedColorBlink()
    {
        if(spriteRenderer.color == Color.red)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = Color.red;
        }
    }
    public void CancelBlink()
    {
        StopAllCoroutines();
        spriteRenderer.color = Color.white;
    }
}
