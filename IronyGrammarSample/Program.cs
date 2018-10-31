using System;
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
         
            ParseQuery(@"
[Option1]
woof: 123
bubu: 3211 

[Option2]
test: 321
", parser);

            ParseQuery(@"
woof: 123
bubu: 3211 

", parser);


            Console.WriteLine("Done");
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

    internal class MyLanguageGrammar : Grammar
    {
        public MyLanguageGrammar()
        {
            var optionIndentifier = new IdentifierTerminal("OptionIdentifier"); 

            var optionTerm = new NonTerminal("Option");
            optionTerm.Rule = ToTerm("[") + optionIndentifier + "]";
            
            var numericFildValue = new NumberLiteral("NumericFieldValue");
            var fieldValue = new NonTerminal("fieldValue");
            fieldValue.Rule = numericFildValue;
            MarkTransient(fieldValue);


            var field = new NonTerminal("Field");
            var fieldName = new IdentifierTerminal("fieldName");

            field.Rule = fieldName + ":" + fieldValue;

            var blockFields = new NonTerminal("BlockFields");        
            MakePlusRule(blockFields, field);

            var optionBlock = new NonTerminal("Option Block");

            optionBlock.Rule = optionTerm + blockFields;

            var blockList = new NonTerminal("BlockList");
            MakePlusRule(blockList, optionBlock);

            var configRoot = new NonTerminal("ConfigRoot");
            configRoot.Rule = blockList | blockFields;
            this.Root = configRoot;
        }
    }
}
