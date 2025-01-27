using System.Collections;
using UnityEngine;

public class EnityFx : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Material originalMat;
    [SerializeField] private Material onHitMat;
    [SerializeField] private float effectTime = 0.2f;
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
}
