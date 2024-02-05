using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaporLab.Model.Context;
using LaporLab.Model.Entity;

namespace LaporLab.Model.Repository
{
    
    public class RuanganRepository
    {
        private SQLiteConnection _conn;

        public RuanganRepository(DBContext context)
        {
            // inisialisasi objek connection
            _conn = context.Conn;
        }

        public List<Ruangan> ReadAll()
        {
            // membuat objek collection untuk menampung objek Ruangan
            List<Ruangan> list = new List<Ruangan>();

            try
            {
                // deklarasi perintah SQL
                string sql = @"select no_ruangan, jml_keyboard, jml_komputer, jml_mouse from ruangan";

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
                            Ruangan ruangan = new Ruangan();
                            ruangan.no_ruangan = dtr["no_ruangan"].ToString();
                            ruangan.jmlKeyboard = dtr["jml_keyboard"].ToString();
                            ruangan.jmlKomputer = dtr["jml_komputer"].ToString();
                            ruangan.jmlMouse = dtr["jml_mouse"].ToString();
                           

                            // tambahkan objek Ruangan ke dalam collection
                            list.Add(ruangan);
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

    }
}
