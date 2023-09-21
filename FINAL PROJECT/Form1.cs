using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FINAL_PROJECT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        string patient;
        double temp1, temp2, temp3, temp4, lowDrop, highDrop, avgTemp;
        int rowID;
        string status;

        StreamReader finalData;

        private void button1_Click(object sender, EventArgs e)
        {//Display Patient Status Button

            if (openFileDialog1.ShowDialog() == DialogResult.OK)

            {
                finalData = File.OpenText(openFileDialog1.FileName);
                rowID = 1;

                while (!finalData.EndOfStream)
                {
                    GetData();
                    double lowDrop = DropLowTemp();
                    double highDrop = DropHighTemp();
                    double avgTemp = GetAVGTemp();
                    GetStatus();
                    OutPutData();

                    rowID ++;
                }
            }



        }
        private void GetData()
        {
            try
            {
                patient = finalData.ReadLine();
                temp1 = double.Parse(finalData.ReadLine());
                temp2 = double.Parse(finalData.ReadLine());
                temp3 = double.Parse(finalData.ReadLine());
                temp4 = double.Parse(finalData.ReadLine());
            }

            catch (FormatException ex) 
            {
                MessageBox.Show("Invalid input format. Please enter valid temperature values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        double DropLowTemp()
        {
            try
            {
                double[] temperatures = { temp1, temp2, temp3, temp4 };
                Array.Sort(temperatures);

                lowDrop = temperatures[0];
            }

            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while trying to drop the lowest temperature: {ex.Message}");
                lowDrop = 0;
            }

            return lowDrop;

        }

        double DropHighTemp()
         {

            try
            {
                double[] temperatures = { temp1, temp2, temp3, temp4 };
                Array.Sort(temperatures);

                highDrop = temperatures[3];
            }

            catch(Exception ex)
            {
                MessageBox.Show($"An error occurred while trying to drop the highest temperature: {ex.Message}");
                highDrop = 0; 
            }
           
            return highDrop;

         }

        
         double GetAVGTemp()
         {
            try
            {
                double[] temperatures = { temp1, temp2, temp3, temp4 };
                Array.Sort(temperatures);

                avgTemp = (temperatures[1] + temperatures[2]) / 2.0;
            }
            
            catch(Exception ex)
            {
                MessageBox.Show($"An error occurred while trying to calculate the average temperature: {ex.Message}");
                avgTemp = 0;
            }

            return avgTemp;
         }
        



        private void GetStatus()
        {
            try
            {
                if (avgTemp > 99)
                {
                    status = "Fever";
                }
                else if (avgTemp < 97)
                {
                    status = "LowTemp";
                }
                else
                {
                    status = "Normal";
                }
            }

            catch(Exception ex)
            {
                MessageBox.Show($"An error occurred while trying get the patient status: {ex.Message}");
                status = "";
            }

        }

        private void OutPutData()
        {
            try
            {
                listBox1.Items.Add(rowID.ToString() + "\t" +
                        patient.PadRight(15) + "\t" +
                        temp1.ToString("F2").PadRight(20) + "\t" +
                        temp2.ToString("F2").PadRight(20) + "\t" +
                        temp3.ToString("F2").PadRight(20) + "\t" +
                        temp4.ToString("F2").PadRight(20) + "\t" +
                        lowDrop.ToString("F2").PadRight(20) + "\t" +
                        highDrop.ToString("F2").PadRight(20) + "\t" +
                        avgTemp.ToString("F2").PadRight(20) + "\t" +
                        status.ToString().PadRight(20));
            }

            catch(Exception ex)
            {
                MessageBox.Show("Error when displaying data to listbox! " + ex.Message);

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {//Clear Display Button
           
            listBox1.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {//Exit Button
            this.Close();
            finalData.Close();
        }

    }
}
