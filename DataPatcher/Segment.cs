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
        public Segment(decimal[] segData)
        {
            data = new decimal[segData.Length / 2, 2];
            for (int i = 0; i < segData.Length; i++)
            {
                data[i / 2, i % 2] = segData[i];
            }
            start = segData[0];
            end = segData[segData.Length - 2];
        }
    }
}
