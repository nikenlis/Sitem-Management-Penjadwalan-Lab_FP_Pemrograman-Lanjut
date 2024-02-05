using LaporLab.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LaporLab.View.FormLapor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using LaporLab.Controller;
using LaporLab.Model.Entity;
using System.Net.NetworkInformation;

namespace LaporLab
{
    public partial class DashboardLapor : Form
    {

        // deklarasi objek collection untuk menampung objek mahasiswa
        private List<BarangRusak> listOfBarangRusak = new List<BarangRusak>();

        // deklarasi objek controller
        private BarangRusakController controller;
        private string status;
        private FormRuangan formRuangan = new FormRuangan();

        public DashboardLapor()
        {
            InitializeComponent();
            controller = new BarangRusakController();
            InitializeListView();
            LoadDataMahasiswa();
            UpdateJumlahBarangRusak();

        }
        private void InitializeListView()
        {

            // Menambahkan ListViewItem ke ListView
            lvmBarangRusak.View = System.Windows.Forms.View.Details;
            lvmBarangRusak.FullRowSelect = true;
            lvmBarangRusak.GridLines = true;

            lvmBarangRusak.Columns.Add("No.", 50, HorizontalAlignment.Center);
            lvmBarangRusak.Columns.Add("Ruang", 90, HorizontalAlignment.Center);
            lvmBarangRusak.Columns.Add("Nomor", 90, HorizontalAlignment.Left);
            lvmBarangRusak.Columns.Add("Jenis", 120, HorizontalAlignment.Center);
            lvmBarangRusak.Columns.Add("Keterangan", 470, HorizontalAlignment.Center);
            lvmBarangRusak.Columns.Add("Status", 130, HorizontalAlignment.Center);
            lvmBarangRusak.Columns.Add("Tanggal", 130, HorizontalAlignment.Center);
        }


        // method untuk menampilkan semua data mahasiswa
        private void LoadDataMahasiswa()
        {
            // kosongkan listview
            lvmBarangRusak.Items.Clear();

            // panggil method ReadAll dan tampung datanya ke dalam collection
            listOfBarangRusak = controller.ReadAll();

            // ekstrak objek mhs dari collection
            foreach (var brgRusak in listOfBarangRusak)
            {
                var noUrut = lvmBarangRusak.Items.Count + 1;

                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(brgRusak.no_ruangan);
                item.SubItems.Add(brgRusak.no_komputer);
                item.SubItems.Add(brgRusak.jenis);
                item.SubItems.Add(brgRusak.keterangan);
                item.SubItems.Add(brgRusak.status);
                item.SubItems.Add(brgRusak.tanggal_pinjam);


                // tampilkan data mhs ke listview
                lvmBarangRusak.Items.Add(item);
            }
        }

        // method event handler untuk merespon event OnCreate,
        private void OnCreateEventHandler(BarangRusak brgRusak)
        {
            // tambahkan objek mhs yang baru ke dalam collection
            listOfBarangRusak.Add(brgRusak);

            int noUrut = lvmBarangRusak.Items.Count + 1;

            // tampilkan data mhs yg baru ke list view
            ListViewItem item = new ListViewItem(noUrut.ToString());
            item.SubItems.Add(brgRusak.no_ruangan);
            item.SubItems.Add(brgRusak.no_komputer);
            item.SubItems.Add(brgRusak.jenis);
            item.SubItems.Add(brgRusak.keterangan);
            item.SubItems.Add(brgRusak.status);
            item.SubItems.Add(brgRusak.tanggal_pinjam);

            lvmBarangRusak.Items.Add(item);
            UpdateJumlahBarangRusak();
        }


        // method event handler untuk merespon event OnUpdate,
        private void OnUpdateEventHandler(BarangRusak brgRusak)
        {
            // ambil index data mhs yang edit
            int index = lvmBarangRusak.SelectedIndices[0];

            // update informasi mhs di listview
            ListViewItem itemRow = lvmBarangRusak.Items[index];
            itemRow.SubItems[5].Text = brgRusak.status;
            UpdateJumlahBarangRusak();

        }



        private void btnLapor_Click(object sender, EventArgs e)
        {

            // buat objek form entry data barang rusak
            FormLapor frmLapor = new FormLapor("Tambahkan Data Barang Rusak", controller);

            // mendaftarkan method event handler untuk merespon event OnCreate
            frmLapor.OnCreate += OnCreateEventHandler;

            frmLapor.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvmBarangRusak.SelectedItems.Count > 0)
            {
                // ambil objek mhs yang mau diedit dari collection
                BarangRusak brgRusak = listOfBarangRusak[lvmBarangRusak.SelectedIndices[0]];

                // buat objek form entry data mahasiswa
                FormEditLapor frmEditLapor = new FormEditLapor("Edit Data Barang Rusak", brgRusak, controller);

                // mendaftarkan method event handler untuk merespon event OnUpdate
                frmEditLapor.OnUpdate += OnUpdateEventHandler;

                // tampilkan form entry mahasiswa
                frmEditLapor.ShowDialog();
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data belum dipilih", "Peringatan", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
            }
        }



