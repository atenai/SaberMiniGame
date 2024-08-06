using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dico.HyperCasualGame.Minigame
{
    public class Cube_Cylinder : MonoBehaviour
    {
        bool isHitCylinder = false;
        public bool IsHitCylinder
        {
            get
            {
                return isHitCylinder;
            }
            private set
            {
                isHitCylinder = value;
            }
        }

        bool isHitCylinderActive = false;

        //[SerializeField]
        //GameObject gameObjectCubeSaberPackage;
        [SerializeField]
        Cube_Saber cubeSaber;
        //public float f_TransrateZ = 0.0f;

        void Start()
        {
            //cube_Saber = GameObject.Find("GameObject_Cube_Saber_Package").GetComponent<Cube_Saber>();
            //cubeSaber = gameObjectCubeSaberPackage.GetComponent<Cube_Saber>();
        }

        void Update()
        {
            //if (isHitCylinderActive == true)
            //{
            //    IsHitCylinder = true;
            //}
            //else if (isHitCylinderActive == false)
            //{
            //    IsHitCylinder = false;
            //}

            //Debug.Log("IsHitCylinder : " + IsHitCylinder);
        }

        void OnTriggerEnter(Collider hit)
        {
            //Debug.Log("接触したよ");

            if (hit.CompareTag("Cube_Enemy"))
            {
                //このCube_Cylinder.csは意味の無いスクリプトかもしれん
                //IsHitCylinder = true;
                //isHitCylinderActive = true;
                //Debug.Log("IsHitCylinder : " + IsHitCylinder);
                //cube_Saber.transform.position -= new Vector3(0, 0, f_TransrateZ) * Time.deltaTime;
                //cube_Saber.Set_b_SaberMoveTrue();
            }
        }

        private void OnTriggerStay(Collider hit)
        {
            if (hit.CompareTag("Cube_Enemy"))
            {
                //IsHitCylinder = true;
                //isHitCylinderActive = true;
                //Debug.Log("IsHitCylinder : " + IsHitCylinder);
                //cube_Saber.transform.position -= new Vector3(0, 0, f_TransrateZ) * Time.deltaTime;
                //cube_Saber.Set_b_SaberMoveTrue();
            }
        }

        private void OnTriggerExit(Collider hit)
        {
            if (hit.CompareTag("Cube_Enemy"))
            {
                //IsHitCylinder = false;
                //isHitCylinderActive = false;
                //Debug.Log("IsHitCylinder : " + IsHitCylinder);
                //cube_Saber.transform.position -= new Vector3(0, 0, f_TransrateZ) * Time.deltaTime;
                //cube_Saber.Set_b_SaberMoveTrue();
            }
        }

    }
}