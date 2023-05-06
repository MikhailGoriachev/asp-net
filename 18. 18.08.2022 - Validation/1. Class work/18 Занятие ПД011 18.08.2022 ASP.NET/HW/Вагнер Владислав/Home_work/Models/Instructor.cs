using System.ComponentModel.DataAnnotations;
using Home_work.Infrastructure;

namespace Home_work.Models;

public class Instructor
    {

        public int Id { get; set; } //Id

        [Display(Name = "Фамилия инструктора")]
        public string Surname { get; set; } //Фамилия 

        [Display(Name = "Имя инструктора")]
        public string Name { get; set; } //Имя

        [Display(Name = "Отчество инструктора")]
        public string Patronymic { get; set; } //Отчество


        [Display(Name = "Категория инструктора")]
        public string Category { get; set; } //Категория

        public string SNP => $"{Surname} {Name[0]}.{Patronymic[0]}.";//Фио


        [Display(Name = "Дата рождения")]
        [UIHint("date")]
        public DateTime BirthDate { get; set; } //Дата рождения

        public Instructor(int id, string surname, string name, string patronymic, string category, DateTime birthDate)
        {
            Id = id;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Category = category;
            BirthDate = birthDate;

        }

        public Instructor():this(1,"","","","",new DateTime(2000,5,5))
        {

        }

        public override string ToString()
        {
            return $"{Surname} {Name[0]}.{Patronymic[0]}.";
        }
    }
