using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPatcher
{
    class Segment
    {
        public decimal start { get; private set; }
        public decimal end { get; private set; }
        public int precedence { get; private set; }
        public decimal avgFreq { get; private set; }

        //private data member to store the actual data from the segment
        private decimal[,] data;
        /// <summary>
        /// Method to return data array.
        /// </summary>
        /// <returns>
        /// Array data.
        /// </returns>
        public decimal[,] getData()
        {
            return data;
        }

        public int getCount()
        {
            return data.GetLength(0);
        }
        /// <summary>
        /// Returns the frequency at point i
        /// </summary>
        /// <param name="index">
        /// Index to return
        /// </param>
        /// <returns>
        /// Value in data array at data point i
        /// </returns>
        public decimal getData(int i)
        {
            return data[i, 0];
        }

        /// <summary>
        /// Constructor for Segment object.
        /// </summary>
        /// <param name="segData">
        /// One dimensional array containing frequency and absorption values.
        /// </param>
        public Segment(decimal[] segData, int precedence)
        {
            data = new decimal[segData.Length / 2, 2];
            for (int i = 0; i < segData.Length; i++)
            {
                data[i / 2, i % 2] = segData[i];
            }
            start = segData[0];
            end = segData[segData.Length - 2];
            avgFreq = Math.Abs((end - start) / 2M);
            this.precedence = precedence;
        }

        /// <summary>
        /// Replaces the data array with an array consisting only of frequencies between startFreq and endFreq
        /// </summary>
        /// <param name="startFreq">
        /// Low frequency bound.
        /// </param>
        /// <param name="endFreq">
        /// High frequency bound.
        /// </param>
        /// <returns>
        /// True if subsegment contains any data points, false if not.
        /// </returns>
        public bool subSegment(decimal startFreq, decimal endFreq)
        {
            int startIndex = 0, endIndex = 0;
            bool started = false;
            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (!started)
                {
                    if (data[i, 0] > startFreq)
                    {
                        startIndex = i;
                        started = true;
                    }
                }
                else
                {
                    if (data[i, 0] > endFreq)
                    {
                        endIndex = i - 1;
                        break;
                    }
                }
            }
            if (!started)
            {
                return false;
            }
            decimal[,] subSeg = new decimal[endIndex - startIndex + 1, 2];
            for (int i = 0; i < subSeg.GetLength(0); i++)
            {
                subSeg[i, 0] = data[startIndex + i, 0];
                subSeg[i, 1] = data[startIndex + i, 1];
            }
            data = subSeg;
            return true;
        }

        public void replaceSubSeg(Segment subSeg)
        {
            int i;
            int start = 0;
            for (i = 0; i < data.GetLength(0); i++)
            {
                if (data[i, 0] > subSeg.start)
                {
                    start = i;
                    break;
                }
            }
            for (i = start; i < data.GetLength(0); i++)
            {
                if (data[i, 0] > subSeg.end)
                {
                    break;
                }
            }

            var newData = new decimal[subSeg.data.GetLength(0) + start + (data.GetLength(0) - 1), 2];
            //decimal[,] lowSubSeg = new decimal[start, 2];
            //decimal[,] highSubSeg = new decimal[data.GetLength(0) - i, 2];
            int j;
            for (j = 0; j < start; j++)
            {
                newData[j, 0] = data[j, 0];
                newData[j, 1] = data[j, 1];
            }
            j = start;
            for (int k = 0; k < subSeg.data.GetLength(0); k++)
            {
                newData[j, 0] = subSeg.data[k, 0];
                newData[j, 1] = subSeg.data[k, 1];
                j++;
            }
            for (int k = i; k < data.GetLength(0); k++)
            {
                newData[j, 0] = data[k, 0];
                newData[j, 1] = data[k, 1];
                j++;
            }
            
        }//end method replaceSubSeg
    }
}
