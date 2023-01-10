using System.Collections;
using TMPro;
using UnityEngine;

namespace LongClass
{
    public class PlayerController : MonoBehaviour
    {


        [SerializeField] private PlayerMovement playerMove;
        [SerializeField] private PlayerAnimate playerAnim;
        [SerializeField] private CollectableController collectablesControl;

        public static PlayerController Instance;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Collectable")) return;

            playerAnim.animatorUpdate();
            collectablesControl.Collect(other.gameObject);
        }


        private void Update()
        {
            playerMove.Move();
            playerMove.RotateBody();
            playerMove.RotateCamera();
        }


        private void Awake()
        {

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            Cursor.lockState = CursorLockMode.Locked;
            collectablesControl.UpdateUI();
            collectablesControl.CollectableCoroutine();
        }
    }
}
