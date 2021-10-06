using CarAddingApplication.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAddingApplication.ObjectCar
{
    public class Car
    {
        string Plate { get; } 
        int BodyType { get; }
        int Class { get; }
        int CompanyId { get; }
        string Model { get; }
        string Brand { get; }

        public Car(List<string> carInfo)
        {
            Plate = carInfo[0];
            BodyType = int.Parse(carInfo[1]);
            Class = int.Parse(carInfo[2]);
            CompanyId = int.Parse(carInfo[3]);
            Model = carInfo[4];
            Brand = carInfo[5];
        }
    }
}
