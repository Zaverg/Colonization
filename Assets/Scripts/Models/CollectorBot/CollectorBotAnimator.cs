using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CollectorBotAnimator : MonoBehaviour
{
    private static readonly int s_move = Animator.StringToHash("IsMove");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMoveAnimation(bool isMove)
    {
        _animator.SetBool(s_move, isMove);
    }
}
