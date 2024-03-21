using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkingLot
{
    public partial class Cars : Form
    {
        private int key = 0;
        private dbContext con;
        public Cars()
        {
            InitializeComponent();
            con = new dbContext();
            ShowCars();
        }
        private void ShowCars()
        {
            string query = "SELECT * FROM CarsTBL";
            dgvCars.DataSource = con.ExecuteQuery(query);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtCarID.Text=="" ||txtPlate.Text == "" || txtDriver.Text == "" || txtCartype.Text == "" || txtColor.Text == "")
            {
                MessageBox.Show("No Data!!!");
            }
            else
            {
                try
                {
                    int ID= int.Parse(txtCarID.Text);
                    string PNumber = txtPlate.Text;
                    string Driver = txtDriver.Text;
                    string Gen = cboGender.SelectedItem.ToString();
                    string CType = txtCartype.Text;
                    string Color = txtColor.Text;
                    string query = $"INSERT INTO CarsTBL (PNumber, Driver, Gender, CarType, CarColor) VALUES('{PNumber}','{Driver}','{Gen}','{CType}','{Color}')";
                    con.ExecuteNonQuery(query);
                    MessageBox.Show("Add success!!!");
                    ShowCars();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtCarID.Text == "" || txtPlate.Text == "" || txtDriver.Text == "" || txtCartype.Text == "" || txtColor.Text == "")
            {
                MessageBox.Show("No Data!!!");
            }
            else
            {
                try
                {
                    int ID = int.Parse(txtCarID.Text);
                    string PNumber = txtPlate.Text;
                    string Driver = txtDriver.Text;
                    string Gen = cboGender.SelectedItem.ToString();
                    string CType = txtCartype.Text;
                    string Color = txtColor.Text;
                    string query = $"UPDATE CarsTBL SET PNumber='{PNumber}', Driver='{Driver}', Gender='{Gen}', CarType='{CType}', CarColor='{Color}' WHERE CNum={ID}";
                    con.ExecuteNonQuery(query);
                    MessageBox.Show("Update success!!!");
                    ShowCars();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)=>Close();

        private void dgvCars_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCars.SelectedRows.Count > 0 && e.RowIndex >= 0 && e.RowIndex < dgvCars.Rows.Count)
            {
                txtCarID.Text = dgvCars.SelectedRows[0].Cells[0].Value.ToString();
                txtPlate.Text = dgvCars.SelectedRows[0].Cells[1].Value.ToString();
                txtDriver.Text = dgvCars.SelectedRows[0].Cells[2].Value.ToString();
                cboGender.SelectedItem = dgvCars.SelectedRows[0].Cells[3].Value.ToString();
                txtCartype.Text = dgvCars.SelectedRows[0].Cells[4].Value.ToString();
                txtColor.Text = dgvCars.SelectedRows[0].Cells[5].Value.ToString();
                key = Convert.ToInt32(txtCarID.Text);
            }
        }

        private void btnParking_Click(object sender, EventArgs e)
        {
            Parking park = new Parking();
            park.Show();
            this.Hide();
        }

        private void btnPlaces_Click(object sender, EventArgs e)
        {
            Places place=new Places();
            place.Show();
            this.Hide();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Please select a Car!!!");
            }
            else
            {
                try
                {
                    string deleteQuery = $"DELETE FROM CarsTBL WHERE CNum={key}";
                    string resetIdentityQuery = "DBCC CHECKIDENT ('CarsTBL', RESEED, 0)";
                    con.ExecuteNonQuery(deleteQuery);
                    con.ExecuteNonQuery(resetIdentityQuery);
                    MessageBox.Show("Car removed successfully.");
                    ShowCars();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }        
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void btnCars_Click(object sender, EventArgs e)
        {
            Cars cars = new Cars();
            cars.Show();
            this.Hide();
        }
    }
}
