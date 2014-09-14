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
using System.Xml;

namespace AddressBook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Contacts> contacts = new List<Contacts>();
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!Directory.Exists(path + "\\Address Book"))
                Directory.CreateDirectory(path + "\\Address Book");
            if (!File.Exists(path + "\\Address Book\\store.xml"))
            {
                XmlTextWriter xw = new XmlTextWriter(path + "\\Address Book\\store.xml", Encoding.UTF8);
                xw.WriteStartElement("Contacts");
                xw.WriteEndElement();
                xw.Close();
            }
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path + "\\Address Book\\store.xml");
            foreach (XmlNode xNode in xDoc.SelectNodes("Contacts/Contacts"))
            {
                Contacts c = new Contacts();
                c.Name = xNode.SelectSingleNode("Name").InnerText;
                c.Email = xNode.SelectSingleNode("Email").InnerText;
                c.Phone = xNode.SelectSingleNode("Phone").InnerText;
                c.Address = xNode.SelectSingleNode("Address").InnerText;
             

                contacts.Add(c);
                listView1.Items.Add(c.Name);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Contacts c = new Contacts();
            c.Name = textBox1.Text;
            c.Email = textBox2.Text;
            c.Phone = textBox3.Text;
            c.Address = textBox4.Text;
            c.Photo = pictureBox1.Image;


            contacts.Add(c);
            listView1.Items.Add(c.Name);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            pictureBox1.Image = pictureBox1.Image;

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = contacts[listView1.SelectedItems[0].Index].Name;
            textBox2.Text = contacts[listView1.SelectedItems[0].Index].Email;
            textBox3.Text = contacts[listView1.SelectedItems[0].Index].Phone;
            textBox4.Text = contacts[listView1.SelectedItems[0].Index].Address;
            pictureBox1.Image = contacts[listView1.SelectedItems[0].Index].Photo;
            listView1.SelectedItems[0].Text = textBox1.Text;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        void Remove()
        {
            try
            {
                listView1.Items.Remove(listView1.SelectedItems[0]);
                contacts.RemoveAt(listView1.SelectedItems[0].Index);
            }
            catch { }


        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Remove();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox1.ImageLocation = ofd.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            contacts[listView1.SelectedItems[0].Index].Name = textBox1.Text;
            contacts[listView1.SelectedItems[0].Index].Email = textBox2.Text;
            contacts[listView1.SelectedItems[0].Index].Phone = textBox3.Text;
            contacts[listView1.SelectedItems[0].Index].Address = textBox4.Text;
            contacts[listView1.SelectedItems[0].Index].Photo = pictureBox1.Image;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            XmlDocument xDoc = new XmlDocument();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            xDoc.Load(path + "\\Address Book\\store.xml");
            XmlNode xNode = xDoc.SelectSingleNode("Contacts");
            xNode.RemoveAll();
            foreach (Contacts c in contacts)
            {
                XmlNode xTop = xDoc.CreateElement("Contacts");
                XmlNode xName = xDoc.CreateElement("Name");
                XmlNode xEmail = xDoc.CreateElement("Email");
                XmlNode xPhone = xDoc.CreateElement("Phone");
                XmlNode xAddress = xDoc.CreateElement("Address");
                XmlNode xPhoto = xDoc.CreateElement("Photo");
                xName.InnerText = c.Name;
                xEmail.InnerText = c.Email;
                xPhone.InnerText = c.Phone;
                xAddress.InnerText = c.Address;
                

                xTop.AppendChild(xName);
                xTop.AppendChild(xEmail);
                xTop.AppendChild(xPhone);
                xTop.AppendChild(xAddress);
                xTop.AppendChild(xPhoto);
                xDoc.DocumentElement.AppendChild(xTop);
            }
            xDoc.Save(path + "\\Address Book\\store.xml");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
       
        }

        List<string> items = new List<string>();

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

            listView1.Items.Clear();

            foreach (string str in items)
            {
                if (str.StartsWith(textBox1.Text, StringComparison.CurrentCultureIgnoreCase))
                {
                    listView1.Items.Add(str);
                }
            }
        }

        private void Form_Load1(object sender, EventArgs e)
        {
            items.AddRange(new string[] { });

            foreach (string str in items)
            {
                listView1.Items.Add(str);
            }
        }

    }
    }
    class Contacts
    {
        public string Name
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string Phone
        {
            get;
            set;
        }
        public string Address
        {
            get;
            set;
        }
        public Image Photo
        {
            get;
            set;
        }
    }


