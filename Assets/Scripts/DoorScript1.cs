using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class DoorScript2 : MonoBehaviour
    {
        private bool isOpen;
        private bool isLocked;
        private float inTimeTimeout = 2.0f;
        private float outTimeTimeout = 5.0f;
        private float timeout;
        private float openTime;
        private AudioSource hitSound;
        private AudioSource openSound;

        void Start()
        {
            isLocked = true;
            isOpen = false;
            openTime = 0.0f;
            hitSound = GetComponent<AudioSource>();
            AudioSource[] audioSources = GetComponents<AudioSource>();
            hitSound = audioSources[0];
            openSound = audioSources[1];
        }

        void Update()
        {
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "Player")
            {

                if (GameState.collectedKeys.Keys.Contains("2"))
                {

                    bool isInTime = GameState.collectedKeys["2"];
                    timeout = isInTime ? inTimeTimeout : outTimeTimeout;
                    openTime = timeout;
                    isLocked = false;
                    ToastScript.ShowToast("Ключ \"2\" застосовано" +
                    (isInTime ? "вчасно" : "He вчасно"));

                    Destroy(gameObject);
                    openSound.Play();
                }

                else

                    ToastScript.ShowToast("Для відкриття двері потрібен ключ \"2\"");
            }

            hitSound.Play();
        }

    }
}
