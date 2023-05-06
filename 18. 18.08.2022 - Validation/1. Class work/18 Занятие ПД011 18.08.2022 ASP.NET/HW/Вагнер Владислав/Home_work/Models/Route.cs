using System.ComponentModel.DataAnnotations;
using Home_work.Infrastructure;

namespace Home_work.Models;

public class Route
    {
        public int Id { get; set; }

        //Начальная точка
        public string StartingPoint { get; set; }


        //Промежуточная точка
        public string MiddlePoint { get; set; }

        
        //Конечная точка
        public string EndPoint { get; set; }
        

        //Уровень сложности
        public string Complexity { get; set; }

        //Протяженность маршрута
        [UIHint("Number")]
        public int Length { get; set; }

        //Инструктор
        public Instructor InstructorData { get; set; }

        public Route(int id, string startingPoint, string middlePoint, string endPoint, string complexity, int lenghth, Instructor instructorData)
        {
            Id = id;
            StartingPoint = startingPoint;
            MiddlePoint = middlePoint;
            EndPoint = endPoint;
            Complexity = complexity;
            Length = lenghth;
            InstructorData = instructorData;
        }

        //ctor по умолчанию
        public Route():this(0,"Парк 1","Озеро N3","Холм N1", 
            Utils.Complexity[Utils.GetRandom(0,Utils.Complexity.Length)], 
            6, Utils.GetInstructors()[0])
        {

        }
    }
