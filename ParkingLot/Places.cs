using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkingLot
{
    public partial class Places : Form
    {
        int key = 0;
        dbContext conn;
        public Places()
        {
            InitializeComponent();
            conn = new dbContext(); 
            ShowPlaces();
        }
        private void ShowPlaces()
        {
            string query = "SELECT * FROM PlaceTBL";
            dgvPlaces.DataSource = conn.ExecuteQuery(query);
        }
        private void btnCars_Click(object sender, EventArgs e)
        {
            Cars car = new Cars();
            car.Show();
            this.Hide();
        }

        private void btnParking_Click(object sender, EventArgs e)
        {
            Parking park = new Parking();
            park.Show();
            this.Hide();
        }       
        private void btnLogout_Click(object sender, EventArgs e)
        {          
            Login login = new Login();
            login.Show();
            this.Hide();           
        }

        private void btnClose_Click(object sender, EventArgs e)=>Close();

        private void dgvPlaces_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPlaces.SelectedRows.Count > 0 && e.RowIndex >= 0 && e.RowIndex < dgvPlaces.Rows.Count)
            {                
                txtPosition.Text = dgvPlaces.SelectedRows[0].Cells[1].Value.ToString();
                txtStatus.Text = dgvPlaces.SelectedRows[0].Cells[2].Value.ToString();
                key = Convert.ToInt32(dgvPlaces.SelectedRows[0].Cells["PlNum"].Value);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtPosition.Text == "" || txtStatus.SelectedIndex == -1)
            {
                MessageBox.Show("No Data!!!");
            }
            else
            {
                try
                {                   
                    string pos = txtPosition.Text;
                    string stat = txtStatus.SelectedItem.ToString();
                    string query = $"INSERT INTO PlaceTBL (Pposition, PStatus) VALUES ('{pos}', '{stat}')";
                    conn.ExecuteNonQuery(query);
                    MessageBox.Show("Add success!!!");
                    ShowPlaces();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtPosition.Text == "" || txtStatus.SelectedIndex == -1)
            {
                MessageBox.Show("No Data!!!");
            }
            else
            {
                try
                {
                    string pos = txtPosition.Text;
                    string stat = txtStatus.SelectedItem.ToString();
                    string query = $"UPDATE PlaceTBL SET Pposition='{pos}', PStatus='{stat}' WHERE PlNum={key}";
                    conn.ExecuteNonQuery(query);
                    MessageBox.Show("Update success!!!");
                    ShowPlaces();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Please select a Place!!!");
            }
            else
            {
                try
                {
                    string deleteQuery = $"DELETE FROM PlaceTBL WHERE PlNum={key}";
                    conn.ExecuteNonQuery(deleteQuery);
                    MessageBox.Show("Place removed successfully.");
                    ShowPlaces();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
