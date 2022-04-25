using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ_3
{
    class Program
    {
        //Задание
        // 1) Не хранить число в строках
        // 2) Переводить числа сразу в 10 при их считывании
        // 3) Считывание и вывод организовать как отдельные методы
        // 4) Проверка на то что, вводимое число подходит для заданной системы счисления


        static void Main(string[] args)
        {
            Console.WriteLine("Введите через пробел:");
            Console.WriteLine("(1) - исходную СС от 2 до 36, (2) - число в исходной СС и (3) - СС, в которую необходимо перевести число от 2 до 36:");

            //Создание массива для ввода исходных данных
            string inputInformation = Console.ReadLine();
            string[] information = inputInformation.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //Инициализация переменных для значений из массива для более удобной работы в далнейшем
            int inputSystem = Convert.ToInt32(information[0]);
            int outputSystem = Convert.ToInt32(information[2]);
            string inputNumber = information[1];

            //Вывод исходных данных в консоль для наглядности представления значений
            Console.WriteLine($"*************************************************************************************");
            Console.WriteLine($"=> Исходная система счисления - {inputSystem}");
            Console.WriteLine($"=> Исходное число - {inputNumber}");
            Console.WriteLine($"=> Система счисления, в которую необходимо перевести заданное число - {outputSystem}");
            Console.WriteLine($"*************************************************************************************");

            //Проверка на соответствие исходной системы счисления
            if (inputSystem < 2 && inputSystem > 36 )
            {
                throw new ArgumentException($"{inputSystem} - система счисления выходит за границы допустимой");
            }

            //Провверка на соответствие заданной системы
            if (outputSystem < 2 && outputSystem > 36)
            {
                throw new ArgumentException($"{outputSystem} - система счисления выходит за границы допустимой");
            }
            
            //Проверка на соответствие символов числа в заданной системе
            if(NumberIsEnteredCorrectly(inputNumber, inputSystem) == false)
            {
                throw new ArgumentException($"{inputNumber} - исходное число содержит недопустимые для данной системы счисления символы");
            }

            //Поиск подходяший значений исходной и заданной сисем счисления
            if (inputSystem != 10 && outputSystem != 10 )
            {
                int number = ConvertingFromAnotherToDecimalSystem(inputNumber, inputSystem);
                Console.WriteLine($"Число {inputNumber} в {inputSystem}-ой системе счисления будет равно в {outputSystem}-ой системе счисления: ");
                ConvertingFromDecimalToAnotherSystem(number, outputSystem);
            }

            else if (inputSystem == 10 && outputSystem != 10)
            {
                Console.WriteLine($"Число {inputNumber} в {inputSystem}-ой системе счисления будет равно в {outputSystem}-ой системе счисления: ");
                ConvertingFromDecimalToAnotherSystem(Convert.ToInt32(inputNumber), outputSystem);
            }

            else if(inputSystem != 10 && outputSystem == 10)
            {
                Console.WriteLine($"Число {inputNumber} в {inputSystem}-ой системе счисления будет равно в {outputSystem}-ой системе счисления: ");
                Console.WriteLine(ConvertingFromAnotherToDecimalSystem(inputNumber, inputSystem));
            }

            else
            {
                Console.WriteLine($"Число {inputNumber} в {inputSystem}-ой системе счисления будет равно в {outputSystem}-ой системе счисления: ");
                Console.WriteLine(inputNumber);
            }

            Console.WriteLine("Всего доброго!");

            Console.ReadKey();
        }

        /// <summary>
        /// Проверка ввода исходного числа пользователем
        /// </summary>
        /// <param name="inputNumber"></param>
        /// <param name="inputSystem"></param>
        /// <returns></returns>
        private static bool NumberIsEnteredCorrectly(string inputNumber, int inputSystem)
        {
            bool isCorrectly = true;
            for(int i = 0; i < inputNumber.Length; i++)
            {
                if(ConvertingFromLettersToNumber(inputNumber[i]) >= inputSystem && isCorrectly == true)  //Если символы в исходном числе превосходят заданную, то
                {                                                                                        //возвращаем false, иначе true
                    isCorrectly = false;
                }
            }
            return isCorrectly;
        }

        /// <summary>
        /// Перевод букв в цифры
        /// </summary>
        /// <param name="letter"></param>
        /// <returns>Число типа int</returns>
        private static int ConvertingFromLettersToNumber(char letter)
        {
            int number = new int();

            int let = Convert.ToInt32(letter);

            number = (let >= 65 && let <= 90) ? let - 55 : let - '0'; //Сравнение символа со значением Юникода, если true - возвращает символ минус 55
                                                                      //(так как A - 65, B - 66 и т.д.), если false - преобразование в символ цифры
            return number;
        }

        /// <summary>
        /// Перевод цифр в буквы
        /// </summary>
        /// <param name="num">Исходный разряд получающегося числа</param>
        private static char ConvertingFromNumberToLetter(int num)
        {
            char letter = new char();

            letter = num >= 10 ? Convert.ToChar((num + 55)) : Convert.ToChar(num + 48); //Если разряд больше или равен 10, то прибавляем 55, т.к A - 65 = 10 + 55
                                                                                        //иначе прибавляем 48, т.к. '0' = 48
            return letter;
        }


        /// <summary>
        /// Перевод из любой системы счисления в десятичную 
        /// </summary>
        private static int ConvertingFromAnotherToDecimalSystem(string inputNumber, int inputSystem)//Организовать при считывании
        {
            int outputNumber = new int();

            int counterPosition = inputNumber.Length - 1;

            int maxDegree = (int) Math.Pow(inputSystem, counterPosition);

            for (int i = 0; i < inputNumber.Length; i++)
            {
                outputNumber += (ConvertingFromLettersToNumber(inputNumber[i])) * maxDegree;
                maxDegree /= inputSystem;
            }

            return outputNumber;
        }

        /// <summary>
        /// Перевод из десятичной системы счисления в любую другую
        /// </summary>
        /// <param name="inputNumber">Исходное число</param>
        /// <param name="outputSystem"></param>
        private static void ConvertingFromDecimalToAnotherSystem(int inputNumber, int outputSystem)
        {
            int remains = new int();    //Остаток от деления

            int quotient = new int();   //Частное делимого и делителя

            Stack<int> stack = new Stack<int>();    //Инициализация стека

            do
            {
                remains = inputNumber % outputSystem;
                stack.Push(remains);
                quotient = inputNumber / outputSystem;
                if (quotient < outputSystem)
                {
                    stack.Push(quotient);
                }
                inputNumber /= outputSystem;
            }
            while (inputNumber / outputSystem > 0);

            while (stack.Count != 0)
            {
                Console.Write(ConvertingFromNumberToLetter(stack.Pop()));
            }
            
        }
    }
}
