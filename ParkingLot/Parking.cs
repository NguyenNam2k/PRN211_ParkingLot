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
    public partial class Parking : Form
    {
        dbContext conn;
        public Parking()
        {
            InitializeComponent();
            conn = new dbContext();
            GetCars();
            GetPlaces();
            ShowBook();
            btnCancel.Click += btnCancel_Click;
        }
        private void GetCars()
        {
            string query = "SELECT CNum, PNumber FROM CarsTBL";
            DataTable carsTable = conn.ExecuteQuery(query);
            txtCars.DataSource = carsTable;
            txtCars.ValueMember = "CNum";
            txtCars.DisplayMember = "PNumber";
        }

        private void GetPlaces()
        {
            string query = "SELECT PlNum, Pposition FROM PlaceTBL WHERE PStatus='Free'";
            DataTable placesTable = conn.ExecuteQuery(query);
            txtPlace.DataSource = placesTable;
            txtPlace.ValueMember = "PlNum";
            txtPlace.DisplayMember = "Pposition";
        }

        private void ShowBook()
        {
            string query = "SELECT * FROM ParkingTBL";
            dgvParking.DataSource = conn.ExecuteQuery(query);
        }
        private void UpdatedStatus(string placeId)
        {
            string query = $"UPDATE PlaceTBL SET PStatus='Booked' WHERE PlNum={placeId}";
            conn.ExecuteNonQuery(query);
            MessageBox.Show("Update success!!!");
        }
        private void btnBook_Click(object sender, EventArgs e)
        {
            if (txtCars.SelectedIndex == -1 || txtPlace.SelectedIndex == -1 || string.IsNullOrEmpty(txtAmount.Text) || string.IsNullOrEmpty(txtDuration.Text))
            {
                MessageBox.Show("No data!!!");
            }
            else
            {
                try
                {
                    string carId = txtCars.SelectedValue.ToString();
                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                    string duration = txtDuration.Text;
                    int amount = Convert.ToInt32(txtAmount.Text);
                    string placeId = txtPlace.SelectedValue.ToString();
                    string query = $"INSERT INTO ParkingTBL (Car, Date, Duration, Amount, Place) VALUES ({carId}, '{date}', {duration}, {amount}, {placeId})";
                    conn.ExecuteNonQuery(query);

                    MessageBox.Show("Book Success!!!");

                    ShowBook();
                    UpdatedStatus(placeId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtAmount.Text = "";
            txtDuration.Text = "";
            txtCars.SelectedIndex = -1;
            txtPlace.SelectedIndex = -1;
            txtDate.Value = DateTime.Now;
        }

        private void btnPlaces_Click(object sender, EventArgs e)
        {
            Places place = new Places();
            place.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void btnCars_Click(object sender, EventArgs e)
        {
            Cars car= new Cars();
            car.Show();
            this.Hide();
        }

        private void btnParking_Click(object sender, EventArgs e)
        {
            Parking park=new Parking();
            park.Show();
            this.Hide();
        }
    }
}