        private void btnHapus_Click_1(object sender, EventArgs e)
        {
            if (lvmBarangRusak.SelectedItems.Count > 0)
            {
                var konfirmasi = MessageBox.Show("Apakah data mahasiswa ingin dihapus?", "Konfirmasi",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (konfirmasi == DialogResult.Yes)
                {
                    // ambil objek mhs yang mau dihapus dari collection
                    BarangRusak brgRusak = listOfBarangRusak[lvmBarangRusak.SelectedIndices[0]];

                    // panggil operasi CRUD
                    var result = controller.Delete(brgRusak);
                    if (result > 0) LoadDataMahasiswa();
                    UpdateJumlahBarangRusak();
                }
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data mahasiswa belum dipilih !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void UpdateJumlahBarangRusak()
        {
            string belumDiperbaiki = "Belum Diperbaiki";
            string sedangDiperbaiki = "Sedang Diperbaiki";
            string selesaiDiperbaiki = "Selesai Diperbaiki";
            
            // Memanggil method dari controller untuk menghitung jumlah barang rusak
            
            int jumlahBelumDiperbaiki = controller.CountBarangByStatus(belumDiperbaiki);
            int jumlahSedangDiperbaiki = controller.CountBarangByStatus(sedangDiperbaiki);
            int jumlahSelesaiDiperbaiki = controller.CountBarangByStatus(selesaiDiperbaiki);

            // Menampilkan jumlah barang rusak pada textbox atau label yang sesuai
            lblBelumDiperbaiki.Text = jumlahBelumDiperbaiki.ToString();
            lblSedangDiperbaiki.Text = jumlahSedangDiperbaiki.ToString();
            lblSelesaiDiperbaiki.Text = jumlahSelesaiDiperbaiki.ToString();
        }




        //tombol keluar
        private void bunifuButton22_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                // Tutup form dashboard
                this.Hide();

                // Tampilkan kembali form login
                FrmLogin frmLogin = new FrmLogin();
                frmLogin.ShowDialog();

                // Setelah login berhasil, lanjutkan ke dashboard atau aplikasi utama
                if (frmLogin.DialogResult == DialogResult.OK)
                {
                    // Tampilkan form dashboard atau form lainnya
                    // Misalnya:
                    DashboardLapor dashboard = new DashboardLapor();
                    dashboard.ShowDialog();
                }
                else
                {
                    // Jika login dibatalkan, keluar dari aplikasi atau tampilkan pesan sesuai kebutuhan
                    Application.Exit();
                }
            }
            else
            {
                // Jika pengguna memilih untuk tidak logout, tidak ada tindakan yang diambil
                // Pengguna tetap berada di dashboard
            }
        }





        //button cari
        private void bunifubtnCari_Click(object sender, EventArgs e)
        {
            StatusBarangRusak();
        }

        private void StatusBarangRusak()
        {

            if (bunifudrdPilihLab.SelectedItem != null)
            {
                status = bunifudrdPilihLab.SelectedItem.ToString();


                if (status == "Semua")
                {
                    // Jika dipilih "Semua Data", panggil ReadAll() dari controller
                    listOfBarangRusak = controller.ReadAll();
                    DisplayData(listOfBarangRusak); // Tampilkan data yang didapatkan ke ListView
                }
                else
                {
                    // Jika dipilih opsi lain, panggil ReadByStatus() dengan status yang dipilih
                    listOfBarangRusak = controller.ReadByStatus(status);
                    DisplayData(listOfBarangRusak); // Tampilkan data yang didapatkan ke ListView
                }



            }
            
        }



        private void DisplayData(List<BarangRusak> data)
        {
            lvmBarangRusak.Items.Clear();

            foreach (var brgRusak in data)
            {
                var noUrut = lvmBarangRusak.Items.Count + 1;

                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(brgRusak.no_ruangan);
                item.SubItems.Add(brgRusak.no_komputer);
                item.SubItems.Add(brgRusak.jenis);
                item.SubItems.Add(brgRusak.keterangan);
                item.SubItems.Add(brgRusak.status);
                item.SubItems.Add(brgRusak.tanggal_pinjam);

                lvmBarangRusak.Items.Add(item);
            }
        }




        private void DashboardLapor_Load(object sender, EventArgs e)
        {
            // Tambahkan UserControlRuangan ke dalam panel
            bunifupanelCari.Controls.Add(formRuangan);

            bunifubtnCari.Visible = true;
            formRuangan.Visible = false;
            bunifubtnCari.BringToFront();         
        }

        //untuk ruang
        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            bunifubtnCari.Visible = false;
            formRuangan.Visible = true;
            formRuangan.BringToFront();
        }

        //untuk lapor
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            bunifubtnCari.Visible = true;
            formRuangan.Visible = false;
            bunifubtnCari.BringToFront();
        }
    }
}



