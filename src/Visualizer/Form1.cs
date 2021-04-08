using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visualizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Overload agar Console.WriteLine ditampilkan pada richTextbox1
            TextBoxWriter writer = new TextBoxWriter(richTextBox1);
            Console.SetOut(writer);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        //Graph MSAGL
        Microsoft.Msagl.Drawing.Graph graph;
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer(); // Graph viewer engine
        Graph virtualGraph;
        bool graphLoaded = false;

        //Tombol Browse
        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.ResetText();
            comboBox2.ResetText();
            //File Dialog buat buka file external type .txt
            openFileDialog1.Filter = "*.txt|*.txt|All files (*.*)|*.*";
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.Title = "Masukkan file eksternal";

            int nNode;
            // Show file dialog
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK) // Kalo file dialog dapet file
            {
                graph = new Microsoft.Msagl.Drawing.Graph("graph"); // Initialize new MSAGL graph                
                // baca file input
                using (StreamReader sr = new StreamReader(openFileDialog1.OpenFile()))
                {
                    string line = sr.ReadLine();
                    if (line == null || line == "0")
                    {
                        MessageBox.Show("File Kosong");
                    }
                    else
                    {
                        graph = new Microsoft.Msagl.Drawing.Graph("graph");
                        graphLoaded = true;
                        //Cek ada berapa simpul
                        nNode = Int32.Parse(line);
                        textBox6.Text = openFileDialog1.FileName;//Tampilin nama file
                        string[] vertix = new string[nNode * 2];//untuk ngelist semua simpul di file eksternal
                        int x = 0;
                        while (sr.Peek() >= 0)
                        {
                            line = sr.ReadLine();
                            string[] cur_line = line.Split(' ');
                            vertix[x] = cur_line[0];
                            vertix[x + 1] = cur_line[1];
                            x += 2;

                            //nambahin edge untuk visualisasi graph
                            var Edge = graph.AddEdge((cur_line[0]), (cur_line[1]));
                            Edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                            Edge.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.None;
                        }

                        //Dicari elemen uniknya buat input pembuatan graf
                        vertix = vertix.Distinct().ToArray();
                        Array.Sort(vertix);
                        
                        //inisialisasi graf
                        virtualGraph = new Graph(vertix.Length, vertix);

                        //Tambahin isi dropdown (comboBox)
                        comboBox1.Items.Clear();
                        comboBox2.Items.Clear();
                        for (int i =0; i <vertix.Length; i++)
                        {
                            comboBox1.Items.Add(vertix[i]);
                            comboBox2.Items.Add(vertix[i]);
                        }

                        // Bind graph to viewer engine
                        viewer.Graph = graph;
                        // Bind viewer engine to the panel
                        panel1.SuspendLayout();
                        viewer.Dock = System.Windows.Forms.DockStyle.Fill;
                        panel1.Controls.Add(viewer);
                        panel1.ResumeLayout();
                    }
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        //Tombol Submit
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            if (graphLoaded)
            {
                using (StreamReader sr = new StreamReader(openFileDialog1.OpenFile()))
                {
                    string line = sr.ReadLine();
                    if (line == null || line == "0")
                    {
                        MessageBox.Show("File Kosong");
                    }
                    else
                    {
                        while (sr.Peek() >= 0)
                        {
                            //baca file buat ngebuat graf yang akan nanti di search
                            line = sr.ReadLine();
                            string[] cur_line = line.Split(' ');
                            virtualGraph.addEdges((cur_line[0]), (cur_line[1]));
                        }
                        //Cek apakah Choose Account dan Explore Friends with terisi
                        if (comboBox1.SelectedIndex > -1 && comboBox2.SelectedIndex > -1)
                        {
                            //Cek bila salah satu checkBox telah dicentang
                            if (checkBox2.Checked || checkBox1.Checked)
                            {
                                //DFS
                                if (checkBox1.Checked)
                                {
                                    virtualGraph.DFS(this.comboBox1.GetItemText(this.comboBox1.SelectedItem), this.comboBox2.GetItemText(this.comboBox2.SelectedItem));
                                }
                                //BFS
                                else
                                {
                                    virtualGraph.BFS(this.comboBox1.GetItemText(this.comboBox1.SelectedItem), this.comboBox2.GetItemText(this.comboBox2.SelectedItem));
                                }   
                            }
                            else
                            {
                                Console.WriteLine("Pilih Algoritma Pencarian");
                            }
                        }
                        //Cek bila hanya Choose Account yang terisi
                        else if (comboBox1.SelectedIndex > -1)
                        {
                            virtualGraph.FriendRecommendation(this.comboBox1.GetItemText(this.comboBox1.SelectedItem));
                        }
                        else
                        {
                            Console.WriteLine("Choose Account harus diisi");
                        }
                        
                    }
                }
            }
            else
            {
                Console.WriteLine("Belom ada graf yang di load");
            }
            
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        //BFS Checkbox
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = !checkBox1.Checked;
        }

        //DFS Checkbox
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = !checkBox2.Checked;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        public void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //Untuk overload fungsi WriteLine
        public class TextBoxWriter : TextWriter
        {
            private Control MyControl;
            public TextBoxWriter(Control control)
            {
                MyControl = control;
            }

            public override void Write(char value)
            {
                MyControl.Text += value;
            }

            public override void Write(string value)
            {
                MyControl.Text += value;
            }

            public override Encoding Encoding
            {
                get { return Encoding.Unicode; }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
