using System;

public class MovingState : BotState
{
    private IBot _bot;

    public override event Action Completed;

    public override void Entry(IBot stateMachine)
    {
        _bot = stateMachine;

        _bot.Mover.SetTarget(stateMachine.CurrentTask.TargetPosition);
        _bot.Animator.SetMoveAnimation(true);
    }

    public override void Run()
    {
        _bot.Mover.Move();

        if (_bot.Mover.HasReachedTarget())
        {
            Completed?.Invoke();
        }
    }

    public override void Exit()
    {
        _bot.Animator.SetMoveAnimation(false);
        _bot = null;
    }
}