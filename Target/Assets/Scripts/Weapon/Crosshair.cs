//using UnityEngine;
//#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
//using UnityEngine.InputSystem;
//#endif
////namespace StarterAssets
////{
//    public class Crosshair : MonoBehaviour
//    {
//    //StarterAssetsInputs inputSystem;
//    [SerializeField] private Camera mainCamera;
        
//        public void Update()
//        {
//        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
//        mouseWorldPos.z = 0f;
//        transform.position = mouseWorldPos;
//        //Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
//        //if (Physics.Raycast(ray, out RaycastHit raycastHit))
//        //{
//        //    transform.position = raycastHit.point;
//        //}
//    }
//}
