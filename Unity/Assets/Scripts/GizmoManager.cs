using UnityEngine;
using System.Collections.Generic;


namespace RTG
{
    public class GizmoManager : MonoBehaviour
    {

        
        void OnEnable()
        {
            IntersectionPointSubscriber.OnIntersectionDetected.AddListener(ChangePivot);
        }

        void OnDisable()
        {
            IntersectionPointSubscriber.OnIntersectionDetected.RemoveListener(ChangePivot);
        }



        [SerializeField] GameObject ObjectToRotate;
        private ObjectTransformGizmo objectTransformGizmo;


        void Start()
        {
            objectTransformGizmo = RTGizmosEngine.Get.CreateObjectRotationGizmo();
            objectTransformGizmo.SetTargetObject(ObjectToRotate);

        }


        public void ChangePivot(Vector3 _NewPivot)
        {
            LogTools.Print(this, LogTools.LogType.Rotation, "Started the Change Pivot Method" + _NewPivot);

          
            OBB worldOBB = ObjectBounds.GetMeshWorldOBB(ObjectToRotate);
            objectTransformGizmo.SetObjectCustomLocalPivot(ObjectToRotate, ObjectToRotate.transform.InverseTransformPoint(_NewPivot));            
            objectTransformGizmo.SetTransformPivot(GizmoObjectTransformPivot.CustomObjectLocalPivot);
           
        }

    }
}
