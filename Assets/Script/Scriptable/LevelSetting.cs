using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Level Setting")]
public class LevelSetting : ScriptableObject
{
    [Header("Size")]
    [SerializeField] private int _towerSize = 10;
    [SerializeField] private int _blockSize = 30;

    [Header("Position")]
    [Range(0, 255)]
    [SerializeField] private int _min = 15;
    [SerializeField] private int _lastBlock = 85;
    [SerializeField] private int _seperateTowerParts = 2;
    [Header("Position to Last Block")]
    [SerializeField] private int _finishLine = 10;
    [SerializeField] private int _pointsBlock = 15;
 


    public int TowerSize { get { return _towerSize; } set { _towerSize = value; } }
    public int BlockSize { get { return _blockSize; } set { _blockSize = value; } }
    public int MinPos { get { return _min; } set { _min = value; } }
    public int Seperate { get { return _seperateTowerParts; } set { _seperateTowerParts = value; } }
    public int LastBlock { get { return _lastBlock; } set { _lastBlock = value; } }
    public int FinishLine { get { return _finishLine; } set { _finishLine = value; } }
    public int PointBlock { get { return _pointsBlock; } set { _pointsBlock = value; } }


}

