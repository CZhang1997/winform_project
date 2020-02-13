/**
 * @Author: Churong Zhang
 * @Date: 2/12/2020
 * @Class: CS 6326.001 - Human Computer Interactions - S20
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asg2_cxz173430
{
    class FileIO
    {
        private string filename;

        public FileIO(string s)
        {
            filename = s;
        }

        public List<RebateData> LoadData()
        {
            List<RebateData> datas = new List<RebateData>();
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(filename);
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] obj = line.Split(',');
                    string first = obj[0];
                    char middle = obj[1][0];
                    string last = obj[2];
                    string address1 = obj[3];
                    string address2 = obj[4];
                    string city = obj[5];
                    string state = obj[6];
                    string zipcode = obj[7];
                    char gender = obj[8][0];
                    string phone = obj[9];
                    string email = obj[10];
                    bool proof = obj[11].Equals("True");
                    DateTime dateRecieve = DateTime.Parse(obj[12]);
                    string firstCharTime = obj[13];
                    string saveTime = obj[14];
                    int backspaceCount = Int32.Parse(obj[15]);
                    RebateData data = new RebateData(first, middle, last, address1, address2,
                        city, state, zipcode, gender, phone, email, proof, dateRecieve,
                        firstCharTime, saveTime, backspaceCount);
                    datas.Add(data);
                }
                file.Close();
                return datas;
            }
            catch (System.IO.FileNotFoundException ex)
            {
                // Write error.
                Console.WriteLine(ex);
                return datas;
            }

        }
        public void saveData(List<RebateData> datas)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(filename);
            foreach (RebateData d in datas)
            {
                file.WriteLine(d.ToString());
            }
            file.Close();
        }
    }
}
