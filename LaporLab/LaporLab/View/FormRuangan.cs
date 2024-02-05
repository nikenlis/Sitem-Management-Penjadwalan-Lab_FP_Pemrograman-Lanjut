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
    public partial class FormRuangan : UserControl
    {
        // deklarasi objek collection untuk menampung objek mahasiswa
        private List<Ruangan> listOfRuangan = new List<Ruangan>();

        // deklarasi objek controller
        private RuanganController controller;
        public FormRuangan()
        {
            InitializeComponent();
            controller = new RuanganController();
            InitializeListView();
            LoadDataMahasiswa();
        }


        private void InitializeListView()
        {

            // Menambahkan ListViewItem ke ListView
            lvmRuangan.View = System.Windows.Forms.View.Details;
            lvmRuangan.FullRowSelect = true;
            lvmRuangan.GridLines = true;

            lvmRuangan.Columns.Add("No ", 100, HorizontalAlignment.Center);
            lvmRuangan.Columns.Add("No Ruangan", 150, HorizontalAlignment.Center);
            lvmRuangan.Columns.Add("Jumlah Keyboard", 150, HorizontalAlignment.Left);
            lvmRuangan.Columns.Add("Jumlah Komputer", 150, HorizontalAlignment.Center);
            lvmRuangan.Columns.Add("Jumlah Mouse", 150, HorizontalAlignment.Center);
            
        }

        private void LoadDataMahasiswa()
        {
            // kosongkan listview
            lvmRuangan.Items.Clear();

            // panggil method ReadAll dan tampung datanya ke dalam collection
            listOfRuangan = controller.ReadAll();

            // ekstrak objek mhs dari collection
            foreach (var ruangan in listOfRuangan)
            {
                var noUrut = lvmRuangan.Items.Count + 1;

                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(ruangan.no_ruangan);
                item.SubItems.Add(ruangan.jmlKeyboard);
                item.SubItems.Add(ruangan.jmlKomputer);
                item.SubItems.Add(ruangan.jmlMouse);


                // tampilkan data mhs ke listview
                lvmRuangan.Items.Add(item);
            }
        }
    }
}
