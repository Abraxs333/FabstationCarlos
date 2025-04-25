using UnityEngine;
using System.Collections.Generic;


namespace RTG
{
    public class GizmoManager : MonoBehaviour
    {

        
        void OnEnable()
        {
            IntersectionPointSubscriber.OnIntersectionDetected.AddListener(ChangePivot);
            if(objectTransformGizmo!=null) objectTransformGizmo.Gizmo.SetEnabled(true);
        }

        void OnDisable()
        {
            if (objectTransformGizmo != null) objectTransformGizmo.Gizmo.SetEnabled(false);
            IntersectionPointSubscriber.OnIntersectionDetected.RemoveListener(ChangePivot);
        }



        [SerializeField] GameObject ObjectToRotate;
        private ObjectTransformGizmo objectTransformGizmo;


        void Start()
        {
            //CreateGizmo();

        }

        void CreateGizmo()
        {
            objectTransformGizmo = RTGizmosEngine.Get.CreateObjectRotationGizmo();
            objectTransformGizmo.SetTargetObject(ObjectToRotate);
            
        }


        public void ChangePivot(Vector3 _NewPivot)
        {
            if (objectTransformGizmo == null) CreateGizmo();
            LogTools.Print(this, LogTools.LogType.Rotation, "Started the Change Pivot Method" + _NewPivot);

          
            OBB worldOBB = ObjectBounds.GetMeshWorldOBB(ObjectToRotate);
            objectTransformGizmo.SetObjectCustomLocalPivot(ObjectToRotate, ObjectToRotate.transform.InverseTransformPoint(_NewPivot));            
            objectTransformGizmo.SetTransformPivot(GizmoObjectTransformPivot.CustomObjectLocalPivot);
           
        }

    }
}
