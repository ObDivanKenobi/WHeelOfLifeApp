using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WheelOfLifeApp.Models
{
    public class CategoryForJson
    {
        //id
        public string id { get; set; }
        
        public int doneTasks { get; set; }

        public int totalTasks { get; set; }

        //высота сегмента (0..100)
        //!alert, при 0 невозможно вызвать тултип!
        public double score { get; set; }
        //ширина сегмента (итоговая вычисляется как доля ширины сегмента в сумме ширины всех сегментов)
        public double weight { get; set; }
        //цвет
        public string color { get; set; }
        //подпись для тултипа
        public string label { get; set; }

        public CategoryForJson(string id, int total, int done, double score, double weight, string color, string label)
        {
            this.id = id;
            doneTasks = done;
            totalTasks = total;
            this.score = score;
            this.weight = weight;
            this.color = color;
            this.label = label;
        }
    }
}