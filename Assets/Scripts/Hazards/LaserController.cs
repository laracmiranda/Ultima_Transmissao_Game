using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour
{

    [SerializeField] private float warningTime = 0.5f;

    [SerializeField] private float activeTime = 3f;

    [SerializeField] private float startDelay = 0f;

    [SerializeField] private float cooldownTime = 2f;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite chargingSprite;

    [SerializeField] private Sprite activeSprite;

    private Collider2D laserCollider;
    private SpriteRenderer laserRenderer;

    private bool laserActive;

    private void Awake()
    {
        laserCollider = GetComponent<Collider2D>();
        laserRenderer = GetComponent<SpriteRenderer>();

        laserCollider.enabled = false;
        laserRenderer.enabled = false;
    }

    // Faz o laser aparecer, esperar, liga a colisão, desliga tudo
    public void StartLaser()
    {
        StartCoroutine(LaserRoutine());
    }

    private IEnumerator LaserRoutine()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            laserRenderer.enabled = true;
            spriteRenderer.sprite = chargingSprite;

            yield return new WaitForSeconds(warningTime);

            laserCollider.enabled = true;
            spriteRenderer.sprite = activeSprite;
            laserActive = true;
            AudioManager.Instance.PlayLaserActivateSound();

            yield return new WaitForSeconds(activeTime);

            laserCollider.enabled = false;
            laserRenderer.enabled = false;
            laserActive = false;

            yield return new WaitForSeconds(cooldownTime);
        }
    }

private void OnTriggerStay2D(Collider2D other)
    {
        if (GameManager.Instance.isGameOver)
            return;

        if (!laserActive)
            return;

        if (!other.CompareTag("Player"))
            return;

        GameManager.Instance.RegisterLaserHit();
    }

    public void EnableLaser()
    {
        StartLaser();
    }

    public void DisableLaser()
    {
        StopAllCoroutines();

        laserCollider.enabled = false;
        laserRenderer.enabled = false;
        laserActive = false;
    }

    public void ResetLaser()
    {
        StopAllCoroutines();

        laserCollider.enabled = false;
        laserRenderer.enabled = false;
        laserActive = false;

        StartLaser();
    }
}