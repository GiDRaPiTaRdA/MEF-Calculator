using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Calculator.Interfaces;

namespace SimpleCalculator3
{
    class Program
    {
        
        string pathToExtentionsDirectory = @"C:\Users\Admin\Desktop\Simple Calculator MEF Application\C#\SimpleCalculator3\Extensions";

        [Import(typeof(ICalculator))]
        private ICalculator calculator;


        private Program()
        {
            //An aggregate catalog that combines multiple catalogs

            var catalog = new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
            catalog.Catalogs.Add(new DirectoryCatalog(this.pathToExtentionsDirectory));



            //Create the CompositionContainer with the parts in the catalog
            CompositionContainer container = new CompositionContainer(catalog);

            //Fill the imports of this object
            try
            {
                container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }


        static void Main(string[] args)
        {
            Program p = new Program(); //Composition is performed in the constructor

            Console.WriteLine("Enter Command:");
            while (true)
            {
                string input = Console.ReadLine();
                Console.WriteLine(p.calculator.Calculate(input));
            }
        }
    }
}
