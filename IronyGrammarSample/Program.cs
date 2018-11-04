using System;
using System.IO;
using System.Linq; 
using Irony.Parsing;

namespace IronyGrammarSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var expressionGrammar = new MyLanguageGrammar();
            var parser = new Parser(expressionGrammar);
//#include ""filename.txt""
         
            ParseQuery(File.ReadAllText("sample.iss"), parser);
//             ParseQuery(@"
// [Option1]
// woof: 123
// bubu: 3211 
// 
// [Option2]
// test: 321
// ", parser);
// 
//             ParseQuery(@"
// woof: 123
// bubu: 3211 
// 
// ", parser);


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }


        private static ParseTree ParseQuery(string query, Parser exparser)
        {
            var parseResult = exparser.Parse(query);

 
            Console.WriteLine(query);

            if (parseResult.HasErrors())
            {
                foreach (var s in parseResult.ParserMessages.Select(x => x.Message))
                {
                    Console.Write(s);
                }
                return null;
            } 
            Console.WriteLine(parseResult.ToXml());
            return parseResult;
        }
    }
}
