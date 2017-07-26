using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace vt_kod4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=musteri.accdb");
        OleDbDataAdapter kurye;
        DataSet ds = new DataSet();
        BindingSource bs=new BindingSource();

        void baglan()
        {
            ds.Clear();
            string sorgu;
            sorgu = "select*from bilgiler";
            kurye = new OleDbDataAdapter(sorgu, bag);
            kurye.Fill(ds);
            bs.DataSource = ds.Tables[0];
            dataGridView1.DataSource = bs;
        }

        void textbagla()
        {
            textBox1.DataBindings.Add("text", bs, "musteri_no");
            textBox2.DataBindings.Add("text", bs, "adi_soyadi");
            textBox3.DataBindings.Add("text", bs, "telefon");
            textBox4.DataBindings.Add("text", bs, "yas");
            textBox5.DataBindings.Add("text", bs, "resim_yolu");
            comboBox1.DataBindings.Add("text", bs, "cinsiyet");
            comboBox2.DataBindings.Add("text", bs, "medeni_hal");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            baglan();
            label7.Text = bs.Position + " / " + bs.Count;
            button10.ForeColor = Color.Green;
            textbagla();
            button10.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bs.MoveFirst();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bs.MovePrevious();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bs.MoveNext();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bs.MoveLast();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            label7.Text = (bs.Position + 1).ToString() + " / " + bs.Count;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            label7.Text = (bs.Position+1).ToString() + " / " + bs.Count;
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = textBox5.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dosya;
            openFileDialog1.ShowDialog();
            dosya = openFileDialog1.FileName;
            textBox5.Text = dosya;
            pictureBox1.ImageLocation = dosya;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bs.AddNew();
            pictureBox1.Image = null;
            MessageBox.Show("Yeni Kayıt Eklendi.");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //kayit ekleme
            try
            {
                bag.Open();
                OleDbCommand kyt = new OleDbCommand("insert into bilgiler values(" + textBox1.Text + ",'" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox1.Text + "'," + textBox4.Text + ",'" + comboBox2.Text + "','" + textBox5.Text + "')", bag);
                kyt.ExecuteNonQuery();
                baglan();
                bag.Close();
                bs.MoveFirst();
                MessageBox.Show("Yeni Kayıt Eklendi");
            }
            catch (Exception)
            {
                
               
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //güncelle
            bag.Open();
            OleDbCommand gncll = new OleDbCommand("update bilgiler set adi_soyadi='" + textBox2.Text + "',telefon='" + textBox3.Text + "',cinsiyet='" + comboBox1.Text + "',yas=" + textBox4.Text + ",medeni_hal='" + comboBox2.Text + "',resim_yolu='" + textBox5.Text + "' where musteri_no="+textBox1.Text+"",bag);
            gncll.ExecuteNonQuery();
            baglan();
            bag.Close();
            MessageBox.Show("Güncellendi");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bag.Open();
            OleDbCommand sil = new OleDbCommand("delete from bilgiler where musteri_no="+textBox1.Text+"", bag);
            int a = sil.ExecuteNonQuery();
            baglan();
            bag.Close();
            MessageBox.Show(a+" Adet Silindi");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ds.Clear();
            string filtre;
            filtre = "select*from bilgiler where musteri_no="+textBox6.Text+"";
            kurye = new OleDbDataAdapter(filtre,bag);
            kurye.Fill(ds);
            dataGridView1.DataSource = bs;
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string sil;
            sil = "delete from bilgiler where musteri_no=" + textBox7.Text + "";
            kurye = new OleDbDataAdapter(sil, bag);
            kurye.Fill(ds);
            baglan();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
