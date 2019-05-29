using UnityEngine;
using UnityEngine.Networking;
public class TankMovement : NetworkBehaviour
{
    public int m_PlayerNumber = 1;
    public float m_Speed = 12f;
    public float m_TurnSpeed = 180f;
    public AudioSource m_MovementAudio;
    public AudioClip m_EngineIdling;
    public AudioClip m_EngineDriving;
    public float m_PitchRange = 0.2f;
    public FixedJoystick joystick;

    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputValue;
    private float m_OriginalPitch;


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;
        m_OriginalPitch = m_MovementAudio.pitch;


    }


    private void Update()
    {
        if(isLocalPlayer)
        {
            m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
            m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
#if UNITY_ANDROID
        if(joystick != null)
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName) + joystick.Horizontal;
#endif
            EngineAudio();
            // Store the player's input and make sure the audio for the engine is playing.
            Move();
            Turn();
        }

    }


    private void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.

        if (!Mathf.Approximately(m_MovementInputValue, 0f) || !Mathf.Approximately(m_TurnInputValue, 0f))
        {
            if(m_MovementAudio.clip != m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.Play();
            }
        }else
        {
            if (m_MovementAudio.clip != m_EngineIdling)
            {
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.Play();
            }
        }
        m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
    }


    private void FixedUpdate()
    {
        // Move and turn the tank.
        //if(isLocalPlayer)
        //{
        //    //Move();
        //    //Turn();
        //    Move();
        //    CmdTurn();
        //}
        
    
    }
    public override void OnStartLocalPlayer()
    {
        Camera.main.GetComponent<MyCameraControl>().SetPlayer(gameObject.transform);
    }
    private void Move()
    {
        // Adjust the position of the tank based on the player's input.
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
#if UNITY_ANDROID
        if(joystick != null)
        {
            float horiJoyStick = joystick.Horizontal;
            float vertiJoyStick = joystick.Vertical;

            movement.Set(horiJoyStick, 0, vertiJoyStick);
            movement.Normalize();
            bool hasHorizontalInput = !Mathf.Approximately(horiJoyStick, 0f);
            bool hasVerticalInput = !Mathf.Approximately(vertiJoyStick, 0f);

            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, m_TurnSpeed * Time.deltaTime, 0f);

            Quaternion m_Rotation = Quaternion.identity;
            m_Rotation = Quaternion.LookRotation(desiredForward);

            m_Rigidbody.MovePosition(m_Rigidbody.position + movement*Time.deltaTime*m_Speed);
            m_Rigidbody.MoveRotation(m_Rotation);
        }
      
#endif
    }


    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion quaternion = Quaternion.Euler(0, turn, 0);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * quaternion);
    }

    [Command]
    void CmdMove()
    {
        Move();
    }

    [Command]
    void CmdTurn()
    {
        Turn();
    }
}