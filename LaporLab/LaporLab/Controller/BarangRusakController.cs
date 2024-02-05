using LaporLab.Model.Context;
using LaporLab.Model.Entity;
using LaporLab.View;
using LaporLab.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.UI.WinForms;

namespace LaporLab.Controller
{
    public class BarangRusakController
    {



        private BarangRusakRepository _repository;


        public bool IsDuplicateData(BarangRusak brgRusak)
        {
            using (DBContext context = new DBContext())
            {
                _repository = new BarangRusakRepository(context);
                // Panggil method di repository untuk memeriksa keberadaan data yang sama
                return _repository.IsDuplicate(brgRusak);
            }
        }


        public int Create(BarangRusak brgRusak)
        {
            int result = 0;


            if (string.IsNullOrEmpty(brgRusak.no_ruangan))
            {
                MessageBox.Show("Pilih ruangan terlebih dahulu !!!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }


            // cek jenis yang diinputkan tidak boleh kosong
            if (string.IsNullOrEmpty(brgRusak.jenis))
            {
                MessageBox.Show("Jenis barang rusak harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }


            if (string.IsNullOrEmpty(brgRusak.no_komputer))
            {
                MessageBox.Show("Pilih no komputer terlebih dahulu !!!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // cek keterangan yang diinputkan tidak boleh kosong
            if (string.IsNullOrEmpty(brgRusak.keterangan))
            {
                MessageBox.Show("Keterangan harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // Validasi untuk memeriksa duplikasi data
            if (IsDuplicateData(brgRusak))
            {
                MessageBox.Show("Data sudah ada di dalam basis data.", "Duplikasi Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }




            // membuat objek context menggunakan blok using
            using (DBContext context = new DBContext())
            {
                // membuat objek class repository
                _repository = new BarangRusakRepository(context);

                // panggil method Create class repository untuk menambahkan data
                result = _repository.Create(brgRusak);
            }

            if (result > 0)
            {
                MessageBox.Show("Data Barang Rusak berhasil disimpan !", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data Barang Rusak gagal disimpan !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }


        public int Update(BarangRusak brgRusak)
        {
            int result = 0;

            // cek npm yang diinputkan tidak boleh kosong
            if (string.IsNullOrEmpty(brgRusak.status))
            {
                MessageBox.Show("checklist harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }


            // membuat objek context menggunakan blok using
            using (DBContext context = new DBContext())
            {
                // membuat objek dari class repository
                _repository = new BarangRusakRepository(context);

                // panggil method Update class repository untuk mengupdate data
                result = _repository.Update(brgRusak);
            }

            if (result > 0)
            {
                MessageBox.Show("Data barang rusak berhasil diupdate !", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data barang rusakl diupdate !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }


        public int Delete(BarangRusak brgRusak)
        {
            int result = 0;

            // membuat objek context menggunakan blok using
            using (DBContext context = new DBContext())
            {
                // membuat objek dari class repository
                _repository = new BarangRusakRepository(context);

                // panggil method Delete class repository untuk menghapus data
                result = _repository.Delete(brgRusak);
            }

            if (result > 0)
            {
                MessageBox.Show("Data BarangRusak berhasil dihapus !", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data BarangRusak gagal dihapus !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }


        // Method untuk menampilkan semua data BarangRusak
        public List<BarangRusak> ReadAll()
        {
            // membuat objek collection
            List<BarangRusak> list = new List<BarangRusak>();

            // membuat objek context menggunakan blok using
            using (DBContext context = new DBContext())
            {
                // membuat objek dari class repository
                _repository = new BarangRusakRepository(context);

                // panggil method GetAll yang ada di dalam class repository
                list = _repository.ReadAll();
            }

            return list;
        }


        public List<BarangRusak> ReadByStatus(string status)
        {
            // membuat objek collection
            List<BarangRusak> list = new List<BarangRusak>();

            // membuat objek context menggunakan blok using
            using (DBContext context = new DBContext())
            {
                // membuat objek dari class repository
                _repository = new BarangRusakRepository(context);

                // panggil method GetByNama yang ada di dalam class repository
                list = _repository.ReadByStatus(status);
            }

            return list;
        }


        public int CountBarangByStatus(string status)
        {
            int count = 0;

            // membuat objek context menggunakan blok using
            using (DBContext context = new DBContext())
            {
                // membuat objek dari class repository
                _repository = new BarangRusakRepository(context);

                // panggil method GetByNama yang ada di dalam class repository
                count = _repository.CountBarangByStatus(status);
            }

            return count;
        }


    }
}
