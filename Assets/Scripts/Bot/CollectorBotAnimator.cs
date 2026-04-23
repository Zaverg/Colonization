using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CollectorBotAnimator : MonoBehaviour
{
    private static readonly int s_MoveHash = Animator.StringToHash("IsMove");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMoveAnimation(bool isMove)
    {
        _animator.SetBool(s_MoveHash, isMove);
    }
}
