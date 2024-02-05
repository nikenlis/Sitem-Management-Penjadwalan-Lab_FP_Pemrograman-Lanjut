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

namespace LaporLab.View
{
    public partial class FormEditLapor : Form
    {
        // deklarasi tipe data untuk event OnCreate dan OnUpdate
        public delegate void UpdateEventHandler(BarangRusak brgRusak);

        // deklarasi event ketika terjadi proses update data
        public event UpdateEventHandler OnUpdate;

        // deklarasi objek controller
        private BarangRusakController controller;

        // deklarasi field untuk meyimpan objek barang rusak
        private BarangRusak brgRusak;



        public FormEditLapor()
        {
            InitializeComponent();
        }


        public FormEditLapor(string title, BarangRusak obj, BarangRusakController controller) : this()
        {
            // ganti text/judul form
            this.Text = title;
            this.controller = controller;

            
            brgRusak = obj; 

            bunifutxtNoRuangan.Text = brgRusak.no_ruangan;
            bunifutxtJenis.Text = brgRusak.jenis;
            bunifutxtNoKomputer.Text = brgRusak.no_komputer;
            bunifutxtKeterangan.Text = brgRusak.keterangan;


        }

        private void btnBunifuEdit_Click(object sender, EventArgs e)
        {

            brgRusak.no_ruangan  = bunifutxtNoRuangan.Text;
            brgRusak.jenis = bunifutxtJenis.Text;
            brgRusak.no_komputer = bunifutxtNoKomputer.Text;
            brgRusak.keterangan = bunifutxtKeterangan.Text;

            if (checkBoxSedangDiperbaiki.Checked)
            {
                
                brgRusak.status = "Sedang Diperbaiki";
            }
            else if (checkBoxSelesaiDiperbaiki.Checked)
            {
                
                brgRusak.status = "Selesai Diperbaiki";
            }



            brgRusak.petugas = UserAuthenticationInfo.LoggedInUserId;

            int result = 0;

            result = controller.Update(brgRusak);

            if (result > 0)
            {
                OnUpdate(brgRusak); // panggil event OnUpdate
                this.Close();
            }
        }

        private void FormEditLapor_Load(object sender, EventArgs e)
        {
            if (brgRusak.status == "Sedang Diperbaiki")
            {
                checkBoxSedangDiperbaiki.Checked = true;
            }
            else if (brgRusak.status == "Selesai Diperbaiki")
            {
                checkBoxSelesaiDiperbaiki.Checked = true;
            }
        }
    }
}
