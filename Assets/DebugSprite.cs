using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public Sprite debugSprite;
    public RuntimeAnimatorController debugAnimator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (spriteRenderer == null)
            Debug.LogWarning($"[SpriteDebugger] Aucun SpriteRenderer sur {gameObject.name}");
        if (animator == null)
            Debug.LogWarning($"[SpriteDebugger] Aucun Animator sur {gameObject.name}");
    }

    private void Start()
    {
        Debug.Log($"[SpriteDebugger] Sur {gameObject.name} - Sprite: {(spriteRenderer?.sprite != null ? spriteRenderer.sprite.name : "NULL")}, Animator: {(animator != null ? "OK" : "NULL")}");
    }

    private void OnEnable()
    {
        if (spriteRenderer == null)
            this.spriteRenderer.sprite = debugSprite;
        if (animator == null)
            this.animator.runtimeAnimatorController = debugAnimator;
    }
    private void Update()
    {
        if (spriteRenderer != null)
        {
            if (spriteRenderer.sprite == null) { }
            Debug.LogWarning($"[SpriteDebugger] Sprite disparu sur {gameObject.name} !");
            this.spriteRenderer.sprite = debugSprite;
        }


        if (animator != null)
        {
            if (animator.runtimeAnimatorController == null)
            {
                Debug.LogWarning($"[SpriteDebugger] AnimatorController est NULL sur {gameObject.name} !");
                this.animator.runtimeAnimatorController = debugAnimator;
            }
        }
    }
}
