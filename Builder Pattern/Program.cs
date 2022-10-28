/*Шаблон представляет способ создания составного объекта. Он отделяет конструирование сложного объекта от его представления так,
 * что в результате одного и того же процесса конструирования могут получаться разные представления.
 * Положительными моментами паттерна является то, что он позволяет изменять внутреннее представление продукта.
 * Изолирует код, реализующий конструирование и представление. Дает более тонкий контроль над процессом конструирования.
 * Алгоритм создания не зависит от того, из каких частей состоит объект и как они стыкуются между собой
 
 Когда использовать:
 * Когда процесс создания нового объекта не должен зависеть от того, из каких частей этот объект состоит 
 * и как эти части связаны между собой
 * Когда необходимо обеспечить получение различных вариаций объекта в процессе его создания*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_Pattern
{
    //класс конечного продукта, содержит скрытое строковое поле data, т.е. инфомацию о телефоне


    class Phone
    {
        string data; //информация о телефоне
        public Phone() => data = ""; //инициализацию поля data пропишем в конструкторе класса
        public string AboutPhone() => data; //открытый метод aboutphone возвращает значение поля data
        public void AppendData(string str) => data += str; //через строку к data добавляется стадия производства телефона

    }
    //интерфейс разработчика телефонов
    interface IDeveloper //разработчик может:
    {
        void CreateDisplay(); //создает дисплей
        void CreateBox(); //может также создавать корпус
        void SystemInstall(); //устанавливать ОС
        Phone GetPhone();// после разработки представлять телефон
    }

    //конкретный класс разработчика на андроид платформе
    class AndroidDeveloper : IDeveloper
    {
        private Phone phone; //приватное поле типа phone
        public AndroidDeveloper() => phone = new Phone(); //конструктор создает и инициализирует экземпляр phone 
        //переопределенные методы интерфейса//
        public void CreateDisplay() => phone.AppendData("Прозведен дисплей Samsung; "); //методы добавляют соответствующие 
        public void CreateBox() => phone.AppendData("Произведен корпус Samsung; ");     //данные к информации телефона
        public void SystemInstall() => phone.AppendData("Установлена ОС Android; ");    //- передают строку в AppendData
        public Phone GetPhone() => phone; //метод представляяет телефон после разработки
    }

    //конкретный класс разработчика на андроид платформе
    class IphoneDeveloper : IDeveloper
    {
        private Phone phone; //приватное поле типа phone
        public IphoneDeveloper() => phone = new Phone(); //конструктор создает и инициализирует экземпляр phone 
        //переопределенные методы интерфейса
        public void CreateDisplay() => phone.AppendData("Прозведен дисплей Iphone; "); //методы добавляют соответствующие 
        public void CreateBox() => phone.AppendData("Произведен корпус Iphone; ");     //данные к информации телефона
        public void SystemInstall() => phone.AppendData("Установлена IOS; ");
        public Phone GetPhone() => phone; //метод представляяет телефон после разработки
    }

    //класс для управления разработчиками
    class Director
    {
        //поле указаывает разработчика которым управляет директор
        private IDeveloper developer; 
        //инициализируем данное поле developer в конструкторе класса
        public Director(IDeveloper developer) => this.developer = developer;
        //метод которым назначается текущий разработчик, подчиняющийся директору
        public void SetDeveloper(IDeveloper developer) => this.developer = developer;
        
        //директор будет иметь возможность через подчиненного разрабочика смонтировать 
        //и выпустить телефон БЕЗ операционной системы
        public Phone MountOnlyPhone() //создание версии телефона без ОС
        {
            developer.CreateBox();
            developer.CreateDisplay();
            return developer.GetPhone();
        }
        //или выпустить полноценный телефон с OS 
        public Phone MountFullPhone() //создание телефона с ОС
        {
            developer.CreateBox();
            developer.CreateDisplay();
            developer.SystemInstall();
            return developer.GetPhone(); //возвращает тип Phone 
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //создаем разработчика на андроид платформе
            AndroidDeveloper adeveloper = new AndroidDeveloper();
            //создаем директора
            Director director = new Director(adeveloper); //и в конструктор класса передаем разработчика
            //на директоре вызываем метод по созданию полноценного телефона
            Phone samsung = director.MountFullPhone();
            //выводим информацию о созданном телефоне            
            Console.WriteLine(samsung.AboutPhone());

            //создаем разработчика айфонов
            IphoneDeveloper ideveloper = new IphoneDeveloper();
            //назначаем его в подчинение директору
            director.SetDeveloper(ideveloper);
            //на директоре вызываем метод по созданию телефона без ОС
            Phone iphone = director.MountOnlyPhone();
            //выводим инфу о созданном телефоне
            Console.WriteLine(iphone.AboutPhone());
        }
    }
}
