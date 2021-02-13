using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class StickActions : MonoBehaviour
{
    
    [SerializeField] SkinnedMeshRenderer mesh;
    [SerializeField] Collider col;
    public List<string> triggerList = new List<string>();
    private bool chek;
    public Vector3 mOffset;
    private GameObject player;
    private string first;
   

    public static StickActions instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        var col =this.GetComponent<Collider>();
        col.isTrigger = true;
        player = GameObject.FindGameObjectWithTag("Player");
        
        
    }
    private void Update()
    {
        if (player.GetComponent<Rigidbody>().velocity.magnitude == 0)
            transform.position = player.transform.position + mOffset;
    }
  

    private void OnTriggerEnter(Collider other)
    {
        first = other.gameObject.name;
        if (!triggerList.Contains(first))
        {
            triggerList.Add(first);
            chek = true;
        }
        else
        {
            chek = false;
        }
        
 

    }

    public void SetEnable( bool key)
    {
        mesh.enabled = key;
        col.enabled = key;
    }

    public void SetPosition()
    {
       
            transform.position = player.transform.position + mOffset;
    }

    #region GetterSetter
    public bool GetCheck()
    {
        return chek;
    }
    public void SetList()
    {
        triggerList.Clear();
    }

    #endregion
}





