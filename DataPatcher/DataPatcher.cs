using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DataPatcher
{
    public partial class DataPatcher : Form
    {
        public DataPatcher()
        {
            InitializeComponent();
        }

        string filePath;
        List<string[]> dataFiles = new List<string[]>();

        private void button1_Click(object sender, EventArgs e)
        {
            string[] dataFile = {};
            addDataFile.Title = "Select a data file to patch";
            if (String.IsNullOrEmpty(filePath))
            {
                addDataFile.InitialDirectory = "C:\\";
            }
            else
            {
                addDataFile.InitialDirectory = filePath;
            }
            addDataFile.FileName = "";
            addDataFile.Filter = "Text|*.txt|All Files|*.*";

            addDataFile.ShowDialog();
            filePath = addDataFile.FileName;

            using (StreamReader dataReader = new StreamReader(filePath))
            {
                char[] delimiters = new char[] { '\t', '\r' };
                string nextLine;
                while ((nextLine = dataReader.ReadLine()) != null)
                {
                    string[] dataLine = nextLine.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    dataFile.Concat(dataLine).ToArray();
                }
                dataFiles.Add(dataFile);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var decData = new List<decimal[]>();
            decimal[] tempDec;
            for (int i = 0; i < dataFiles.Count; i++)
            {
                tempDec = new decimal[dataFiles[i].Length];
                for (int j = 0; j < dataFiles[i].Length; j++)
                {
                    tempDec[j] = Convert.ToDecimal(dataFiles[i][j]);
                }
                decData.Add(tempDec);
            }
        }//end open data file button

        private List<Segment> segmenter(List<decimal[]> data)
        {
            var segList = new List<Segment>();
            for (int i = 0; i < data.Count; i++)
            {
                //find average step size based off of first 10 data points
                decimal diff = 0.0M;
                //need to use step of 2 and start at 1 because every other data point is presumed to be a frequency point
                for (int k = 1; k < 22; k += 2)
                {
                    diff += data[i][k + 2] - data[i][k];
                }
                //divide by 10 to get the average step between data points.
                diff /= 10.0M;                
                //now multiply by 4 and set that as the threshold for making a new segment
                diff *= 4.0M;
                //check to make sure that diff is a positive value
                diff = Math.Abs(diff);

                decimal[] temp;

                //to store where the next segment should start at
                int start = 0;
                for (int j = 3; j < data[i].Length; j += 2)
                {
                    if (Math.Abs(data[i][j] - data[i][j - 2]) > diff)
                    {
                        //need to add code here to take contiguous portion of data from the data[i] array to pass to segment constructor
                        start = j - 3;
                    }
                }
            }


            return segList;
        }//end method segmenter

        private decimal[] patcher(List<Segment> segList)
        {
            decimal[] patchedData;


            return patchedData;
        }
    }
}
