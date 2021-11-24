using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class MyRigidBody : MonoBehaviour
{
    public Vector3 Position;
    public Vector3 Velocity;
    public Vector3 Acceleration;
    
    public Vector3 Force;
    private Vector3 gravityAcceleration;
    
    public float Mass = 1;
    public float GravityAccelerationStrength = 10;
    
    private float CurrentdurationToApplyForce = 0;
    private float duration;
    private float currentDuration;
    
    private bool ShouldApplyForce = false;
    
    //public float AirResistance = 0.3f;
    //private float DurationToApplyForce = 0;

    public int debugPath;
    //private GameObject[] path;
    
    void Start()
    {
        Position = transform.position;


    }

    void FixedUpdate()
    {
        GetAcceleration();

        Velocity += Acceleration * Time.deltaTime; 
        Position += Velocity * Time.deltaTime;
        
        transform.position = Position;
        
        Force = Vector3.zero;
    }
    
    public void ApplyForce(Vector3 force, float duration)
    {
        //      F = 0;      when moven 
        //      F = 100;    hit wall <-|
        //      F = 0;      stop
        //DurationToApplyForce = duration;
        
        Force = force;
        ShouldApplyForce = true;
    }

    public Vector3 AddAccelerationImpulse(Vector3 ImpulseForce)
    {
        currentDuration += Time.deltaTime;

        if (currentDuration < duration)
        {
            return (ImpulseForce / Mass);
        }
        
        return Vector3.zero;
    }
    
    public void SetVelocity(Vector3 velocity)
    {
        Velocity = velocity;
    }

    void GetAcceleration()
    {
        gravityAcceleration = (GravityAccelerationStrength) * Vector3.down;
        
        Acceleration = (Force / Mass) + gravityAcceleration + AddAccelerationImpulse(Vector3.up);

        for (int i = 1; i < debugPath; i++)
        {
            Gizmos.DrawSphere(transform.position, 1);
            

        }
    }
    /*
     if (ShouldApplyForce)
       {
           CurrentdurationToApplyForce += Time.deltaTime;
           
           if (CurrentdurationToApplyForce > DurationToApplyForce)
           {
               CurrentdurationToApplyForce = 0;
               Force = Vector3.zero;
               ShouldApplyForce = false;
           }
       }      
    */
}
