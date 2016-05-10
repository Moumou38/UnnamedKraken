using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;


public class PathTransitionZone : EditorWindow {

    [MenuItem("Environment Tool/Transition Path")]
    public static void ShowWindow()
    {
        // opens a window to build an AssetBundle using an XML file to collect the data
        EditorWindow e = EditorWindow.GetWindow(typeof(PathTransitionZone), false, "Path Transition Zone Window");
    }

    void OnGUI()
    {
        index = EditorGUILayout.Popup(index, options);
        if (GUILayout.Button("Create"))
            InstantiatePrimitive();

    }
    void InstantiatePrimitive()
    {
        GameObject root = new GameObject();
        root.name = "Intersection";
        root.transform.position = SceneView.lastActiveSceneView.camera.transform.position + SceneView.lastActiveSceneView.camera.transform.forward;
        Intersections inter = root.AddComponent<Intersections>();
        BoxCollider collider = root.AddComponent<BoxCollider>();
        collider.isTrigger = true; 
        inter.setCollider(collider); 
        List<IntersectionExit> list = new List<IntersectionExit>();

        for (int i = 0; i < index + 1; ++i)
        {
            GameObject intersectionExit = new GameObject();
            intersectionExit.name = "Intersection Exit";
            IntersectionExit interE = intersectionExit.AddComponent<IntersectionExit>();
            BoxCollider colliderExit = intersectionExit.AddComponent<BoxCollider>();
            colliderExit.isTrigger = true; 
            interE.setCollider(colliderExit);
            list.Add(interE);
            intersectionExit.transform.SetParent(root.transform);
            intersectionExit.transform.localPosition = new Vector3(0, 0, 0);
        }

        inter.setIntersectionExits(list);
    }

    public string[] options = new string[] { "1", "2", "3", "4" };
    public int index = 0;
    int nbPaths = 0; 
}
