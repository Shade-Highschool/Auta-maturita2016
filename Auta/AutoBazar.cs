using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auta
{
     class AutoBazar
    {
        public static List<Auto> carList = new List<Auto>();
        public static int vydaje = 0;
        public static int prodano = 0;
         
         public static int Vydelek
        {
            get { return prodano - vydaje;}
        } //propertie | getter,setter | vlastnost



         public static void PridejAuto(Auto car)
         {
             vydaje += car.NakupniCena;
             carList.Add(car);
         } //Auto se koupí - cena do výdajů, auto do listu
         public static void OdstranAuto(int auto)
         {
             carList.RemoveAt(auto);
         } //Odstraní z listu auto
         public static void VratAuto(int auto)
         {
             prodano += carList[auto].NakupniCena;
             carList.RemoveAt(auto);
         } //vrátí za stejnou cenu jako koupil
         public static void ProdejAuto(int auto)
         {
             prodano += carList[auto].ProdejniCena;
             carList.RemoveAt(auto);
         } //prodá



         public static int ZobrazCenuVsechAut()
         {
             int soucet = 0;
             foreach (Auto car in AutoBazar.carList)
                 soucet += car.ProdejniCena;
             return soucet;
         } //sečte ceny a ukáže
         public static void ZlevniVsechnyAuta(int hodnota)
         {
             foreach (Auto car in carList)
                 car.ProdejniCena -= hodnota;
         } //zlevní o hodnotu
         public static void ZdrazVsechnyAuta(int hodnota)
         {
             ZlevniVsechnyAuta(hodnota * (-1));
         } //Tohle se mi vůbec nelíbí, ale chtěl jste to tak
         public static void ZlevniProcenta(int procento)
         {
             foreach(Auto car in carList)
             {
                 int cena = (car.ProdejniCena * procento) / 100;
                 car.ProdejniCena -= cena;
             }
         } //Zlevní o procenta - trojčlenka
         public static void ZdrazProcenta(int procento)
         {
             foreach (Auto car in carList)
             {
                 int cena = (car.ProdejniCena * procento) / 100;
                 car.ProdejniCena += cena;
             }
         } //Zdraží o procenta - trojčlenka
    }
}
