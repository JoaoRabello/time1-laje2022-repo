using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Animate : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _spriteRenderer;

    public bool inMovement;
    private Sprite _visual;

    public void SetAnimatorController(AnimatorOverrideController controller)
    {
        _animator.runtimeAnimatorController = controller;
    }

    public void PlayTransformation(CharacterData nextCharacter)
    {
        _animator.Play("Transformation");
        _visual = nextCharacter.Visual;
        // StartCoroutine(ChangeSprite(nextCharacter.Visual));
    }

    public void ChangeSprite()
    {
        Debug.Log(_visual);
        _spriteRenderer.sprite = _visual;
    }

    // private IEnumerator ChangeSprite(Sprite sprite)
    // {
    //     yield return new WaitForSecondsRealtime(0.2f);
    //     _spriteRenderer.sprite = sprite;
    //     Time.timeScale = 1;
    // }
    
    private void Update()
    {
        _animator.SetBool("inMovement", inMovement);
    }
}
