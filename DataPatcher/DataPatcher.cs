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

            PatchDataFiles.Enabled = true;
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

            List<Segment> segmentedData = segmenter(decData);

            sortSegs(ref segmentedData);

            patcher(ref segmentedData);

            var linesToWrite = new List<string>();
            for (int i = 0; i < segmentedData.Count; i++)
            {
                var line = new StringBuilder();
                var tempData = segmentedData[i].getData();
                for (int j = 0; j < segmentedData[i].getCount(); j++)
                { 
                    line.Append(Convert.ToString(tempData[j, 0])).Append("\t");
                    line.Append(Convert.ToString(tempData[j, 1]));
                }
                linesToWrite.Add(line.ToString());
            }
            if (String.IsNullOrEmpty(filePath))
            {
                saveFile.InitialDirectory = "C:";
            }
            else
            {
                saveFile.InitialDirectory = filePath;
            }
            saveFile.OverwritePrompt = true;
            saveFile.CreatePrompt = false;
            saveFile.FileName = "";
            saveFile.Filter = "Text|*.txt|All Files|*.*";
            saveFile.ShowDialog();
            filePath = saveFile.FileName;

            File.WriteAllLines(filePath, linesToWrite, Encoding.ASCII);

            dataFiles.Clear();
            PatchDataFiles.Enabled = false;
        }

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
                for (int j = 2; j < data[i].Length; j += 2)
                {
                    if (Math.Abs(data[i][j] - data[i][j - 2]) > diff)
                    {
                        temp = new decimal[j - 2 - start + 1];
                        for (int k = 0; k < temp.Length; k++)
                        {
                            temp[k] = data[i][start + k];
                        }
                        segList.Add(new Segment(temp, i));
                        start = j - 1;
                    }
                }//end for loop over data elements
            }//end for loop over data
            return segList;
        }//end method segmenter

        private void patcher(ref List<Segment> segList)
        {
            for (int i = 0; i < segList.Count - 1; i++)
            {
                if (segList[i + 1].start < segList[i].end)//means the two segments overlap
                {
                    if (segList[i + 1].start > segList[i].start)//means segList[i] has a lower start freq than segList[i + 1]
                    {
                        if (segList[i + 1].end > segList[i].end)//means segList[i + 1] has a higher end freq than segList[i]
                        {
                            if (segList[i + 1].precedence < segList[i].precedence)//means segList[i] should be trimmed
                            {
                                segList[i].subSegment(0M, segList[i + 1].start);
                            }
                            else//means trim segList[i + 1]
                            {
                                segList[i + 1].subSegment(segList[i].end, decimal.MaxValue);
                            }
                        }
                        else//means segList[i + 1] is entirely within the range of segList[i]
                        {
                            if (segList[i + 1].precedence < segList[i].precedence)//if segList[i + 1] has lower precedencecall replaceSubSeg to insert the data from segList[i + 1] into segList[i]
                            {
                                segList[i].replaceSubSeg(segList[i + 1]);
                            }
                            //if not lower precedence then remove it without doing anything, if it was then it still needs to be removed
                            segList.RemoveAt(i + 1);
                            i--;
                        }
                    }
                    else//means that segList[i] is completely within the bounds of segList[i + 1]
                    {
                        if (segList[i + 1].precedence > segList[i].precedence)//means call replaceSubSeg
                        {
                            segList[i + 1].replaceSubSeg(segList[i]);
                        }
                        //if not just remove, if true it still needs to be cut
                        segList.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        private void sortSegs(ref List<Segment> segList)
        {
            bool swapped = true;
            Segment temp;
            while (swapped)
            {
                swapped = false;
                for (int i = 1; i < segList.Count; i++)
                {
                    if (segList[i - 1].avgFreq > segList[i].avgFreq)
                    {
                        temp = segList[i - 1];
                        segList[i - 1] = segList[i];
                        segList[i] = temp;
                        swapped = true;
                    }//end if
                }//end for
            }//end while
        }//end sortSegs
    }
}
