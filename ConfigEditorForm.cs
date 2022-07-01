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
using Newtonsoft.Json;

namespace taskk2
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bool b;
            if (File.Exists("connectionConfigs.json"))
                b = true;
            else
            { return; }
            string json = File.ReadAllText("connectionConfigs.json");
            var temp = (Root)JsonConvert.DeserializeObject(json, typeof(Root));

            foreach (Member item in temp.members)
            {
                string[] lines = new string[] { item.LaboratoryName, item.ConnectionString };
                dataGridView2.Rows.Add(lines[0], lines[1]);
            }
        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool a = true;
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                    if (dataGridView2[0,i].Value==null)
                    {
                        label1.Visible = true;
                        label2.Visible = true;
                    return;
                    }
                }
                label1.Visible = false;
                label2.Visible = false;
            Member[] Datavalue = new Member[dataGridView2.Rows.Count - 1];
                for(int i = 0;i < dataGridView2.Rows.Count - 1; i++)
                {
                if (dataGridView2[1, i].Value != null) 
                    Datavalue[i] = new Member(dataGridView2[0, i].Value.ToString(), dataGridView2[1, i].Value.ToString());
                else Datavalue[i] = new Member(dataGridView2[0, i].Value.ToString(), " ");

            }
                string json = "{\"members\":" + JsonConvert.SerializeObject(Datavalue) + "}";
            if (File.Exists("connectionConfigs.json"))
                File.WriteAllText("connectionConfigs.json", json);
            else { File.Create("connectionConfigs.json").Close(); File.WriteAllText("connectionConfigs.json", json); }
            }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    public class Member
    {
        public Member(string laboratoryName, string connectionString)
        {
            LaboratoryName = laboratoryName;
            ConnectionString = connectionString;
        }

        public string LaboratoryName { get; set; }
        public string ConnectionString { get; set; }
        public override string ToString()
        {
            return LaboratoryName + " " + ConnectionString;
        }
    }

    public class Root
    {
        public List<Member>  members { get; set; }
    }

}
