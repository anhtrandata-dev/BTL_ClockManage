﻿using BTL_ClockManage.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_ClockManage.Views
{
    public partial class viewAccountManage : Form
    {
        public viewAccountManage()
        {
            InitializeComponent();
        }
        private void viewAccountManage_Load(object sender, EventArgs e)
        {
            List<NHANVIEN> idnv = Program.context.NHANVIENs.ToList();
            idnv.ForEach(x =>
            {
                cbId.Items.Add(x.MANV);
            });
            initTable();
        }
        public void initTable()
        {
            //tbl.DataSource = null;
            List<NHANVIEN> nv = null;
            if (cbPosition.SelectedIndex != -1)
            {
                nv = Program.context.NHANVIENs.Where(n => n.PASSWORDS != null && n.CHUCVU == cbPosition.SelectedItem.ToString()).OrderBy(n => n.TENNV).ToList();
            }else nv = Program.context.NHANVIENs.Where(n => n.PASSWORDS != null).OrderBy(n => n.TENNV).ToList();
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("Tên", typeof(string));
            table.Columns.Add("Chức vụ", typeof(string));
            table.Columns.Add("Password", typeof(string));
            nv.ForEach(x =>
            {
                var rowDT = table.NewRow();
                rowDT["Id"] = x.MANV;
                rowDT["Tên"] = x.TENNV;
                rowDT["Chức vụ"] = x.CHUCVU;
                rowDT["Password"] = x.PASSWORDS;
                table.Rows.Add(rowDT);
            });
            tbl.DataSource = table;
        }
        private void tbl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cbId.SelectedItem = tbl.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = tbl.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPosition.Text = tbl.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtAccount.Text = tbl.Rows[e.RowIndex].Cells[3].Value.ToString();
            btnAdd.Hide();
        }

        private void cbId_SelectedIndexChanged(object sender, EventArgs e)
        {
            NHANVIEN nv = Program.context.NHANVIENs.FirstOrDefault(n=>n.MANV == cbId.SelectedItem.ToString());
            txtName.Text = nv.TENNV;
            txtPosition.Text = nv.CHUCVU;
            txtAccount.Text = nv.PASSWORDS;
            if (txtAccount.Text == "")
            {
                btnAdd.Show();
            }
            else btnAdd.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            NHANVIEN nv = Program.context.NHANVIENs.FirstOrDefault(n => n.MANV == cbId.SelectedItem.ToString());
            nv.PASSWORDS = txtAccount.Text;
            Program.context.SaveChanges();
            MessageBox.Show("Thêm thành công");
            initTable();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            NHANVIEN nv = Program.context.NHANVIENs.FirstOrDefault(n => n.MANV == cbId.SelectedItem.ToString());
            nv.PASSWORDS = null;
            Program.context.SaveChanges();
            MessageBox.Show("Xóa thành công");
            initTable();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            NHANVIEN nv = Program.context.NHANVIENs.FirstOrDefault(n => n.MANV == cbId.SelectedItem.ToString());
            nv.PASSWORDS = txtAccount.Text;
            Program.context.SaveChanges();
            MessageBox.Show("Sửa thành công");
            initTable();
        }

        private void saveQR_Click(object sender, EventArgs e)
        {
            if (picQR.Image == null)
                return;
            using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "*PNG|*.png" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    picQR.Image.Save(saveFileDialog.FileName);
            }
        }

        private void cbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            initTable();
        }

        private void txtAccount_TextChanged(object sender, EventArgs e)
        {
            if(txtAccount.Text == "")
            {
                picQR.Image = Properties.Resources.Oggy_modified;
            }
            else
            {
                QRCodeGenerator qr = new QRCodeGenerator();
                QRCodeData data = qr.CreateQrCode(txtAccount.Text, QRCodeGenerator.ECCLevel.H);
                QRCode code = new QRCode(data);
                picQR.Image = code.GetGraphic(5);
            }
        }
    }
}
