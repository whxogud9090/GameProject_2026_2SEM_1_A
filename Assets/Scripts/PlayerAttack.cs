using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] private float attackDuration = 0.8f;
    [SerializeField] private float attackMoment = 0.3f;
    [SerializeField] private string attackAnimationName = "Attack";
    private bool isAttacking;

    void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse == null || isAttacking) return;
        if (playerController.IsNormalState() == false) return;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            StartCoroutine(AttackAnimation());
        }
    }

    private IEnumerator AttackAnimation()
    {
        isAttacking = true;
        playerController.ChangeState(PlayerState.Attack);
        int attackState = Animator.StringToHash(attackAnimationName);
        if (animator.HasState(0, attackState))
        {
            animator.CrossFade(attackAnimationName, 0.1f);
        }
        else
        {
            Debug.LogWarning(attackAnimationName + " 애니메이션 상태가 없습니다.");
        }
        yield return new WaitForSeconds(attackMoment);
        HitEnemy();
        yield return new WaitForSeconds(Mathf.Max(0f, attackDuration - attackMoment));
        animator.CrossFade("Blend Tree", 0.1f);
        playerController.ChangeState(PlayerState.Normal);
        isAttacking = false;
    }

    private void HitEnemy()
    {
        Collider[] targets = Physics.OverlapSphere(attackPoint.position, attackRange);
        foreach (Collider target in targets)
        {
            EnemyHealth enemy = target.GetComponentInParent<EnemyHealth>();
            if (enemy == null) continue;
            enemy.TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
