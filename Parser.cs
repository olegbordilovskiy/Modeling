using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using System.Globalization;

namespace AKG
{
    internal class Parser
    {
        public List<Vector4> Vertices { get; } = new List<Vector4>();
        public List<int[]> Faces { get; } = new List<int[]>();

        public void ParseFile(string filename)
        {

            using (StreamReader sr = new StreamReader(filename))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    ParseLine(line);
                }
            }
        }
        private void ParseLine(string line)
        {
            string[] parts = line.Split(' ');

            if (parts[0] == "v") AddVertix(parts);
            if (parts[0] == "f") AddFace(parts);

        }

        private void AddVertix(string[] parts)
        {   
            int i = 1;
            while (parts[i] == "")
                i++;
            float x = float.Parse(parts[i++], CultureInfo.InvariantCulture);
            float y = float.Parse(parts[i++], CultureInfo.InvariantCulture);
            float z = float.Parse(parts[i++], CultureInfo.InvariantCulture);
            float w = 1.2f;
            Vertices.Add(new Vector4(x, y, z, w));
        }

        private void AddFace(string[] parts)
        {
            int[] indices = new int[(parts.Length - 1)];
            for (int i = 1, j = 0; i < parts.Length; i++)
            {
                string[] faceIndices = parts[i].Split('/');

                // Первое значение - вершина
                indices[j++] = int.Parse(faceIndices[0]) - 1;

                // Второе значение - текстурные координаты, пропускаем

                // Третье значение - нормали
                //indices[j++] = int.Parse(faceIndices[2]) - 1;
            }
            Faces.Add(indices);
        }
    }
}
