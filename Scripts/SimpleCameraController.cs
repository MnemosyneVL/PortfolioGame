
using UnityEngine;

namespace UnityTemplateProjects
{
    public class SimpleCameraController : MonoBehaviour
    {
        class CameraState
        {
            public float yaw;
            public float pitch;
            public float roll;
            public float x;
            public float y;
            public float z;

            public void SetFromTransform(Transform t)
            {
                pitch = t.eulerAngles.x;
                yaw = t.eulerAngles.y;
                roll = t.eulerAngles.z;
                x = t.position.x;
                y = t.position.y;
                z = t.position.z;
            }

            public void Translate(Vector3 translation)
            {
                Vector3 rotatedTranslation = Quaternion.Euler(pitch, yaw, roll) * translation;

                x += rotatedTranslation.x;
                y += rotatedTranslation.y;
                z += rotatedTranslation.z;
            }

            public void LerpTowards(CameraState target, float positionLerpPct, float rotationLerpPct)
            {
                yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);
                pitch = Mathf.Lerp(pitch, target.pitch, rotationLerpPct);
                roll = Mathf.Lerp(roll, target.roll, rotationLerpPct);
                
                x = Mathf.Lerp(x, target.x, positionLerpPct);
                y = Mathf.Lerp(y, target.y, positionLerpPct);
                z = Mathf.Lerp(z, target.z, positionLerpPct);
            }

            public void UpdateTransform(Transform t)
            {
                t.eulerAngles = new Vector3(pitch, yaw, roll);
                t.position = new Vector3(x, y, z);
            }
        }
        
        CameraState m_TargetCameraState = new CameraState();
        CameraState m_InterpolatingCameraState = new CameraState();

        [Header("Movement Settings")]
        [Tooltip("Exponential boost factor on translation, controllable by mouse wheel.")]
        public float boost = 3.5f;

        [Tooltip("Time it takes to interpolate camera position 99% of the way to the target."), Range(0.001f, 1f)]
        public float positionLerpTime = 0.2f;

        [Header("Rotation Settings")]
        [Tooltip("X = Change in mouse position.\nY = Multiplicative factor for camera rotation.")]
        public AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

        [Tooltip("Time it takes to interpolate camera rotation 99% of the way to the target."), Range(0.001f, 1f)]
        public float rotationLerpTime = 0.01f;

        [Tooltip("Whether or not to invert our Y axis for mouse input to rotation.")]
        public bool invertY = false;

        //Animations
        public Animator charAnimator;
        public BoxCollider2D collider2D;
        public GameObject charVisuals;
        public GameObject effectVHS;
        private bool isMirrored = false;
        private bool leftMovementAllowed = true;
        private bool rightMovementAllowed = true;
        private int animationSpeed =1;

        private void Start()
        {
            effectVHS.SetActive(false);
            charVisuals = GameObject.FindGameObjectWithTag("Player");
            charAnimator.speed = animationSpeed;
        }
        void OnEnable()
        {
            m_TargetCameraState.SetFromTransform(transform);
            m_InterpolatingCameraState.SetFromTransform(transform);
        }

        public void SetCharAnimation(int animID)
        {
            charAnimator.SetInteger("AnimationState", animID);
        }

        public void AnimationHandler()
        {
            charAnimator.speed = animationSpeed;
            if (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D) && leftMovementAllowed && rightMovementAllowed)
            {
                SetCharAnimation(1);

            }
            else 
            {
                SetCharAnimation(0);
            }
            if (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D) && leftMovementAllowed && rightMovementAllowed)
            {
                if (Input.GetKey(KeyCode.A) && leftMovementAllowed)
                {
                    charVisuals.transform.rotation = Quaternion.Euler(0, 180f, 0);
                    collider2D.offset = new Vector2(-1.75f, 2.75f);
                }
                if (Input.GetKey(KeyCode.D) && rightMovementAllowed)
                {
                    charVisuals.transform.rotation = Quaternion.Euler(0, 0, 0);
                    collider2D.offset = new Vector2(1.75f, 2.75f);
                }
            }
        }

        Vector3 GetInputTranslationDirection()
        {
            Vector3 direction = new Vector3();
           /* if (Input.GetKey(KeyCode.W))
            {
                direction += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector3.back;
            }*/
            if (Input.GetKey(KeyCode.A) && leftMovementAllowed)
            {
                direction += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D) && rightMovementAllowed)
            {
                direction += Vector3.right;
            }
            return direction;
        }
        
        void Update()
        {

            var translation = GetInputTranslationDirection() * Time.deltaTime;

            // Speed up movement when shift key held
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                animationSpeed = 3;
                effectVHS.SetActive(true);
            }
            else if(Input.GetKey(KeyCode.LeftShift))
            {
                translation *= 3.0f;
            }
            else if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                animationSpeed = 1;
                effectVHS.SetActive(false);
            }
            //Animations
            AnimationHandler();

            // Translation
            translation *= Mathf.Pow(2.0f, boost);

            m_TargetCameraState.Translate(translation);

            // Framerate-independent interpolation
            // Calculate the lerp amount, such that we get 99% of the way to our target in the specified time
            var positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / positionLerpTime) * Time.deltaTime);
            var rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / rotationLerpTime) * Time.deltaTime);
            m_InterpolatingCameraState.LerpTowards(m_TargetCameraState, positionLerpPct, rotationLerpPct);

            m_InterpolatingCameraState.UpdateTransform(transform);
        }

        public void TeleportPlayer(Vector3 destination)
        {
            Vector3 translation = destination - transform.position;
            m_TargetCameraState.Translate(new Vector3(translation.x, 0, 0));

        }

        public void Immobilize()
        {
            rightMovementAllowed = false;
            leftMovementAllowed = false;
        }

        public void ImmobilizeLeft()
        {
            leftMovementAllowed = false;
        }

        public void ImmobilizeRight()
        {
            rightMovementAllowed = false;
        }

        public void Mobilize()
        {
            leftMovementAllowed = true;
            rightMovementAllowed = true;
        }
    }

}