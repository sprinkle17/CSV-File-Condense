//dalton sprinkle
//Final Project pt. 2
//4/15/2019
//Description: reads rome.csv and puts lines into list. Then changes to a double list and divides by '10.0' to get appropriate values. Condenses data by month.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;

namespace Dataproject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //creating strings to use utilize steamreader and split
            List<string> Date = new List<string>();
            List<string> Precipitation = new List<string>();
            List<string> Max = new List<string>();
            List<string> Min = new List<string>();

            //uses steamreader to read in file. checks for -9999 value and if found, skips over it.
            using (var reader = new StreamReader("Rome.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var value = line.Split(',');
                    if (value[1].ToString() != "-9999" && value[2].ToString() != "-9999" && value[3].ToString() != "-9999")
                    {
                        Date.Add(value[0]);
                        Precipitation.Add(value[1]);
                        Max.Add(value[2]);
                        Min.Add(value[3]);
                    }
                }
            }

            //creating double lists from string lists
            //not making date a double list, so we can split each value for the year to condense data

            List<double> Precipitationint = new List<double>();
            List<double> Maxint = new List<double>();
            List<double> Minint = new List<double>();

            //dividing each element by 10.0 to change into appropriate value

            for (int i = 0; i < Precipitation.Count; i++)
            {
                Precipitationint.Add(int.Parse(Precipitation[i]) / 10.0);
                Maxint.Add(int.Parse(Max[i]) / 10.0);
                Minint.Add(int.Parse(Min[i]) / 10.0);
            }

            List<string> condensedDate = new List<string>();
            List<double> condensedMin = new List<double>();
            List<double> condensedMax = new List<double>();
            List<double> condensedPrecipitation = new List<double>();


            int yearstart = 1951;
            int monthstart = 1;
            double conMax = 0;
            double conPrec = 0;
            double conMin = 0;
            int indexcount = 0;
            double dividecount = 0;

            foreach (var i in Date)
            {
                var split = i.ToCharArray();
                var year = int.Parse(split[0].ToString() + split[1].ToString() + split[2].ToString() + split[3].ToString());
                var month = int.Parse(split[4].ToString() + split[5].ToString());


                //incrementing month and year variables

                if (monthstart == 13)
                {
                    monthstart = 1;
                    yearstart += 1;
                }

                //condense data by month of each year
                if (yearstart == year)
                {
                    if (monthstart == month)
                    {
                        conMax += Maxint[indexcount];
                        conMin += Minint[indexcount];
                        conPrec += Precipitationint[indexcount];
                        dividecount += 1;
                    }
                    if (monthstart != month)
                    {
                        condensedMax.Add(conMax/ dividecount);
                        condensedMin.Add(conMin/ dividecount);
                        condensedPrecipitation.Add(conPrec/ dividecount);
                        condensedDate.Add(yearstart.ToString() + "-" + monthstart.ToString());
                        monthstart = month;
                        yearstart = year;
                        conMax = 0;
                        conPrec = 0;
                        conMin = 0;
                        dividecount = 0;
                        conMax += Maxint[indexcount];
                        conMin += Minint[indexcount];
                        conPrec += Precipitationint[indexcount];
                    }
                }
                if (yearstart != year)
                {
                    condensedMax.Add(conMax/ dividecount);
                    condensedMin.Add(conMin/ dividecount);
                    condensedPrecipitation.Add(conPrec/ dividecount);
                    condensedDate.Add(yearstart.ToString() +"-"+ monthstart.ToString());
                    monthstart = month;
                    yearstart = year;
                    conMax = 0;
                    conPrec = 0;
                    conMin = 0;
                    dividecount = 0;
                    conMax += Maxint[indexcount];
                    conMin += Minint[indexcount];
                    conPrec += Precipitationint[indexcount];
                }
                indexcount += 1;
            }
            MessageBox.Show(condensedDate.Count.ToString(), "total records");
            for (int i = 0; i < condensedDate.Count; i++)
            {
                MessageBox.Show(condensedDate[i], "Date is:");
                MessageBox.Show(condensedPrecipitation[i].ToString(), "Precipitation is:");
                MessageBox.Show(condensedMax[i].ToString(), "Max temparature average is:");
                MessageBox.Show(condensedMin[i].ToString(), "Min temperature average is:");
            }
        }
    }
}