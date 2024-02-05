using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaporLab.Model.Repository;

namespace LaporLab.Model.Entity
{
    public class BarangRusak
    {
        public string id_barang_rusak { get; set; }
        public string jenis { get; set; }
        public string status { get; set; }
        public string keterangan { get; set; }

        public string no_komputer { get; set; }

        public string petugas { get; set; }
        public string no_ruangan { get; set; }

        public string tanggal_pinjam { get; set; }

    }

    
}
