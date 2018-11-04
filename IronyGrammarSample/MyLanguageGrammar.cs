using Irony.Parsing;

namespace IronyGrammarSample
{
    internal class MyLanguageGrammar : Grammar
    {
        public MyLanguageGrammar()
        {

            var sectionIndentifier = new IdentifierTerminal("SectionIdentifier"); 

            var sectionTerm = new NonTerminal("Section");
            sectionTerm.Rule = ToTerm("[") + sectionIndentifier + "]";

            var stringLiteral = new StringLiteral("StringFieldValue", "\"");
            var fieldValue = new NonTerminal("fieldValue");
            fieldValue.Rule = stringLiteral;
            MarkTransient(fieldValue);

            var parameter = new NonTerminal("Parameter");
            var fieldName = new IdentifierTerminal("Parameter Name");
            parameter.Rule = fieldName + ":" + fieldValue;

            var parameterLine = new NonTerminal("Parameter Line");
            MakeListRule(parameterLine, ToTerm(";"), parameter);

            var parameterLines = new NonTerminal("Parameter Lines");
            MakePlusRule(parameterLines, parameterLine);

            var sectionBlock = new NonTerminal("Section Block");
            sectionBlock.Rule = sectionTerm + parameterLines;

            var sectionList = new NonTerminal("Section List");
            MakePlusRule(sectionList, sectionBlock);

            var root = new NonTerminal("Script");
            root.Rule = sectionList | parameterLine;
            this.Root = root;
        }
    }
}