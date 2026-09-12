using System;
using System.Collections.Generic;

interface Hinh
{
    void Nhap();
    double GetDienTich();
    double GetChuVi();
    void HienThi();
}

class HinhTron : Hinh
{
    private double banKinh;

    public double BanKinh
    {
        get { return banKinh; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Ban kinh phai lon hon 0!");
            banKinh = value;
        }
    }

    public HinhTron() { }

    public HinhTron(double banKinh)
    {
        BanKinh = banKinh;
    }

    public void Nhap()
    {
        bool hopLe = false;
        while (!hopLe)
        {
            try
            {
                Console.Write("Nhap ban kinh hinh tron: ");
                double r = double.Parse(Console.ReadLine());
                BanKinh = r;
                hopLe = true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Gia tri nhap khong phai la so. Vui long nhap lai!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message + " Vui long nhap lai!");
            }
        }
    }

    public double GetDienTich()
    {
        return Math.PI * banKinh * banKinh;
    }

    public double GetChuVi()
    {
        return 2 * Math.PI * banKinh;
    }

    public void HienThi()
    {
        Console.WriteLine("--- HINH TRON ---");
        Console.WriteLine($"Ban kinh: {banKinh}");
        Console.WriteLine($"Dien tich: {GetDienTich():F2}");
        Console.WriteLine($"Chu vi: {GetChuVi():F2}");
    }
}

class HinhChuNhat : Hinh
{
    private double chieuDai;
    private double chieuRong;

    public double ChieuDai
    {
        get { return chieuDai; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Chieu dai phai lon hon 0!");
            chieuDai = value;
        }
    }

    public double ChieuRong
    {
        get { return chieuRong; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Chieu rong phai lon hon 0!");
            chieuRong = value;
        }
    }

    public HinhChuNhat() { }

    public HinhChuNhat(double chieuDai, double chieuRong)
    {
        ChieuDai = chieuDai;
        ChieuRong = chieuRong;
    }

    public void Nhap()
    {
        bool hopLe = false;
        while (!hopLe)
        {
            try
            {
                Console.Write("Nhap chieu dai: ");
                double d = double.Parse(Console.ReadLine());
                Console.Write("Nhap chieu rong: ");
                double r = double.Parse(Console.ReadLine());
                ChieuDai = d;
                ChieuRong = r;
                hopLe = true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Gia tri nhap khong phai la so. Vui long nhap lai!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message + " Vui long nhap lai!");
            }
        }
    }

    public double GetDienTich()
    {
        return chieuDai * chieuRong;
    }

    public double GetChuVi()
    {
        return 2 * (chieuDai + chieuRong);
    }

    public void HienThi()
    {
        Console.WriteLine("--- HINH CHU NHAT ---");
        Console.WriteLine($"Chieu dai: {chieuDai}, Chieu rong: {chieuRong}");
        Console.WriteLine($"Dien tich: {GetDienTich():F2}");
        Console.WriteLine($"Chu vi: {GetChuVi():F2}");
    }
}

class HinhTamGiac : Hinh
{
    private double canhA;
    private double canhB;
    private double canhC;

    public double CanhA { get { return canhA; } }
    public double CanhB { get { return canhB; } }
    public double CanhC { get { return canhC; } }

    public HinhTamGiac() { }

    public HinhTamGiac(double a, double b, double c)
    {
        if (a <= 0 || b <= 0 || c <= 0)
            throw new ArgumentException("Cac canh phai lon hon 0!");
        if (!IsTamGiac(a, b, c))
            throw new ArgumentException("Ba canh khong tao thanh tam giac hop le!");

        canhA = a;
        canhB = b;
        canhC = c;
    }

    public bool IsTamGiac(double a, double b, double c)
    {
        return (a + b > c) && (a + c > b) && (b + c > a);
    }

    public void Nhap()
    {
        bool hopLe = false;
        while (!hopLe)
        {
            try
            {
                Console.Write("Nhap canh a: ");
                double a = double.Parse(Console.ReadLine());
                Console.Write("Nhap canh b: ");
                double b = double.Parse(Console.ReadLine());
                Console.Write("Nhap canh c: ");
                double c = double.Parse(Console.ReadLine());

                if (a <= 0 || b <= 0 || c <= 0)
                {
                    Console.WriteLine("Cac canh phai lon hon 0! Vui long nhap lai.");
                    continue;
                }

                if (!IsTamGiac(a, b, c))
                {
                    Console.WriteLine("Ba canh khong tao thanh tam giac hop le! Vui long nhap lai.");
                    continue;
                }

                canhA = a;
                canhB = b;
                canhC = c;
                hopLe = true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Gia tri nhap khong phai la so. Vui long nhap lai!");
            }
        }
    }

    public double GetDienTich()
    {
        double p = GetChuVi() / 2;
        return Math.Sqrt(p * (p - canhA) * (p - canhB) * (p - canhC));
    }

    public double GetChuVi()
    {
        return canhA + canhB + canhC;
    }

    public void HienThi()
    {
        Console.WriteLine("--- HINH TAM GIAC ---");
        Console.WriteLine($"Canh a: {canhA}, Canh b: {canhB}, Canh c: {canhC}");
        Console.WriteLine($"Dien tich: {GetDienTich():F2}");
        Console.WriteLine($"Chu vi: {GetChuVi():F2}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Hinh> danhSachHinh = new List<Hinh>();
        int soLuong;

        Console.Write("Nhap so luong hinh can nhap: ");
        soLuong = int.Parse(Console.ReadLine());

        for (int i = 0; i < soLuong; i++)
        {
            Console.WriteLine($"\n=== Nhap hinh thu {i + 1} ===");
            Console.WriteLine("Chon loai hinh: 1-Hinh tron, 2-Hinh chu nhat, 3-Hinh tam giac");
            Console.Write("Lua chon: ");
            int chon = int.Parse(Console.ReadLine());

            Hinh h = null;

            switch (chon)
            {
                case 1:
                    h = new HinhTron();
                    break;
                case 2:
                    h = new HinhChuNhat();
                    break;
                case 3:
                    h = new HinhTamGiac();
                    break;
                default:
                    Console.WriteLine("Lua chon khong hop le, bo qua hinh nay.");
                    continue;
            }

            h.Nhap();
            danhSachHinh.Add(h);
        }

        Console.WriteLine("\n========== KET QUA ==========");
        foreach (Hinh h in danhSachHinh)
        {
            h.HienThi();
            Console.WriteLine();
        }
    }
}
