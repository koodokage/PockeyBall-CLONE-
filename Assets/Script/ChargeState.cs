using UnityEngine;
using StateStuff;

public class ChargeState : State<BallPerformer>
{

    private static ChargeState _instance;

    private ChargeState()
    {
        if(_instance != null)
        {
            return;
        }
        _instance = this;
        
    }

    public static ChargeState Instance
    {
        get
        {
            if(_instance == null)
            {
                new ChargeState();
                
            }
            return _instance;
        }
    }


    public override void ExitState(BallPerformer _owner)
    {
       
    }

    public override void EnterState(BallPerformer _owner)
    {
        _owner.StopMovemment();
        _owner.setIsShoot(true);

    }

    public override void UpdateState(BallPerformer _owner)
    {
        if (_owner.getSwitch())
        {
            _owner.StateMachine.ChangeState(LaunchState.Instance);
        }
           
    }

   

}
