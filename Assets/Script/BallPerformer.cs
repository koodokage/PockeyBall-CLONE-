
using UnityEngine;
using StateStuff;

public class BallPerformer : MonoBehaviour
{
    [SerializeField] private Transform transformToRotate;
    private bool switchState,isShoot;
    private Rigidbody _rb;
    private float _speed,_firstAim, _lastAim;
    private Vector2 currentRot;
  

    [Range(5, 20)]
    public float sens = 10;
    [Range(10,200)]
    public float power;
    public StateMachine<BallPerformer> StateMachine { get; set; }

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        StateMachine = new StateMachine<BallPerformer>(this);
        StateMachine.ChangeState(ChargeState.Instance);
        
   
    }
    private void Update()
    {
        var turn = _rb.velocity.magnitude;
        if (_rb.velocity.magnitude > 0.1f)
        {
            transformToRotate.Rotate(new Vector3(0,0,turn * 10) * Time.deltaTime, Space.Self);
            transform.position = new Vector3(0.1f,transform.position.y, 0.03f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            
        }
        #region Controller
        if (Input.GetMouseButtonDown(1))
        {
            StickActions.instance.SetEnable(true);
            StickActions.instance.SetPosition();
            var IsTrigger = StickActions.instance.GetCheck();
            if (!IsTrigger)
            {
                StopMovemment();
                setSwitch(false);
                StickActions.instance.SetList();
            }
       
        }

        if ( isShoot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentRot.x = 0;
                _firstAim = currentRot.x;
        
            }
            if (Input.GetMouseButtonUp(0))
            {

                _lastAim = currentRot.x;
                if (_lastAim >= -15f)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);

                }
                else
                {
                    _rb.constraints = RigidbodyConstraints.None;
                    if (_rb.velocity.magnitude == 0)
                    {
                        setSwitch(true);
                       
                    }

                }

            }

            if (Input.GetMouseButton(0))
            {
                if (_rb.velocity.magnitude == 0)
                {
                    currentRot.x += Input.GetAxis("Mouse Y") * sens;
                    currentRot.x = Mathf.Clamp(currentRot.x, -34f, 0f);
                    transform.rotation = Quaternion.Euler(0, 0, currentRot.x);

                }

               
                

            }
        }
        #endregion
        setSpeed(_firstAim - _lastAim);  
        StateMachine.Update();
    }
    public void StopMovemment()
    {
        _rb.velocity = new Vector3(0, 0, 0);
        _rb.constraints = RigidbodyConstraints.FreezePositionY;
        transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    #region GetterSetter
    public Rigidbody getRigidbody()
    {
        return _rb;
    }
    public float getSpeed()
    {
        return _speed;
    }
    public void setSpeed(float speed)
    {
        _speed = speed;
    }

    public bool getSwitch()
    {
        return switchState;
    }
    public void setSwitch(bool state)
    {
        switchState = state;
    }
    public bool getIsShoot()
    {
        return isShoot;
    }
    public void setIsShoot(bool state)
    {
        isShoot = state;
    }

    #endregion

  

  
    


}
