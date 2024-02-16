using UnityEngine.UI;

namespace UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets
{
    /// <summary>
    /// Add this component to a GameObject and call the <see cref="IncrementText"/> method
    /// in response to a Unity Event to update a text display to count up with each event.
    /// </summary>
    public class TheDoor : MonoBehaviour
    {
        public float openAngle = 90f; // Angle by which the door will rotate when opened
        public float closeAngle = 0f; // Angle by which the door will rotate when closed
        public float smoothSpeed = 2f; // Speed of door rotation
        private bool isOpen = false; // Flag to keep track of door state
        private Quaternion targetRotation;
        public AudioClip doorOpenSound;
        private AudioSource audioSource;
        [SerializeField]
        [Tooltip("The Text component this behavior uses to display the incremented value.")]
        Text m_Text;
        void Start()
        {
            targetRotation = transform.rotation;
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            // Smoothly rotate the door towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
        }
        /// <summary>
        /// The Text component this behavior uses to display the incremented value.
        /// </summary>
        public Text text
        {
            get => m_Text;
            set => m_Text = value;
        }

        public int m_Count;

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        protected void Awake()
        {
            if (m_Text == null)
                Debug.LogWarning("Missing required Text component reference. Use the Inspector window to assign which Text component to increment.", this);
        }

        /// <summary>
        /// Increment the string message of the Text component.
        /// </summary>
        public void IncrementText()
        {
            m_Count += 1;
            if (m_Text != null)
            {
                m_Text.text = m_Count.ToString();
            }
            Debug.Log(m_Count);
            if (m_Count == 3)
                {
                PlaySoundEffect();
                    ToggleDoor();
                }
        }


        

        // Function to open the door
        public void OpenDoor()
        {
            if (!isOpen)
            {
                isOpen = true;
                targetRotation = Quaternion.Euler(0, openAngle, 0);
            }
        }

        // Function to close the door
        public void CloseDoor()
        {
            if (isOpen)
            {
                isOpen = false;
                targetRotation = Quaternion.Euler(0, closeAngle, 0);
            }
        }

        // Function to toggle the door (open/close)
        public void ToggleDoor()
        {
            if (isOpen)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }
        private void PlaySoundEffect()
        {
            if (doorOpenSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(doorOpenSound); // Play the sound effect
            }
        }
    }
}
