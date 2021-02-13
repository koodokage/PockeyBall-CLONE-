using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LevelBuilder { 

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private LevelSetting _setting;
    [SerializeField] private GameObject[] _ball;
    [SerializeField] private GameObject[] _towerPart;
    [SerializeField] private List<GameObject> _pointBlock;
    [SerializeField] private GameObject _block;
    [SerializeField] private GameObject _stick;
    [SerializeField] private GameObject _finish;
    [SerializeField] private Transform _locationTower;
    [SerializeField] private Transform _locationBlock;
    [SerializeField] private Transform _locationBall;
    [SerializeField] private Transform _locationStick;
    int blockRandom;
    int towerRandom;
    int ballRandom;
    int pointBlockRandom;
 
    

    private void Start()
    {
        
        towerRandom = Random.Range(0, _towerPart.Length);
        ballRandom = Random.Range(0, _ball.Length);
        Instantiate(_ball[ballRandom],_locationBall.transform.position,Quaternion.identity);
        Instantiate(_stick, _locationStick.position, Quaternion.Euler(0,90,0));
        Vector3 posBlock = _locationBlock.localPosition;
        Vector3 posTower = _locationTower.localPosition;
        for (int i = 0; i<=_setting.TowerSize; i++)
        {      
            _locationTower.transform.position = new Vector3(posTower.x, posTower.y, posTower.z);
            Instantiate(_towerPart[towerRandom], _locationTower.transform.position,Quaternion.identity);
            var pos = _locationTower.position.y;
            if (pos < _setting.LastBlock)
            {
                _setting.TowerSize++;
            }
            posTower.y += _setting.Seperate;
        }

        for (int i = 0; i <= _setting.BlockSize; i++)
        {
            blockRandom = Random.Range(_setting.MinPos, _setting.LastBlock);
            posBlock.y = blockRandom;
            _locationBlock.transform.position = new Vector3(posBlock.x, posBlock.y, posBlock.z);
            Instantiate(_block, _locationBlock.transform.position,Quaternion.identity);
        }

        Instantiate(_finish, new Vector3(0.7f, _setting.LastBlock + _setting.FinishLine, 0),Quaternion.Euler(0,90,0));
        
        while (_pointBlock.Count >0) 
        {
            pointBlockRandom = Random.Range(0, _pointBlock.Count);
            Instantiate(_pointBlock[pointBlockRandom], new Vector3(0,(_setting.LastBlock + _setting.PointBlock)+_pointBlock.Count*4, 0), Quaternion.identity);
            _pointBlock.RemoveAt(pointBlockRandom);
           
        }
        

    }

}
}
