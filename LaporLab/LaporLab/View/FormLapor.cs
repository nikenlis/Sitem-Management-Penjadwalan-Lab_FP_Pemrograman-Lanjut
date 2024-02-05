using LaporLab.Controller;
using LaporLab.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LaporLab.View
{
    public partial class FormLapor : Form
    {

        // deklarasi tipe data untuk event OnCreate dan OnUpdate
        public delegate void CreateEventHandler(BarangRusak brgRusak);

        // deklarasi event ketika terjadi proses input data baru
        public event CreateEventHandler OnCreate;

        private BarangRusakController controller;

        private BarangRusak brgRusak;

        public FormLapor()
        {
            InitializeComponent();
        }

        //constructor untuk inisialisasi data ketika entri data baru
        public FormLapor(string title, BarangRusakController controller)
            : this()
        {
            // ganti text/judul form
            this.Text = title;
            this.controller = controller;
        }


        private void bunifubtnSimpan_Click(object sender, EventArgs e)
        {
            // jika data baru, inisialisasi objek barang rusak
            brgRusak = new BarangRusak();


            if (dpdNamaLab.SelectedItem != null)
            {
                brgRusak.no_ruangan  = dpdNamaLab.SelectedItem.ToString();

            }
            brgRusak.jenis = bunifutxtJenis.Text;
            brgRusak.no_komputer = bunifutxtNoKomputer.Text;
            brgRusak.keterangan = bunifutxtKeterangan.Text;
            brgRusak.status = "Belum Diperbaiki";
            brgRusak.petugas = UserAuthenticationInfo.LoggedInUserId;
            DateTime currentTime = DateTime.Today;
            brgRusak.tanggal_pinjam = currentTime.ToShortDateString();

            int result = 0;

            // panggil operasi CRUD
            result = controller.Create(brgRusak);

                if (result > 0) // tambah data berhasil
                {
                    OnCreate(brgRusak); // panggil event OnCreate

                    // reset form input, utk persiapan input data berikutnya
                    dpdNamaLab.ResetText();
                    bunifutxtJenis.Clear();
                    bunifutxtNoKomputer.Clear();
                    bunifutxtKeterangan.Clear();

                    bunifutxtJenis.Focus();
                }
            
        }

    }
}
