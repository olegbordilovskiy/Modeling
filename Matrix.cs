using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace AKG
{
    internal class Matrix
    {
        public static float[,] MultiplyMatrices(float[,] matrixA, float[,] matrixB)
        {
            int rowsA = matrixA.GetLength(0);
            int colsA = matrixA.GetLength(1);
            int rowsB = matrixB.GetLength(0);
            int colsB = matrixB.GetLength(1);

            if (colsA != rowsB)
                throw new ArgumentException("Number of columns in matrixA must be equal to number of rows in matrixB.");

            float[,] result = new float[rowsA, colsB];

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    float sum = 0.0f;
                    for (int k = 0; k < colsA; k++)
                    {
                        sum += matrixA[i, k] * matrixB[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }

        public static Vector4 MultiplyMatrixByVector(float[,] matrix, Vector4 vector)
        {
            int matrixRows = matrix.GetLength(0);
            int matrixCols = matrix.GetLength(1);
            int vectorLength = 4;

            if (matrixCols != vectorLength)
                throw new ArgumentException("Matrix column count must be equal to vector length.");

            Vector4 result = new Vector4();

            for (int i = 0; i < matrixRows; i++)
            {
                float sum = 0;
                for (int j = 0; j < matrixCols; j++)
                {
                    sum += matrix[i, j] * vector[j];
                }
                result[i] = sum;
            }

            return result;
        }



        public static float[,] CreateTranslationMatrix(float translationX, float translationY, float translationZ)
        {
            return new float[,]
            {
                {1, 0, 0, translationX },
                {0, 1, 0, translationY },
                {0, 0, 1, translationZ },
                {0, 0, 0, 1}
            };
        }
        public static float[,] CreateScaleMatrix(float scaleX, float scaleY, float scaleZ)
        {
            return new float[,]
            {
                { scaleX, 0, 0, 0 },
                { 0, scaleY, 0, 0 },
                { 0, 0, scaleZ, 0 },
                { 0, 0, 0, 1 }
            };
        }
        public static float[,] CreateRotationXMatrix(float angleInRadians)
        {
            float cosTheta = (float)Math.Cos(angleInRadians);
            float sinTheta = (float)Math.Sin(angleInRadians);

            return new float[,]
            {
            { 1, 0, 0, 0 },
            { 0, cosTheta, -sinTheta, 0 },
            { 0, sinTheta, cosTheta, 0 },
            { 0, 0, 0, 1 }
            };
        }

        public static float[,] CreateRotationYMatrix(float angleInRadians)
        {
            float cosTheta = (float)Math.Cos(angleInRadians);
            float sinTheta = (float)Math.Sin(angleInRadians);

            return new float[,]
            {
            { cosTheta, 0, sinTheta, 0 },
            { 0, 1, 0, 0 },
            { -sinTheta, 0, cosTheta, 0 },
            { 0, 0, 0, 1 }
            };
        }

        public static float[,] CreateRotationZMatrix(float angleInRadians)
        {
            float cosTheta = (float)Math.Cos(angleInRadians);
            float sinTheta = (float)Math.Sin(angleInRadians);

            return new float[,]
            {
            { cosTheta, -sinTheta, 0, 0 },
            { sinTheta, cosTheta, 0, 0 },
            { 0, 0, 1, 0 },
            { 0, 0, 0, 1 }
            };
        }

        public static float[,] CreateViewMatrix(Vector3 eye, Vector3 target, Vector3 up)
        {

            Vector3 zAxis;
            if (eye - target == Vector3.Zero) zAxis = new(5, 0, 0);
            else
                zAxis = Vector3.Normalize(eye - target);


            Vector3 xAxis = Vector3.Normalize(Vector3.Cross(up, zAxis));
            Vector3 yAxis = up;

            float[,] viewMatrix = new float[,]
            {
                { xAxis.X, xAxis.Y, xAxis.Z, -Vector3.Dot(xAxis, eye) },
                { yAxis.X, yAxis.Y, yAxis.Z, -Vector3.Dot(yAxis, eye) },
                { zAxis.X, zAxis.Y, zAxis.Z, -Vector3.Dot(zAxis, eye) },
                { 0, 0, 0, 1 }
            };

            return viewMatrix;
        }

        public static float[,] CreateOrthographicProjectionMatrix(float width, float height, float nearPlane, float farPlane)
        {
            float w = width;
            float h = height;
            float zn = nearPlane;
            float zf = farPlane;

            float[,] projectionMatrix = new float[,]
            {
                { 2.0f / w, 0, 0, 0 },
                { 0, 2.0f / h, 0, 0 },
                { 0, 0, 1.0f / (zf - zn), 0 },
                { 0, 0, zn / (zn - zf), 1 }
            };

            return projectionMatrix;
        }



        public static float[,] CreateViewportMatrix(float width, float height)
        {
            float xMin = 0;
            float xMax = width;
            float yMin = 0;
            float yMax = height;

            float xTranslate = (xMax + xMin) / 2.0f;
            float yTranslate = (yMax + yMin) / 2.0f;

            float xScale = (xMax - xMin) / 1f;
            float yScale = (yMax - yMin) / 1f;

            float[,] projectionToViewportMatrix = new float[,]
            {
                { xScale, 0, 0, xTranslate },
                { 0, -yScale, 0, yTranslate },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }

            };

            return projectionToViewportMatrix;
        }




    }
}
