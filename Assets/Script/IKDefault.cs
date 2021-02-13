using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IKDefault : MonoBehaviour
{

    public int chainLength;
    [SerializeField ]private Transform target;
    public int Iterations = 10;

    public float delta = 0.001f;

    [Range(0, 1)]
    public float snapBackStrength = 1;
    
    protected float[] BonesLength;
    protected float CompleteLength;
    protected Transform[] Bones;
    protected Vector3[] Postitions;
    protected Vector3[] StartDirrectionSucc;
    protected Quaternion[] StartRotationsBone;
    protected Quaternion StartRotationTarget;
    protected Quaternion StartRotationRoot;

    private void Awake()
    {
        if (target == null)
        {
            var targetG = GameObject.FindGameObjectWithTag("PlayerBall");
            target = targetG.GetComponent<Transform>();
        }
        init();
    }

    private void init()
    {
        Bones = new Transform[chainLength + 1];
        Postitions = new Vector3[chainLength + 1];
        BonesLength = new float[chainLength];
        StartDirrectionSucc = new Vector3[chainLength +1];
        StartRotationsBone = new Quaternion[chainLength + 1];
        StartRotationTarget = target.rotation;
        CompleteLength = 0;

        var current = transform;
        for (var i = Bones.Length-1; i>=0;i--)
        {

            Bones[i] = current;
            StartRotationsBone[i] = current.rotation;
            if (i == Bones.Length-1)
            {
                StartDirrectionSucc[i] = target.position - current.position; 
            }
            else
            {
                StartDirrectionSucc[i] = Bones[i + 1].position - current.position;
                BonesLength[i] = (Bones[i + 1].position - current.position).magnitude;
                CompleteLength += BonesLength[i];
            }
            current = current.parent;
        }

    }

    private void LateUpdate()
    {
        ResolveIK();
    }

    private void ResolveIK()
    {
        if (target == null)
        {
           var  targetG = GameObject.FindGameObjectWithTag("Player");
            target = targetG.GetComponent<Transform>();
        }

        if(BonesLength.Length != chainLength)
        {
            init();
        }
        //Get
        for (int i = 0; i < Bones.Length; i++)
        {
            Postitions[i] = Bones[i].position;
        }
        var RootRot = (Bones[0].parent != null) ? Bones[0].parent.rotation : Quaternion.identity;
        var RootRotDiff = RootRot * Quaternion.Inverse(StartRotationRoot);

        //Calculate
        if((target.position - Bones[0].position).sqrMagnitude >= CompleteLength * CompleteLength)
        {
            var dir = (target.position - Postitions[0]).normalized;
            for (int i = 1; i < Postitions.Length; i++)
            {
                Postitions[i] = Postitions[i - 1] + dir * BonesLength[i-1];
            }
        }
        else
        {
            for (int i = 0; i < Postitions.Length - 1; i++)
            {
                Postitions[i + 1] = Vector3.Lerp(Postitions[i + 1], Postitions[i] + RootRotDiff * StartDirrectionSucc[i], snapBackStrength);
            }

            for (int iteration = 0; iteration < Iterations ; iteration++)
            {
                for (int i = Postitions.Length - 1; i > 0; i--)
                {
                    if(i == Postitions.Length - 1)
                    {
                        Postitions[i] = target.position;
                    }else
                    {
                        Postitions[i] = Postitions[i + 1] + (Postitions[i] - Postitions[i+1]).normalized * BonesLength[i];
                    }
                }
                for (int i = 1; i < Postitions.Length; i++)
                {
                    Postitions[i] = Postitions[i - 1] + (Postitions[i] - Postitions[i - 1]).normalized * BonesLength[i - 1];
                }

                if ((Postitions[Postitions.Length - 1]-target.position).sqrMagnitude < delta*delta)
                    break;
                
            }
       
        }

   


        //Set
        for (int i = 0; i < Postitions.Length; i++)
        {
            if (i ==  Postitions.Length - 1)
            {
                Bones[i].rotation = target.rotation * Quaternion.Inverse(StartRotationTarget) * StartRotationsBone[i];
            }
            else
            {
                Bones[i].rotation = Quaternion.FromToRotation(StartDirrectionSucc[i], Postitions[i + 1] - Postitions[i]) * StartRotationsBone[i];
                Bones[i].position = Postitions[i];
            }
        }


    }

    private void OnDrawGizmos()
    {
        var current = this.transform;
        for (int i = 0; i<chainLength && current!=null && current.parent!= null; i++)
        {
            var scale = Vector3.Distance(current.position, current.parent.position)*0.1f;
            Handles.matrix = Matrix4x4.TRS(current.position, Quaternion.FromToRotation(Vector3.up, current.parent.position - current.position), new Vector3(scale, Vector3.Distance(current.position, current.parent.position), scale));
            Handles.color = Color.magenta;
            Handles.DrawWireCube(Vector3.up*0.5f,Vector3.one);
            current = current.parent;
        }
    }
}
