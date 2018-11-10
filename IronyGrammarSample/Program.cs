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

            var text = File.ReadAllText("Files.Section.Lines.iss");
            var tree = ParseQuery(text, parser);

            var newLine = Environment.NewLine;
            Console.WriteLine($"{newLine}{newLine}Press any key to exit...");
            Console.ReadKey();
        }


        private static ParseTree ParseQuery(string query, Parser parser)
        {
            parser.Context.TracingEnabled = true;

            if (parser.Language.Errors.Any())
            {
                foreach (var error in parser.Language.Errors.Select(x => x.Message))
                {
                    Console.WriteLine(error);
                }
            }

            var parseResult = parser.Parse(query);

            Console.WriteLine(query);

            if (parseResult.HasErrors())
            {
                foreach (var error in parseResult.ParserMessages.Select(x => x.Message))
                {
                    Console.Write(error);
                }

                return null;
            }

            Console.WriteLine(parseResult.ToXml());
            return parseResult;
        }
    }
}
