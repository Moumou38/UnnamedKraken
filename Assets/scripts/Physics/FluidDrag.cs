using UnityEngine;

// READ THIS : http://www.performancesimulations.com/wp/how-to-get-big-speed-increases-in-unitys-physics-or-any-math-heavy-code/

public class FluidDrag : MonoBehaviour
{
    public float viscosityDrag = 1f;
    private Rigidbody rig;

    private float x_area;
    private float y_area;
    private float z_area;


    void Start()
    {
        rig = GetComponent<Rigidbody>();

        // Calculate surface areas for each face:
        x_area = transform.localScale.y * transform.localScale.z;
        y_area = transform.localScale.x * transform.localScale.z;
        z_area = transform.localScale.x * transform.localScale.y;
    }

    void FixedUpdate()
    {
        // Cache positive axis vectors:
        Vector3 forward = transform.forward;
        Vector3 up = transform.up;
        Vector3 right = transform.right;
        // Find centers of each of box's faces
        Vector3 xpos_face_center = (right * transform.localScale.x / 2) + transform.position;
        Vector3 ypos_face_center = (up * transform.localScale.y / 2) + transform.position;
        Vector3 zpos_face_center = (forward * transform.localScale.z / 2) + transform.position;

        // FRONT (posZ):
        Vector3 pointVelPosZ = rig.GetPointVelocity(zpos_face_center); 
        Vector3 fluidDragVecPosZ = -forward * Vector3.Dot(forward, pointVelPosZ) * z_area * viscosityDrag; // TO DO : compute dot product myself      
        rig.AddForceAtPosition(fluidDragVecPosZ * 2, zpos_face_center);  // Apply force at face's center, in the direction opposite the face normal


        // TOP (posY):
        Vector3 pointVelPosY = rig.GetPointVelocity(ypos_face_center);
        Vector3 fluidDragVecPosY = -up * Vector3.Dot(up, pointVelPosY) * y_area * viscosityDrag;
        rig.AddForceAtPosition(fluidDragVecPosY * 2, ypos_face_center);

        // RIGHT (posX):
        Vector3 pointVelPosX = rig.GetPointVelocity(xpos_face_center);
        Vector3 fluidDragVecPosX = -right * Vector3.Dot(right, pointVelPosX) * x_area * viscosityDrag;
        rig.AddForceAtPosition(fluidDragVecPosX * 2, xpos_face_center);


        // TO DO : opposite faces and optimisation 


        Vector3 xneg_face_center = -(right * transform.localScale.x / 2) + transform.position; // Opposite faces
        Vector3 yneg_face_center = -(up * transform.localScale.y / 2) + transform.position;
        Vector3 zneg_face_center = -(forward * transform.localScale.z / 2) + transform.position;


        Vector3 cachedTransformForward = transform.forward; // already normalized ?
        Vector3 fluidDragVecNegZ;
        Vector3 pointVelNegZ = rig.GetPointVelocity(zneg_face_center);

        float dot;
        dot = -cachedTransformForward.x * pointVelNegZ.x +
            -cachedTransformForward.y * pointVelNegZ.y +
            -cachedTransformForward.z * pointVelNegZ.z;

        fluidDragVecNegZ.x = cachedTransformForward.x * dot * z_area * viscosityDrag;
        fluidDragVecNegZ.y = cachedTransformForward.y * dot * z_area * viscosityDrag;
        fluidDragVecNegZ.z = cachedTransformForward.z * dot * z_area * viscosityDrag;
        rig.AddForceAtPosition(fluidDragVecNegZ * 2, zneg_face_center);

        Vector3 fluidDragVecNegY;
        Vector3 pointVelNegY = rig.GetPointVelocity(yneg_face_center);

        dot = -cachedTransformForward.x * pointVelNegZ.x +
            -cachedTransformForward.y * pointVelNegZ.y +
            -cachedTransformForward.z * pointVelNegZ.z;

        fluidDragVecNegY.x = cachedTransformForward.x * dot * y_area * viscosityDrag;
        fluidDragVecNegY.y = cachedTransformForward.y * dot * y_area * viscosityDrag;
        fluidDragVecNegY.z = cachedTransformForward.z * dot * y_area * viscosityDrag;
        rig.AddForceAtPosition(fluidDragVecNegY * 2, yneg_face_center);

        Vector3 fluidDragVecNegX;
        Vector3 pointVelNegX = rig.GetPointVelocity(xneg_face_center);

        dot = -cachedTransformForward.x * pointVelNegX.x +
            -cachedTransformForward.y * pointVelNegX.y +
            -cachedTransformForward.z * pointVelNegX.z;

        fluidDragVecNegX.x = cachedTransformForward.x * dot * x_area * viscosityDrag;
        fluidDragVecNegX.y = cachedTransformForward.y * dot * x_area * viscosityDrag;
        fluidDragVecNegX.z = cachedTransformForward.z * dot * x_area * viscosityDrag;
        rig.AddForceAtPosition(fluidDragVecNegX * 2, xneg_face_center);
    }
}

