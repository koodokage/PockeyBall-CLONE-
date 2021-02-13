
using UnityEngine;
[CreateAssetMenu(menuName ="Settings/Ball Setting")]
public class BallSettings : ScriptableObject
{
    
    [Range(5, 20)]
    [SerializeField] private float _sensivityDrag = 10;
    [Range(10, 200)]
    [SerializeField] private float _powerMultiplier = 10;

    public float Sensivity { get { return _sensivityDrag; } }
    public float Power { get { return _powerMultiplier; } }
}
