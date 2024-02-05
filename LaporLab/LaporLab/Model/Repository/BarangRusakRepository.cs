using LaporLab.Model.Context;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaporLab.Model.Entity;


namespace LaporLab.Model.Repository
{
    public class BarangRusakRepository
    {
        private SQLiteConnection _conn;

        public BarangRusakRepository(DBContext context)
        {
            // inisialisasi objek connection
            _conn = context.Conn;
        }

        public int Create(BarangRusak brgRusak)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"insert into barang_rusak (no_ruangan, jenis, status, keterangan, password, no_komputer, tanggal_pinjam) values (@no_ruangan, @jenis, @status, @keterangan, @password, @no_komputer, @tanggal_pinjam)";

            // membuat objek command menggunakan blok using
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {



                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@no_ruangan", brgRusak.no_ruangan);
                cmd.Parameters.AddWithValue("@jenis", brgRusak.jenis);
                cmd.Parameters.AddWithValue("@status", brgRusak.status);
                cmd.Parameters.AddWithValue("@keterangan", brgRusak.keterangan);
                cmd.Parameters.AddWithValue("@password", brgRusak.petugas);
                cmd.Parameters.AddWithValue("@no_komputer", brgRusak.no_komputer);
                cmd.Parameters.AddWithValue("@tanggal_pinjam", brgRusak.tanggal_pinjam);

                try
                {
                    // jalankan perintah INSERT dan tampung hasilnya ke dalam variabel result
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                }
            }

            return result;
        }




        public bool IsDuplicate(BarangRusak brgRusak)
        {
           
            string sql = "SELECT COUNT(*) FROM barang_rusak WHERE no_ruangan = @noRuangan AND jenis = @jenis AND no_komputer = @noKomputer";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@noRuangan", brgRusak.no_ruangan);
                cmd.Parameters.AddWithValue("@jenis", brgRusak.jenis);
                cmd.Parameters.AddWithValue("@noKomputer", brgRusak.no_komputer);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                // Jika count > 0, data sudah ada di dalam basis data
                return count > 0;
            }
        }






        public int Update(BarangRusak brgRusak)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"update barang_rusak set status = @status where no_komputer = @no_komputer AND no_ruangan = @no_ruangan";

            // membuat objek command menggunakan blok using
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@status", brgRusak.status);
                cmd.Parameters.AddWithValue("@no_komputer", brgRusak.no_komputer);
                cmd.Parameters.AddWithValue("@no_ruangan", brgRusak.no_ruangan);


                try
                {
                    // jalankan perintah UPDATE dan tampung hasilnya ke dalam variabel result
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Update error: {0}", ex.Message);
                }
            }

            return result;
        }



        public int Delete(BarangRusak brgRusak)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"delete from barang_rusak WHERE no_komputer = @no_komputer AND no_ruangan = @no_ruangan";

            // membuat objek command menggunakan blok using
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@no_komputer", brgRusak.no_komputer);
                cmd.Parameters.AddWithValue("@no_ruangan", brgRusak.no_ruangan);

                try
                {
                    // jalankan perintah DELETE dan tampung hasilnya ke dalam variabel result
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Delete error: {0}", ex.Message);
                }
            }

            return result;
        }




        public List<BarangRusak> ReadAll()
        {
            // membuat objek collection untuk menampung objek BarangRusak
            List<BarangRusak> list = new List<BarangRusak>();

            try
            {
                // deklarasi perintah SQL
                string sql = @"select no_ruangan, jenis, status, keterangan, password, no_komputer, tanggal_pinjam from barang_rusak order by no_ruangan";

                // membuat objek command menggunakan blok using
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    // membuat objek dtr (data reader) untuk menampung result set (hasil perintah SELECT)
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                        while (dtr.Read())
                        {
                            // proses konversi dari row result set ke object
                            BarangRusak brgRusak = new BarangRusak();
                            brgRusak.no_ruangan = dtr["no_ruangan"].ToString();
                            brgRusak.no_komputer = dtr["no_komputer"].ToString();
                            brgRusak.jenis = dtr["jenis"].ToString();
                            brgRusak.keterangan = dtr["keterangan"].ToString();
                            brgRusak.status = dtr["status"].ToString();
                            brgRusak.tanggal_pinjam = dtr["tanggal_pinjam"].ToString();

                            // tambahkan objek BarangRusak ke dalam collection
                            list.Add(brgRusak);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll error: {0}", ex.Message);
            }

            return list;
        }



        public List<BarangRusak> ReadByStatus(string status)
        {
            // membuat objek collection untuk menampung objek BarangRusak
            List<BarangRusak> list = new List<BarangRusak>();

            try
            {
                // deklarasi perintah SQL
                string sql = @"select no_ruangan, jenis, status, keterangan, password, no_komputer from barang_rusak where status like @status order by no_ruangan";

                // membuat objek command menggunakan blok using
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    // mendaftarkan parameter dan mengeset nilainya
                    cmd.Parameters.AddWithValue("@status", status);

                    // membuat objek dtr (data reader) untuk menampung result set (hasil perintah SELECT)
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                        while (dtr.Read())
                        {
                            // proses konversi dari row result set ke object
                            BarangRusak brgRusak = new BarangRusak();
                            brgRusak.no_ruangan = dtr["no_ruangan"].ToString();
                            brgRusak.no_komputer = dtr["no_komputer"].ToString();
                            brgRusak.jenis = dtr["jenis"].ToString();
                            brgRusak.keterangan = dtr["keterangan"].ToString();
                            brgRusak.status = dtr["status"].ToString();

                            // tambahkan objek BarangRusak ke dalam collection
                            list.Add(brgRusak);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByStatus error: {0}", ex.Message);
            }

            return list;
        }


        public int CountBarangByStatus(string status)
        {
            int jumlahBarang = 0;

            try
            {
                // deklarasi perintah SQL
                string sql = @"SELECT COUNT(*) AS JumlahBarang
                       FROM barang_rusak
                       WHERE status = @status";

                // membuat objek command menggunakan blok using
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    // mendaftarkan parameter dan mengeset nilainya
                    cmd.Parameters.AddWithValue("@status", status);

                    // eksekusi perintah SQL dan baca jumlah baris yang memenuhi kriteria
                    jumlahBarang = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error counting barang: " + ex.Message);
            }

            return jumlahBarang;
        }



    }
}
