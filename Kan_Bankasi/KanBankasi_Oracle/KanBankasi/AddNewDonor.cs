﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KanBankasi
{
    public partial class AddNewDonor : Form
    {
        DBFunctions islem = new DBFunctions();
        ErrorProvider error = new ErrorProvider();

        public AddNewDonor()
        {
            InitializeComponent();
        }
        
    

        private void AddNewDonor_Load(object sender, EventArgs e)
        {
            String sorgu = "SELECT MAX(donorNo) FROM Donorler";
            DataSet ds = islem.veriyiAl(sorgu);
            Console.WriteLine("İşte Buradaaaa" + ds.Tables[0].Rows[0][0].GetType());
            int id = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            txtDonorNo.Text = (id + 1).ToString();
            txtDonorNo.ReadOnly = true;
            txtDonorNo.TabStop = false;
            txtTcNo.MaxLength = 11;
            txtCepNo.MaxLength = 10;

               
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtAdres.Text == "" || txtTcNo.Text.Length != 11 || txtCepNo.Text == "" || txtCepNo.Text.Length != 10 || txtDogumTarih.Text == "" || txtDonorAd.Text =="" || 
                txtDonorNo.Text == "" || txtDonorSoyad.Text == "" || txtIlce.Text == "" || txtPosta.Text == "" || 
                txtSehir.Text == "" || txtTcNo.Text == "" || comboCinsiyet.Text == "" || comboKanGrubu.Text == "")
            {
                MessageBox.Show("Bütün Alanlar Doldurulmalıdır!!!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {          
                String sorgu = "insert into Donorler (donorNo, tcNo, ad, soyad, dogumTarihi, cinsiyet, cepNo, kanGrubu, ePosta, sehir, ilce, adres) values('" + txtDonorNo.Text + "', '" + txtTcNo.Text + "'," +
                    " '" + txtDonorAd.Text + "', '" + txtDonorSoyad.Text + "', '" + txtDogumTarih.Text + "', '" + comboCinsiyet.Text + "', '" + txtCepNo.Text + "', '" + comboKanGrubu.Text + "'," +
                    " '" + txtPosta.Text + "', '" + txtSehir.Text + "', '" + txtIlce.Text + "', '" + txtAdres.Text + "')  ";
                
                Boolean control = islem.veriAyarla(sorgu);

                if (control)
                {
                    MessageBox.Show("İşlem Başarıyla Gerçekleşti.", "Veri Kaydı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAdres.Clear();
                    txtCepNo.Clear();
                    txtDogumTarih.ResetText();
                    txtDonorAd.Clear();
                    txtDonorNo.Clear();
                    txtDonorSoyad.Clear();
                    txtIlce.Clear();
                    txtPosta.Clear();
                    txtSehir.Clear();
                    txtTcNo.Clear();
                    comboCinsiyet.SelectedItem = null;
                    comboKanGrubu.SelectedItem = null;
                    
                } 
                
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTcNo_TextChanged(object sender, EventArgs e)
        {
            if (txtTcNo.TextLength < 11 && txtTcNo.TextLength > 0)
            {
                error.SetError(txtTcNo, "TC Kimlik Numarası 11 haneli olmalı!!");
            }
            else
                error.Clear();
        }
        private void txtCepNo_TextChanged(object sender, EventArgs e)
        {
            if (txtCepNo.TextLength < 10 && txtCepNo.TextLength > 0)
            {
                error.SetError(txtCepNo, "Başında 0 olmadan giriniz.");
            }
            else
                error.Clear();
        }


        private void sadeceHarf(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void sadeceRakam(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8)
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
