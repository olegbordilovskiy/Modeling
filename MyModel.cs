using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AKG
{
    internal class MyModel
    {
        public List<Vector4> SourceVertices { get; set; } = new List<Vector4>();
        public List<Vector4> Vertices { get; set; } = new List<Vector4>();
        public List<int[]> Faces { get; } = new List<int[]>();

        public float translationX { get; set; } = 0.0f;
        public float translationY { get; set; } = 0.0f;
        public float translationZ { get; set; } = 0.0f;


        public float scaleX { get; set; } = 0.5f;
        public float scaleY { get; set; } = 0.5f;
        public float scaleZ { get; set; } = 0.5f;


        public float rotationXAngleRad { get; set; } = 0.0f;
        public float rotationYAngleRad { get; set; } = 1f;
        public float rotationZAngleRad { get; set; } = 0.0f;


        public Vector3 eye { get; set; } = new Vector3(0, 0, 1);
        public Vector3 target { get; set; } = new Vector3(0, 0, 0);
        public Vector3 up { get; set; } = Vector3.UnitY;


        public float cameraWidth { get; set; } = 800.0f;
        public float cameraHeight { get; set; } = 600.0f;
        public float nearPlane { get; set; } = 1.0f;
        public float farPlane { get; set; } = 10000.0f;


        public MyModel(List<Vector4> vertices, List<int[]> faces)
        {
            this.SourceVertices = vertices;
            this.Faces = faces;
        }

        public void UpdateModel()
        {

            float[,] translationMatrix = Matrix.CreateTranslationMatrix(translationX, translationY, translationZ);
            float[,] scaleMatrix = Matrix.CreateScaleMatrix(scaleX, scaleY, scaleZ);

            float[,] rotationX = Matrix.CreateRotationXMatrix(rotationXAngleRad);
            float[,] rotationY = Matrix.CreateRotationYMatrix(rotationYAngleRad);
            float[,] rotationZ = Matrix.CreateRotationZMatrix(rotationZAngleRad);

            float[,] ViewMatrix = Matrix.CreateViewMatrix(eye, target, up);
            float[,] OrthographicProjectionMatrix = Matrix.CreateOrthographicProjectionMatrix(cameraWidth, cameraHeight, nearPlane, farPlane);

            float[,] viewportMatrix = Matrix.CreateViewportMatrix(cameraWidth, cameraHeight);

            float[,] finalMatrix = Matrix.MultiplyMatrices(viewportMatrix, OrthographicProjectionMatrix);

            finalMatrix = Matrix.MultiplyMatrices(finalMatrix, ViewMatrix);

            finalMatrix = Matrix.MultiplyMatrices(finalMatrix, rotationZ);
            finalMatrix = Matrix.MultiplyMatrices(finalMatrix, rotationY);
            finalMatrix = Matrix.MultiplyMatrices(finalMatrix, rotationX);

            finalMatrix = Matrix.MultiplyMatrices(finalMatrix, translationMatrix);
            finalMatrix = Matrix.MultiplyMatrices(finalMatrix, scaleMatrix);


            Vertices.Clear();

            foreach (Vector4 v in SourceVertices)
            {
                Vertices.Add(Matrix.MultiplyMatrixByVector(finalMatrix, v));
            }
        }
    }
}
