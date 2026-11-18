using System;
using System.Collections.Generic;

namespace Soal
{
    // soal 1 - class dan object
    public class Agen
    {
        public string idAgen;
        public string namaAgen;
        public int stokAgen;

        public void TampilInfo()
        {
            Console.WriteLine("ID Agen   : " + idAgen);
            Console.WriteLine("Nama Agen : " + namaAgen);
            Console.WriteLine("Stok Agen : " + stokAgen);
        }
    }

    // soal 2 - encapsulation
    // soal 5 - abstract class
    public abstract class ProdukGalon
    {
        private string idProduk;
        private string namaProduk;
        private int stokGudang;

        public string IdProduk
        {
            get { return idProduk; }
            set { idProduk = value; }
        }

        public string NamaProduk
        {
            get { return namaProduk; }
            set { namaProduk = value; }
        }

        public int StokGudang
        {
            get { return stokGudang; }
            set
            {
                if (value >= stokGudang)
                    stokGudang = value;
                else
                    Console.WriteLine("Stok hanya bisa dikurangi melalui KurangiStok!");
            }
        }

        public void KurangiStok(int jumlah)
        {
            if (jumlah > 0 && jumlah <= stokGudang)
                stokGudang -= jumlah;
            else
                Console.WriteLine("Jumlah stok tidak valid!");
        }

        public abstract int HitungBiaya(int jumlah);
    }

    // soal 3 - inheritance
    // soal 4 - polymorphism
    public class GalonAkua : ProdukGalon
    {
        public override int HitungBiaya(int jumlah)
        {
            return jumlah * 20000;
        }
    }

    public class GalonBerkeran : ProdukGalon
    {
        public string jenisKeran;

        public override int HitungBiaya(int jumlah)
        {
            return jumlah * 12000;
        }
    }

    // soal 6 - interface
    public interface DapatDidistribusikan
    {
        void KirimPaket();
    }

    public class DistribusiGalon : DapatDidistribusikan
    {
        public Agen agenTujuan;
        public ProdukGalon produk;
        public int jumlah;

        public void KirimPaket()
        {
            if (jumlah <= 0)
            {
                Console.WriteLine("Jumlah pengiriman harus lebih dari 0!");
            }
            else if (jumlah > produk.StokGudang)
            {
                Console.WriteLine("Stok gudang tidak mencukupi!");
            }
            else
            {
                produk.KurangiStok(jumlah);
                agenTujuan.stokAgen += jumlah;

                Console.WriteLine("Pengiriman berhasil!");
            }
        }
    }

    // soal 7 - aggregation
    public class SindikatGalon
    {
        public List<Agen> daftarAgen = new List<Agen>();
        public List<ProdukGalon> daftarProduk = new List<ProdukGalon>();

        public void TambahAgen(Agen agen)
        {
            daftarAgen.Add(agen);
        }

        public void TambahProduk(ProdukGalon produk)
        {
            daftarProduk.Add(produk);
        }

        public void TampilkanSindikat()
        {
            Console.WriteLine("\n=== DAFTAR AGEN ===");

            foreach (Agen agen in daftarAgen)
            {
                agen.TampilInfo();
                Console.WriteLine();
            }

            Console.WriteLine("=== DAFTAR PRODUK ===");

            foreach (ProdukGalon produk in daftarProduk)
            {
                Console.WriteLine("ID Produk   : " + produk.IdProduk);
                Console.WriteLine("Nama Produk : " + produk.NamaProduk);
                Console.WriteLine("Stok Gudang : " + produk.StokGudang);
                Console.WriteLine();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Agen agen1 = new Agen();
            agen1.idAgen = "A001";
            agen1.namaAgen = "Agen Jember";
            agen1.stokAgen = 10;

            Agen agen2 = new Agen();
            agen2.idAgen = "A002";
            agen2.namaAgen = "Agen Bondowoso";
            agen2.stokAgen = 15;

            agen1.TampilInfo();
            agen2.TampilInfo();

            GalonAkua akua = new GalonAkua();
            akua.IdProduk = "P001";
            akua.NamaProduk = "Galon Akua";
            akua.StokGudang = 100;

            GalonBerkeran berkeran = new GalonBerkeran();
            berkeran.IdProduk = "P002";
            berkeran.NamaProduk = "Galon Berkeran";
            berkeran.StokGudang = 50;
            berkeran.jenisKeran = "Keran Putar";

            ProdukGalon produk1 = akua;
            ProdukGalon produk2 = berkeran;

            Console.WriteLine("\n=== HITUNG BIAYA ===");
            Console.WriteLine("Akua 5 galon : Rp" + produk1.HitungBiaya(5));
            Console.WriteLine("Berkeran 5 galon : Rp" + produk2.HitungBiaya(5));

            DistribusiGalon distribusi = new DistribusiGalon();
            distribusi.agenTujuan = agen1;
            distribusi.produk = akua;
            distribusi.jumlah = 20;

            Console.WriteLine("\n=== DISTRIBUSI ===");
            Console.WriteLine("Stok gudang sebelum : " + akua.StokGudang);
            Console.WriteLine("Stok agen sebelum : " + agen1.stokAgen);

            distribusi.KirimPaket();

            Console.WriteLine("Stok gudang sesudah : " + akua.StokGudang);
            Console.WriteLine("Stok agen sesudah : " + agen1.stokAgen);

            SindikatGalon sindikat = new SindikatGalon();

            sindikat.TambahAgen(agen1);
            sindikat.TambahAgen(agen2);
            sindikat.TambahProduk(akua);
            sindikat.TambahProduk(berkeran);

            sindikat.TampilkanSindikat();

            Console.ReadKey();
        }
    }
}
