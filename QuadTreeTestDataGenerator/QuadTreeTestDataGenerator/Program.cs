using System;
using System.IO;
using System.Windows.Forms;

namespace QuadTreeTestDataGenerator
{
    class Program
    {
        private static readonly int ONE_THOUSAND = 1000;
        private static readonly int TEN_THOUSAND = 10000;
        private static readonly int ONE_HUNDRED_THOUSAND = 100000;
        private static readonly int ONE_MILLION = 1000000;
        private static readonly int TEN_MILLION = 10000000;

        private static readonly int MIN_LATITUDE = -180;
        private static readonly int MIN_LONGITUDE = -90;

        private static readonly int MAX_LATITUDE = 180;
        private static readonly int MAX_LONGITUDE = 90;

        [STAThread]
        static void Main(string[] args)
        {
            string FILE_PATH = GetFilePath();
            File.WriteAllText(FILE_PATH, String.Empty);

            for(int i = 0; i < 1; i++ )
            {
                string[] coordinates = GenerateOneMillionXYCoords();
                File.AppendAllLines(FILE_PATH, coordinates);
            }
        }

        private static string GetFilePath()
        {
            string fileName = "";
            Stream myStream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            fileName = openFileDialog.FileName;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            return fileName;
        }

        private static string[] GenerateOneMillionXYCoords()
        {
            Random r = new Random();
            string[] coordinates = new String[ONE_MILLION];
            for(int i = 0; i < coordinates.Length; i++)
            {
                coordinates[i] = GenerateXYCoords(r);
            }
            return coordinates;
        }

        private static string GenerateXYCoords(Random r)
        {
            return GenerateFormattedXYCoords(r);
        }

        private static string GenerateFormattedXYCoords(Random r)
        {
            return GenerateXCoord(r) + "," + GenerateYCoord(r) + "," + GenerateValue(r);
        }

        private static double GenerateXCoord(Random r)
        {
            return GenerateRandomDoubleNumber(r, MIN_LATITUDE, MAX_LATITUDE);
        }

        private static double GenerateYCoord(Random r)
        {
            return GenerateRandomDoubleNumber(r, MIN_LONGITUDE, MAX_LONGITUDE);
        }

        private static double GenerateValue(Random r)
        {
            return GenerateRandomDoubleNumber(r, 0, 500);
        }

        private static int GenerateRandomIntegerNumber(Random r, int minValue, int maxValue)
        {
            return r.Next(minValue, maxValue);
        }

        private static double GenerateRandomDoubleNumber(Random r, double minValue, double maxValue)
        {
            return Math.Round(r.NextDouble() * (maxValue - minValue) + minValue, 2);
        }
    }
}
