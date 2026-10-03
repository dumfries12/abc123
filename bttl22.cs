using System;
using System.Collections.Generic;
using System.Linq;

namespace baitaptrenlop2
{
    class Student
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double GPA { get; set; }

        public Student()
        {
        }

        public Student(string id, string name, int age, double gpa)
        {
            Id = id;
            Name = name;
            Age = age;
            GPA = gpa;
        }

        public void Display()
        {
            Console.WriteLine(
                $"ID: {Id} | Name: {Name} | Age: {Age} | GPA: {GPA:F2}"
            );
        }
    }

    class StudentDAO
    {
        private List<Student> students = new List<Student>();

        public void Add(Student student)
        {
            if (GetById(student.Id) != null)
            {
                Console.WriteLine("ID da ton tai!");
                return;
            }

            students.Add(student);
            Console.WriteLine("Them sinh vien thanh cong!");
        }

        public void Edit(Student student)
        {
            Student oldStudent = GetById(student.Id);

            if (oldStudent == null)
            {
                Console.WriteLine("Khong tim thay sinh vien!");
                return;
            }

            oldStudent.Name = student.Name;
            oldStudent.Age = student.Age;
            oldStudent.GPA = student.GPA;

            Console.WriteLine("Sua sinh vien thanh cong!");
        }

        public void Delete(string id)
        {
            Student student = GetById(id);

            if (student == null)
            {
                Console.WriteLine("Khong tim thay sinh vien!");
                return;
            }

            students.Remove(student);
            Console.WriteLine("Xoa sinh vien thanh cong!");
        }

        public List<Student> GetAlls()
        {
            return students;
        }

        public Student GetById(string id)
        {
            return students.FirstOrDefault(s => s.Id == id);
        }

        public List<Student> GetByName(string name)
        {
            return students
                .Where(s => s.Name.ToLower().Contains(name.ToLower()))
                .ToList();
        }

        public List<Student> GetByGPA(double gpa)
        {
            return students
                .Where(s => s.GPA >= gpa)
                .ToList();
        }
    }

    class Program
    {
        static StudentDAO dao = new StudentDAO();

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("\n========== QUAN LY SINH VIEN ==========");
                Console.WriteLine("1. Them sinh vien");
                Console.WriteLine("2. Sua sinh vien");
                Console.WriteLine("3. Xoa sinh vien");
                Console.WriteLine("4. Hien thi danh sach");
                Console.WriteLine("5. Tim sinh vien theo ID");
                Console.WriteLine("6. Tim sinh vien theo ten");
                Console.WriteLine("7. Tim sinh vien theo GPA");
                Console.WriteLine("0. Thoat");
                Console.Write("Chon chuc nang: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;

                    case "2":
                        EditStudent();
                        break;

                    case "3":
                        DeleteStudent();
                        break;

                    case "4":
                        ShowAllStudents();
                        break;

                    case "5":
                        FindById();
                        break;

                    case "6":
                        FindByName();
                        break;

                    case "7":
                        FindByGPA();
                        break;

                    case "0":
                        Console.WriteLine("Ket thuc chuong trinh!");
                        return;

                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        break;
                }
            }
        }

        static void AddStudent()
        {
            Console.WriteLine("\n--- THEM SINH VIEN ---");

            Console.Write("Nhap ID: ");
            string id = Console.ReadLine();

            Console.Write("Nhap ten: ");
            string name = Console.ReadLine();

            Console.Write("Nhap tuoi: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Nhap GPA: ");
            double gpa = double.Parse(Console.ReadLine());

            Student student = new Student(id, name, age, gpa);

            dao.Add(student);
        }

        static void EditStudent()
        {
            Console.WriteLine("\n--- SUA SINH VIEN ---");

            Console.Write("Nhap ID sinh vien can sua: ");
            string id = Console.ReadLine();

            Student oldStudent = dao.GetById(id);

            if (oldStudent == null)
            {
                Console.WriteLine("Khong tim thay sinh vien!");
                return;
            }

            Console.Write("Nhap ten moi: ");
            string name = Console.ReadLine();

            Console.Write("Nhap tuoi moi: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Nhap GPA moi: ");
            double gpa = double.Parse(Console.ReadLine());

            Student student = new Student(id, name, age, gpa);

            dao.Edit(student);
        }

        static void DeleteStudent()
        {
            Console.WriteLine("\n--- XOA SINH VIEN ---");

            Console.Write("Nhap ID sinh vien can xoa: ");
            string id = Console.ReadLine();

            dao.Delete(id);
        }

        static void ShowAllStudents()
        {
            Console.WriteLine("\n--- DANH SACH SINH VIEN ---");

            List<Student> list = dao.GetAlls();

            if (list.Count == 0)
            {
                Console.WriteLine("Danh sach rong!");
                return;
            }

            foreach (Student student in list)
            {
                student.Display();
            }
        }

        static void FindById()
        {
            Console.WriteLine("\n--- TIM THEO ID ---");

            Console.Write("Nhap ID: ");
            string id = Console.ReadLine();

            Student student = dao.GetById(id);

            if (student == null)
            {
                Console.WriteLine("Khong tim thay sinh vien!");
            }
            else
            {
                student.Display();
            }
        }

        static void FindByName()
        {
            Console.WriteLine("\n--- TIM THEO TEN ---");

            Console.Write("Nhap ten can tim: ");
            string name = Console.ReadLine();

            List<Student> result = dao.GetByName(name);

            if (result.Count == 0)
            {
                Console.WriteLine("Khong tim thay sinh vien!");
                return;
            }

            foreach (Student student in result)
            {
                student.Display();
            }
        }

        static void FindByGPA()
        {
            Console.WriteLine("\n--- TIM THEO GPA ---");

            Console.Write("Nhap GPA toi thieu: ");
            double gpa = double.Parse(Console.ReadLine());

            List<Student> result = dao.GetByGPA(gpa);

            if (result.Count == 0)
            {
                Console.WriteLine("Khong tim thay sinh vien!");
                return;
            }

            foreach (Student student in result)
            {
                student.Display();
            }
        }
    }
}