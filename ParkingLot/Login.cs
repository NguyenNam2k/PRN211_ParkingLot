using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkingLot
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (username == "" || password == "")
            {
                MessageBox.Show("Input must not be empty");
            }
            else
            {
                if (username == "admin@Fparking.com" && password == "admin@@")
                {
                    Cars car = new Cars();
                    car.Show();
                    this.Hide();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)=>Close();
    }
}
