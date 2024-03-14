using UnityEngine;
using Windows.Kinect;

public class kinectManager : MonoBehaviour
{
    private KinectSensor kinectSensor;
    private BodyFrameReader bodyFrameReader;

    public float maxDistance = 185f; // Distância máxima de detecção
    private Body closestBody; // Corpo mais próximo detectado
    private Vector3 handLeftPosition; // Posição da mão esquerda
    private Vector3 handRightPosition; // Posição da mão direita
    public float dist;

    void Start()
    {
        // Inicializa o sensor Kinect
        kinectSensor = KinectSensor.GetDefault();
        if (kinectSensor != null)
        {
            // Inicializa o leitor de quadros do corpo
            bodyFrameReader = kinectSensor.BodyFrameSource.OpenReader();
            if (!kinectSensor.IsOpen)
            {
                kinectSensor.Open();
            }
        }
    }

    void Update()
    {


        if (bodyFrameReader != null)
        {

            using (var frame = bodyFrameReader.AcquireLatestFrame())
            {
                if (frame != null)
                {
                    Body[] bodies = new Body[kinectSensor.BodyFrameSource.BodyCount];
                    frame.GetAndRefreshBodyData(bodies);

                    closestBody = GetClosestBody(bodies);

                    if (closestBody != null)
                    {
                        //Debug.Log("Corpo detectado! dist: " + Vector3.Distance(transform.position, GetJointPosition(closestBody.Joints[Windows.Kinect.JointType.SpineBase])));

                        dist = Vector3.Distance(transform.position, GetJointPosition(closestBody.Joints[Windows.Kinect.JointType.SpineBase]));
                        // Limita a detecção à distância máxima
                        if (dist <= maxDistance)
                        {
                            // Obtém as posições das mãos esquerda e direita
                            handLeftPosition = GetJointPosition(closestBody.Joints[Windows.Kinect.JointType.HandLeft]);
                            handRightPosition = GetJointPosition(closestBody.Joints[Windows.Kinect.JointType.HandRight]);
                            // Debug.Log("HandLeft Position: " + handLeftPosition);

                        }
                        else
                        {

                            handLeftPosition = new Vector3(200f, -1000f, 0);
                            handRightPosition = new Vector3(200f, -1000f, 0); ;
                        }
                    }
                }
            }
        }
        else
        {
            handLeftPosition = new Vector3(200f, -1000f, 0);
            handRightPosition = new Vector3(200f, -1000f, 0);
        }
    }

    private Body GetClosestBody(Body[] bodies)
    {
        Body closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (var body in bodies)
        {
            if (body != null && body.IsTracked)
            {
                float distance = Vector3.Distance(transform.position, GetJointPosition(body.Joints[JointType.SpineBase]));
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = body;
                }
            }
        }

        return closest;
    }

    private Vector3 GetJointPosition(Windows.Kinect.Joint joint)
    {
        CameraSpacePoint position = joint.Position;
        return new Vector3(position.X, position.Y, -position.Z);
    }

    void OnDestroy()
    {
        if (bodyFrameReader != null)
        {
            bodyFrameReader.Dispose();
            bodyFrameReader = null;
        }

        if (kinectSensor != null)
        {
            if (kinectSensor.IsOpen)
            {
                kinectSensor.Close();
            }
            kinectSensor = null;
        }
    }

    public Vector3 GetHandLeftPosition()
    {
        return handLeftPosition;
    }

    public Vector3 GetHandRightPosition()
    {
        return handRightPosition;
    }

    public float GetDistance()
    {

        return dist;
    }
}
