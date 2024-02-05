using LaporLab.Model.Context;
using LaporLab.Model.Entity;
using LaporLab.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaporLab.Controller
{
    public class RuanganController
    {
        private RuanganRepository _repository;
        // Method untuk menampilkan semua data Ruangan
        public List<Ruangan> ReadAll()
        {
            // membuat objek collection
            List<Ruangan> list = new List<Ruangan>();

            // membuat objek context menggunakan blok using
            using (DBContext context = new DBContext())
            {
                // membuat objek dari class repository
                _repository = new RuanganRepository(context);

                // panggil method GetAll yang ada di dalam class repository
                list = _repository.ReadAll();
            }

            return list;
        }
    }
}
