﻿using LaporLab.Model.Context;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace LaporLab.Model.Repository
{
    public class UserRepository
    {
        // deklarsi objek connection
        private SQLiteConnection _conn;

        // constructor
        public UserRepository(DBContext context)
        {
            // inisialisasi objek connection
            _conn = context.Conn;
        }

        public bool IsValidUser(string userName, string password)
        {
            bool result = false;

            string sql = @"select count(*) as row_count
                           from petugas
                           where nama = @userName and password = @password";

            // membuat objek command menggunakan blok using
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@password", password);

                // membuat objek dtr (data reader) untuk menampung result set (hasil perintah SELECT)
                using (SQLiteDataReader dtr = cmd.ExecuteReader())
                {
                    // panggil method Read untuk mendapatkan baris dari result set
                    if (dtr.Read())
                    {
                        result = Convert.ToInt32(dtr["row_count"]) > 0;
                    }
                }
            }
            return result;
        }
    }
}
