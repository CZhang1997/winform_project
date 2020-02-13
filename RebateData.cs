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
    public class RebateData : IEquatable<RebateData>
    {
        private string firstName;
        private char middleName;
        private string lastName;
        private string address1;
        private string address2;
        private string city;
        private string state;
        private string zipCode;
        private char gender;
        private string phoneNumber;
        private string emailAddress;
        private bool proofPurchase;
        private DateTime dateRecieve;
        private string firstCharEnterTime;
        private string saveButtonClickedTime;
        private int backspaceCount;
        
        // Constructor
        public RebateData(String f, string l, string p)
        {
            firstName = f;
            lastName = l;
            phoneNumber = p;
        }
            
        public RebateData(string f, char m, string l, string a1, string a2, 
            string c, string s, string z, char g, string p, string e, bool pro, 
            DateTime d, string firstTime, string saveTime, int count)
        {
            firstName = f;
            middleName = m;
            lastName = l;
            address1 = a1;
            address2 = a2;
            city = c;
            state = s;
            zipCode = z;
            gender = g;
            phoneNumber = p;
            emailAddress = e;
            proofPurchase = pro;
            dateRecieve = d;
            firstCharEnterTime = firstTime;
            saveButtonClickedTime = saveTime;
            backspaceCount = count;
        }
        
        public override string ToString()
        {   // to string function that print all data
            return $"{firstName},{middleName},{lastName},{address1},{address2},{city},{state},{zipCode},{gender}" +
                $",{phoneNumber},{emailAddress},{proofPurchase.ToString()},{dateRecieve.ToShortDateString()},{firstCharEnterTime},{saveButtonClickedTime},{backspaceCount}";
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            RebateData objAsRebateData = obj as RebateData;
            if (objAsRebateData == null) return false;
            else return Equals(objAsRebateData);
        }
        public override int GetHashCode()
        {
            return firstName.GetHashCode() 
                + lastName.GetHashCode() 
                + phoneNumber.GetHashCode();
        }

        // check if two Rebate data are equals
        public bool Equals(RebateData d)
        {
            return firstName.Equals(d.getFirstName())
                && lastName.Equals(d.getLastName())
                && phoneNumber.Equals(d.getPhoneNumber());
        }

        public void setFirstName(string i)
        {
            firstName = i;
        }
        public void setLastName(string i)
        {
            lastName = i;
        }
        public void setMiddleName(char i)
        {
            middleName = i;
        }
        public void setAddress1(string i)
        {
            address1 = i;
        }
        public void setAddress2(string i)
        {
            address2 = i;
        }
        public void setCity(string i)
        {
            city = i;
        }
        public void setState(string i)
        {
            state = i;
        }
        public void setZipCode(string i)
        {
            zipCode = i;
        }
        public void setGender(char i)
        {
            gender = i;
        }
        public void setPhoneNumber(string i)
        {
            phoneNumber = i;
        }
        public void setEmailAddress(string i)
        {
            emailAddress = i;
        }
        public void setAddress2(bool i)
        {
            proofPurchase = i;
        }
        public void setProofPurchased(bool b)
        {
            proofPurchase = b;
        }
        public void setDateRecieved(DateTime i)
        {
            dateRecieve = i;
        }

        public string getFirstName()
        {
            return firstName;
        }
        public string getLastName()
        {
            return lastName;
        }
        public char getMiddleName()
        {
            return middleName;
        }
        public string getAddressLine1()
        {
            return address1;
        }
        public string getAddressLine2()
        {
            return address2;
        }
        public string getCity()
        {
            return city;
        }
        public string getState()
        {
            return state;
        }
        public string getZipCode()
        {
            return zipCode;
        }
        public char getGender()
        {
            return gender;
        }
        public string getPhoneNumber()
        {
            return phoneNumber;
        }
        public string getEmailAddress()
        {
            return emailAddress;
        }
        public bool getProofPurchased()
        {
            return proofPurchase;
        }
        public DateTime getDateRecieve()
        {
            return dateRecieve;
        }



    }
}
