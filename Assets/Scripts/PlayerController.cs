using System.Collections;
using TMPro;
using UnityEngine;

namespace LongClass
{
    public class PlayerController : MonoBehaviour
    {
        private static readonly int CollectableCollectedHash = Animator.StringToHash("CollectableCollected");

        [SerializeField] private float movementSpeed = 7;
        [SerializeField] private float cameraRotateSpeed = 45;
        [SerializeField] private float minCameraXRotation = 10;
        [SerializeField] private float maxCameraXRotation = 35;

        [SerializeField] private Transform cameraTransform;

        [SerializeField] private int targetCollectableCount;
        [SerializeField] private GameObject collectablePrefab;
        [SerializeField] private Transform collectableSpawnTopRightHandle;
        [SerializeField] private Transform collectableSpawnBottomLeftHandle;

        [SerializeField] private TMP_Text collectablesCountText;
        [SerializeField] private Animator uiAnimator;

        [SerializeField] private float minCollectableSpawnTime = 3;
        [SerializeField] private float maxCollectableSpawnTime = 6;

        private int collectedCollectables;
        private Coroutine spawnCoroutine;

        private void Collect(GameObject collectable)
        {
            Destroy(collectable);
            collectedCollectables++;
            UpdateUI();

            if (collectedCollectables == targetCollectableCount)
            {
                StopCoroutine(spawnCoroutine);
                enabled = false;
            }
        }

        private void UpdateUI()
        {
            collectablesCountText.text = $"{collectedCollectables}/{targetCollectableCount}";
            uiAnimator.SetTrigger(CollectableCollectedHash);
        }

        private void Move()
        {
            var movementDirection =
                Input.GetAxis("Horizontal") * transform.right +
                Input.GetAxis("Vertical") * transform.forward;

            transform.position += movementDirection.normalized * Time.deltaTime * movementSpeed;
        }

        private void RotateBody()
        {
            var rotationDelta = Input.GetAxis("Mouse X") * cameraRotateSpeed * Time.deltaTime;
            var newYRotation = transform.rotation.eulerAngles.y + rotationDelta;

            transform.localRotation = Quaternion.Euler(0, newYRotation, 0);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Collectable")) return;

            Collect(other.gameObject);
        }

        private void RotateCamera()
        {
            var rotationDelta = Input.GetAxis("Mouse Y") * cameraRotateSpeed * Time.deltaTime;
            var newXRotation = cameraTransform.localRotation.eulerAngles.x - rotationDelta;
            newXRotation = Mathf.Clamp(newXRotation, minCameraXRotation, maxCameraXRotation);

            cameraTransform.localRotation = Quaternion.Euler(newXRotation, 0, 0);
        }

        private void Update()
        {
            Move();
            RotateBody();
            RotateCamera();
        }

        private IEnumerator SpawnCollectable()
        {
            while (true)
            {
                var topRight = collectableSpawnTopRightHandle.position;
                var bottomLeft = collectableSpawnBottomLeftHandle.position;
                var position = new Vector3(
                    Random.Range(bottomLeft.x, topRight.x),
                    topRight.y,
                    Random.Range(bottomLeft.z, topRight.z));
                Instantiate(collectablePrefab, position, Quaternion.identity);

                yield return new WaitForSeconds(Random.Range(minCollectableSpawnTime, maxCollectableSpawnTime));
            }
        }

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            UpdateUI();
            spawnCoroutine = StartCoroutine(SpawnCollectable());
        }
    }
}
