using UnityEngine;

namespace N.Fridman.CameraFollow.Scripts
{
    public class CameraFollow2D : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private Transform playerTransform;
        [SerializeField] private string playerTag;
        [SerializeField] [Range(0.5f, 7.5f)] private float movingSpeed = 1.5f;

        private void LateUpdate()
        {
            transform.position = playerTransform.position;
        }
    
    }
}
