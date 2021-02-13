using UnityEngine;
using StateStuff;

public class LaunchState : State<BallPerformer>
{
    private static LaunchState _instance;

    private LaunchState()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static LaunchState Instance
    {
        get
        {
            if (_instance == null)
            {
                new LaunchState();
            }
            return _instance;
        }
    }


    public override void ExitState(BallPerformer _owner)
    {
    }

    public override void EnterState(BallPerformer _owner)
    {
        Debug.Log("LAUNCH!!!");
        StickActions.instance.SetEnable(false);
        float speed = _owner.getSpeed();
        float power = _owner._setting.Power;
        Rigidbody _rb = _owner.getRigidbody();
        _rb.AddForce(new Vector3(0, speed, 0) * power );
        
    }

    public override void UpdateState(BallPerformer _owner)
    {
        if (!_owner.getSwitch())
            _owner.StateMachine.ChangeState(ChargeState.Instance);
    }
}
