/**
 * @Author: Churong Zhang
 * @Date: 2/12/2020
 * @Class: CS 6326.001 - Human Computer Interactions - S20
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asg2_cxz173430
{
    public partial class RebateForm : Form
    {
        private List<RebateData> datas;
        //  the filename that is going to used to store the rebate form
        private const string filename = "CS6326Asg2.txt";
        private FileIO DataIO; 
        private string first;
        private char middle;
        private string last;
        private string address1;
        private string address2;
        private string city;
        private string state;
        private string zipcode;
        private char gender;
        private string phone;
        private string email;
        private bool proof;
        private DateTime dateRecieve;
        private string firstCharTime;
        private string saveTime;
        private int backspaceCount;
        private int selectedIndex = -1;

        private const string notes = "Noted: Field with * must enter Vaild Information";

        public RebateForm()
        {
            InitializeComponent();
        }
        private void From1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // count the number of back-space is beening press
            if(e.KeyChar == (char)8)
            {
                backspaceCount++;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // create the FileIO object with a filename that used to save and load data from
            DataIO = new FileIO(filename);
            // load the data from the file with filename
            datas = DataIO.LoadData();
            dateTimePicker1.MaxDate = DateTime.Today;
            tipsLabel.Font = new Font("Microsoft Sans Serif", 10);
            tipsLabel.Text = notes;
            // loop throught the Collection of rebate Data
            foreach (RebateData s in datas)
            {   // add the data into the listview
                addToNameList(s);
            }
            modifyButton.Enabled = false;
            deleteButton.Enabled = false;
            // reset everything for new form to use
            cleanUILabel();
        }

        private void addressText1_TextChanged(object sender, EventArgs e)
        {

        }

        private void firstNameText_TextChanged(object sender, EventArgs e)
        {
            // record the time of the first char was enter
            if(firstNameText.Text.Length == 1)
            {   // renew this variable as long as the length
                firstCharTime = DateTime.Now.ToString("HH:mm:ss");
            }
        }

        private void addToNameList(RebateData s)
        {
            // design the listviewItem
            ListViewItem item = new ListViewItem(s.getFirstName()); // first name
            item.SubItems.Add(s.getLastName()); // add last name into column
            item.SubItems.Add(s.getPhoneNumber());  // add phone number into column
            nameList.Items.Add(item);   // add the item into listview
        }
        private void setLabels(RebateData d)
        {
            // set all UI labels to the correct data base on d
            firstNameText.Text = d.getFirstName();
            if (d.getMiddleName().Equals(' '))
                middleNameText.Text ="";
            else
                middleNameText.Text = $"{d.getMiddleName()}";
            lastNameText.Text = d.getLastName();
            addressText1.Text = d.getAddressLine1();
            addressText2.Text = d.getAddressLine2();
            cityText.Text = d.getCity();
            stateText.Text = d.getState();
            zipCodeText.Text = d.getZipCode();
            genderText.Text = $"{d.getGender()}"; ;
            phoneNumber.Text = d.getPhoneNumber();
            emailAddressText.Text = d.getEmailAddress();
            proofChecker.Checked = d.getProofPurchased();
            dateTimePicker1.Value = d.getDateRecieve();
        }
        private bool getDataFromUI()
        {
            // get the data from the text box
            // check if all data are valid
            first = firstNameText.Text;
            if(first.Length == 0)
            {   
                tipsLabel.Text = "First Name cannot be empty";
                return false;
            }
            if (middleNameText.Text.Length > 0)
            {
                middle = middleNameText.Text[0];
            }
            else
                middle = ' ';
            last = lastNameText.Text;
            if (last.Length == 0)
            {
                tipsLabel.Text = "Last Name cannot be empty";
                return false;
            }
            address1 = addressText1.Text;
            if (address1.Length == 0)
            {
                tipsLabel.Text = "Address 1 cannot be empty";
                return false;
            }
            address2 = addressText2.Text;
            city = cityText.Text;
            if (city.Length == 0)
            {
                tipsLabel.Text = "City cannot be empty";
                return false;
            }
            state = stateText.Text;
            if (state.Length == 0)
            {
                tipsLabel.Text = "State cannot be empty";
                return false;
            }
            zipcode = zipCodeText.Text;
            if (zipcode.Length == 0)
            {
                tipsLabel.Text = "Zipcode cannot be empty";
                return false;
            }
            if (genderText.Text.Length == 0)
            {
                tipsLabel.ForeColor = Color.Red;
                tipsLabel.Text = "Gender can not be empty, M - male, F - female";
                return false;
            }
            else
            {
                gender = genderText.Text[0];
                if (gender != 'M' && gender != 'F')
                {
                    tipsLabel.Text = "Please enter M for Male, F for Female";
                    return false;
                }
            }
            phone = phoneNumber.Text;
            if (phone.Length == 0)
            {
                tipsLabel.Text = "Phone Number cannot be empty";
                return false;
            }
            email = emailAddressText.Text;
            if (email.Length == 0)
            {
                tipsLabel.Text = "email cannot be empty";
                return false;
            }
            if(email.IndexOf('@') == -1)
            {
                tipsLabel.Text = "Invalid Email formate";
                return false;
            }
            proof = proofChecker.Checked;
            dateRecieve = dateTimePicker1.Value;
            return true;
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            // get the data from ui and check if all data are valid
            bool validData = getDataFromUI();
            if (validData)
            {
                tipsLabel.Text = notes;
                tipsLabel.ForeColor = Color.Black;
                // get the time when save was pressed
                saveTime = DateTime.Now.ToString("HH:mm:ss");
                //
                RebateData data = new RebateData(first, middle, last, address1, address2, city, state, zipcode, gender, phone, email, proof, dateRecieve, firstCharTime, saveTime, backspaceCount);
                bool exist = checkExist(data);
                // if not exist, then add it to the list
                if (!exist)
                {
                    datas.Add(data);    // add to data list
                    addToNameList(data);    // add to list view
                    DataIO.saveData(datas); // save the data to file
                    tipsLabel.ForeColor = Color.Black;
                    cleanUILabel();
                    tipsLabel.Text = "Record has been saved";
                }
                else
                {   // if already exist, then tell the user it is already exist through the tips label 
                    tipsLabel.ForeColor = Color.Red;
                    tipsLabel.Text = "This record is already exist.";
                }
            }
            else
                tipsLabel.ForeColor = Color.Red;
        }
     
        private void cleanUILabel()
        {
            // set all UI text box to empty
            firstNameText.Text = "";
            middleNameText.Text = "";
            lastNameText.Text = "";
            addressText1.Text = "";
            addressText2.Text = "";
            cityText.Text = "";
            stateText.Text = "";
            zipCodeText.Text = "";
            genderText.Text = "";
            phoneNumber.Text = "";
            emailAddressText.Text = "";
            proofChecker.Checked = true;
            dateTimePicker1.Value = DateTime.Today;
            firstCharTime = "";
            saveTime = "";
            backspaceCount = 0;
            tipsLabel.ForeColor = Color.Black;
            tipsLabel.Text = notes;
            saveButton.Enabled = true;
            modifyButton.Enabled = false;
            deleteButton.Enabled = false;
        }

        private void nameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // check if a row is selected in the list view
            if (nameList.SelectedItems.Count > 0)
            {
                // find the index of that item
                selectedIndex = nameList.Items.IndexOf(nameList.SelectedItems[0]);
                // change all UI Text box base on the selected data
                setLabels(datas[selectedIndex]);
                // make only modify and delete button enable
                saveButton.Enabled = false;
                modifyButton.Enabled = true;
                deleteButton.Enabled = true;
            }
            else
            {
                saveButton.Enabled = true;
                cleanUILabel();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            // check if an index has been selected from the list view
            if(selectedIndex !=  -1)
            {
                datas.Remove(datas[selectedIndex]); // remove the record from data list
                nameList.Items.Remove(nameList.Items[selectedIndex]);   // remove item from list view
                DataIO.saveData(datas); // save to file
                tipsLabel.ForeColor = Color.Black;
                tipsLabel.Text = "Record Deleted";
                selectedIndex = -1; // set the selected index to -1
            }
            else
            {   // no item was selected, tell the user select a record
                tipsLabel.ForeColor = Color.Red;
                tipsLabel.Text = "Please select an item first";
            }
        }

        private void ModifyButton_Click(object sender, EventArgs e)
        {
            // check if a record is been selected
            if(selectedIndex != -1)
            {   // find the selected record
                RebateData rb = datas[selectedIndex];
                bool valid = getDataFromUI(); // get the data and check if is valid
                if(!valid)
                {   // if is not valid data, then cancel the modify action
                    tipsLabel.ForeColor = Color.Red;
                    tipsLabel.Text = tipsLabel.Text + $" Modify failed";
                }
                else
                {   // set all fields except the last 3 field and save the data
                    RebateData d = new RebateData(first, last, phone);
                    if(checkExist(d))
                    {
                        tipsLabel.ForeColor = Color.Red;
                        tipsLabel.Text = "Modify failed, Record already exist";
                    }
                    else
                    {
                        rb.setFirstName(first);
                        rb.setMiddleName(middle);
                        rb.setLastName(last);
                        rb.setAddress1(address1);
                        rb.setAddress2(address2);
                        rb.setCity(city);
                        rb.setState(state);
                        rb.setZipCode(zipcode);
                        rb.setGender(gender);
                        rb.setPhoneNumber(phone);
                        rb.setEmailAddress(email);
                        rb.setProofPurchased(proof);
                        ListViewItem item = nameList.Items[selectedIndex];
                        item.SubItems[0].Text = first;
                        item.SubItems[1].Text = last;
                        item.SubItems[2].Text = phone;
                        DataIO.saveData(datas);
                        tipsLabel.ForeColor = Color.Black;
                        tipsLabel.Text = "Modify Completed";
                    }
                }
            }
            else
            {
                tipsLabel.ForeColor = Color.Red;
                tipsLabel.Text = "Please select an item first";
            }
        }
        private bool checkExist(RebateData data)
        {
            foreach (RebateData d in datas)
            {// check if this new record is already exist in the collection
                if (d.Equals(data))
                    return true;
            }
            return false;
        }
        private void zipCodeText_KeyPress(object sender, KeyPressEventArgs e)
        {   // only allow digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void phoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {   // only allow digits and '-'
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }
        private void phoneNumber_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void resetButton_Click(object sender, EventArgs e)
        {   // reset all fields to be empty
            cleanUILabel();
        }
        private void cityText_KeyPress(object sender, KeyPressEventArgs e)
        {   // only allow letters
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }
    }
}
