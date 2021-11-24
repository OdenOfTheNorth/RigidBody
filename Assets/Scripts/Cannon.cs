using System;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private MyRigidBody _body;
    public float CannonForce = 10;
    public GameObject _gameObject;
    public GameObject _aim;
    private Vector3 dir;
    private LineRenderer lineRenderer;
    public int Steps = 100;
    
    private Vector3 Force;
    private Vector3 gravityAcceleration;
    
    private float Mass = 1;
    private float GravityAccelerationStrength = 10;
    
    private Vector3 Position;
    private Vector3 Velocity;
    private Vector3 Acceleration;
    
    private float duration;
    private float currentDuration;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (!lineRenderer)
        {
            gameObject.AddComponent<LineRenderer>();
        }
    }

    void Start()
    {
        Position = transform.position;
        _body = _gameObject.GetComponent<MyRigidBody>();

    }

    void Update()
    {
        Mass = _body.Mass;
        GravityAccelerationStrength = _body.GravityAccelerationStrength;
        
        transform.LookAt(_aim.transform);
        Vector3 pos = Position = transform.position;
        dir = _aim.transform.position - pos;
        dir.Normalize();
        dir *= CannonForce;
        Force = dir;
        
        if (Input.GetButtonDown("Jump"))
        {
            //print(pos);
            var obj = Instantiate(_gameObject);
            obj.transform.position = pos;
            MyRigidBody _body = obj.GetComponent<MyRigidBody>();
            _body.Position = pos;
            _body.ApplyForce(dir,100.1f);
        }
        
        Vector3[] LinePosition = new Vector3[Steps];
        
        lineRenderer.positionCount = Steps;
        
        float TimeSteps = Time.fixedDeltaTime;
        
        for (int i = 0; i < (Steps); i++)
        {
            LinePosition[i] = AccelerationAndVelocity(TimeSteps);
            //print("steps" + Steps + "index = " + i + " LinePosition = " + LinePosition[i]);
        }
        
        lineRenderer.SetPositions(LinePosition);
        Position = Vector3.zero;
        Velocity = Vector3.zero;
    }
    
    public Vector3 AccelerationAndVelocity(float Time)
    {
        gravityAcceleration = (GravityAccelerationStrength) * Vector3.down;
        Acceleration = (Force / Mass) + gravityAcceleration;
        Velocity += Acceleration * Time; 
        Position += Velocity * Time;
        //transform.position = Position;
        //print("gravityAcceleration" + gravityAcceleration + " Acceleration = " + Acceleration + " Velocity = " + Velocity + " Position = " + Position);
        Force = Vector3.zero;
        return Position;
    }
}
