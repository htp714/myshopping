using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class KhachHangDTO
    {
        string _ID;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        string _BirthDay;

        public string BirthDay
        {
            get { return _BirthDay; }
            set { _BirthDay = value; }
        }
        string _Sex;

        public string Sex
        {
            get { return _Sex; }
            set { _Sex = value; }
        }
        string _Email;

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        string _Phone;

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        string _Address;

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        string _Type;
        

        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private string _Status;

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
    }
}
