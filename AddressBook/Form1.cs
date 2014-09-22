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
        List<Contact> contacts = new List<Contact>();

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
                Contact c = new Contact();
                c.Name = xNode.SelectSingleNode("Name").InnerText;
                c.Email = xNode.SelectSingleNode("Email").InnerText;
                c.Phone = xNode.SelectSingleNode("Phone").InnerText;
                c.Address = xNode.SelectSingleNode("Address").InnerText;
             
                contacts.Add(c);
                listViewContacts.Items.Add(c.Name);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Contact c = new Contact();
            c.Name = textBoxName.Text;
            c.Email = textBoxEmail.Text;
            c.Phone = textBoxPhone.Text;
            c.Address = textBoxAddress.Text;
            c.Photo = pictureBoxPhoto.Image;

            contacts.Add(c);
            listViewContacts.Items.Add(c.Name);
            textBoxName.Text = "";
            textBoxEmail.Text = "";
            textBoxPhone.Text = "";
            textBoxAddress.Text = "";
            pictureBoxPhoto.Image = pictureBoxPhoto.Image;

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonUpdate.Enabled = listViewContacts.SelectedItems.Count > 0;
            try
            {
                if (listViewContacts.SelectedItems.Count > 0)
                textBoxName.Text = contacts[listViewContacts.SelectedItems[0].Index].Name;
                textBoxEmail.Text = contacts[listViewContacts.SelectedItems[0].Index].Email;
                textBoxPhone.Text = contacts[listViewContacts.SelectedItems[0].Index].Phone;
                textBoxAddress.Text = contacts[listViewContacts.SelectedItems[0].Index].Address;
                pictureBoxPhoto.Image = contacts[listViewContacts.SelectedItems[0].Index].Photo;
                listViewContacts.SelectedItems[0].Text = textBoxName.Text;
            }
            catch (Exception)
            { 

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem eachItem in listViewContacts.SelectedItems)
            {
                listViewContacts.Items.Remove(eachItem);
            }
        }

        void Remove()
        {
            try
            {
                listViewContacts.Items.Remove(listViewContacts.SelectedItems[0]);
                contacts.RemoveAt(listViewContacts.SelectedItems[0].Index);
            }
            catch (Exception)
            {
   
            }
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
                pictureBoxPhoto.ImageLocation = ofd.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewContacts.SelectedItems.Count > 0)
                contacts[listViewContacts.SelectedItems[0].Index].Name = textBoxName.Text;
                contacts[listViewContacts.SelectedItems[0].Index].Email = textBoxEmail.Text;
                contacts[listViewContacts.SelectedItems[0].Index].Phone = textBoxPhone.Text;
                contacts[listViewContacts.SelectedItems[0].Index].Address = textBoxAddress.Text;
                contacts[listViewContacts.SelectedItems[0].Index].Photo = pictureBoxPhoto.Image;
            }
            catch (Exception)
            {
              
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            XmlDocument xDoc = new XmlDocument();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            xDoc.Load(path + "\\Address Book\\store.xml");
            XmlNode xNode = xDoc.SelectSingleNode("Contacts");
            xNode.RemoveAll();
            foreach (Contact c in contacts)
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

        List<string> items = new List<string>();

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Form_Load1(object sender, EventArgs e)
        {
            items.AddRange(new string[] { });

            foreach (string str in items)
            {
                listViewContacts.Items.Add(str);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

    }
 }
    