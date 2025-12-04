using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Main
{
    public class CornerMeshes : MonoBehaviour
    {
        public static CornerMeshes Instance;

        public GameObject MeshObject;

        private Dictionary<string, Mesh> meshes = new Dictionary<string, Mesh>();


        private void Awake()
        {
            Instance = this;

            initialize();
        }

        private void initialize()
        {
            foreach (Transform child in MeshObject.transform)
            {
                string childName = child.name;

                Mesh mesh = child.GetComponent<MeshFilter>().sharedMesh;

                meshes.Add(childName, mesh);
            }
        }

        public Mesh GetCornerMesh(int bitMask, int level)
        {
            Mesh returnMesh;

            if (level > 1)
            {
                if (meshes.TryGetValue(bitMask.ToString(), out returnMesh))
                {
                    return returnMesh;
                }
            }
            else if (level == 0)
            {
                if (meshes.TryGetValue(0 + "_" + bitMask.ToString(), out returnMesh))
                {
                    return returnMesh;
                }
            }
            else if (level == 1)
            {
                if (meshes.TryGetValue(1 + "_" + bitMask.ToString(), out returnMesh))
                {
                    return returnMesh;
                }
            }

            return null;
        }
    }
}